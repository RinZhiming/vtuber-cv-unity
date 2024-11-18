using System.Collections.Generic;
using DlibFaceLandmarkDetector;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UnityUtils.Helper;
using UnityEngine;
using UnityEngine.UI;
using Utility;
using Zenject;
using Rect = UnityEngine.Rect;

namespace Facial.FacialDetect
{
    public class FacialDetector : IFacialDetect
    {
        private readonly WebCamTextureToMatHelper webcamToMatHelper;
        private readonly ImageOptimizationHelper imageOptimizationHelper;
        private readonly RawImage screen;
        
        private static readonly string DLIB_SHAPEPREDICTOR_FILENAME_PRESET = "DlibFaceLandmarkDetector/sp_human_face_68.dat";
        private static readonly string DLIB_SHAPEPREDICTOR_MOBILE_FILENAME_PRESET = "DlibFaceLandmarkDetector/sp_human_face_68_for_mobile.dat";
        
        private bool didUpdateFaceLandmarkPoints;
        
        private string dlibShapePredictorFilePath;
        private string dlibShapePredictorFileName;
        private string dlibShapePredictorMobileFileName;
        
        private FaceLandmarkDetector faceLandmarkDetector;
        
        private List<Vector2> faceLandmarkPoints = new();
        private List<Vector2> tempList = new();
        
        private Color32[] debugColors;
        private Texture2D debugTexture;
        
        private Mat debugMat;
        private Mat matSource;
        private Mat matDownScale;
        
        [Inject]
        public FacialDetector(
            WebCamTextureToMatHelper webcamToMatHelper, 
            ImageOptimizationHelper imageOptimizationHelper,
            RawImage screen)
        {
            this.webcamToMatHelper = webcamToMatHelper;
            this.imageOptimizationHelper = imageOptimizationHelper;
            this.screen = screen;
        }

        public void Init()
        {
            webcamToMatHelper.avoidAndroidFrontCameraLowLightIssue = true;
            webcamToMatHelper.Initialize();
            
            if (string.IsNullOrEmpty(dlibShapePredictorFileName))
                dlibShapePredictorFileName = DLIB_SHAPEPREDICTOR_FILENAME_PRESET;

            if (string.IsNullOrEmpty(dlibShapePredictorMobileFileName))
                dlibShapePredictorMobileFileName = DLIB_SHAPEPREDICTOR_MOBILE_FILENAME_PRESET;
            
#if UNITY_ANDROID || UNITY_IOS
            dlibShapePredictorFilePath = DlibFaceLandmarkDetector.UnityUtils.Utils.getFilePath(dlibShapePredictorMobileFileName);
#else
            dlibShapePredictorFilePath = DlibFaceLandmarkDetector.UnityUtils.Utils.getFilePath(dlibShapePredictorFileName);
#endif
            if (string.IsNullOrEmpty(dlibShapePredictorFilePath))
            {
                Debug.LogError("shape predictor file does not exist. Please copy from “DlibFaceLandmarkDetector/StreamingAssets/DlibFaceLandmarkDetector/” to “Assets/StreamingAssets/DlibFaceLandmarkDetector/” folder. ");
            }

            InitDetector();
        }

        private void InitDetector()
        {
            faceLandmarkDetector = new FaceLandmarkDetector(dlibShapePredictorFilePath);

            didUpdateFaceLandmarkPoints = false;
        }

        public void Begin()
        {
            webcamToMatHelper.Play();
        }

        public void Detect(bool isDebugMode = true, bool hideImage = false)
        {
            if (webcamToMatHelper.IsPlaying() && webcamToMatHelper.DidUpdateThisFrame() && !imageOptimizationHelper.IsCurrentFrameSkipped())
            {
                matSource = webcamToMatHelper.GetMat();
                matDownScale = imageOptimizationHelper.GetDownScaleMat(matSource);
            }
            
            if (faceLandmarkDetector == null)
                return;
            
            if (matSource == null)
                return;
            
            if (matDownScale == null)
                return;

            didUpdateFaceLandmarkPoints = false;

            var rgbaMat = matSource;
            var downScaleRgbaMat = matDownScale;
            if (rgbaMat != null)
            {
                if (isDebugMode && screen != null)
                {

                    if (debugMat != null && (debugMat.width() != rgbaMat.width() || debugMat.height() != rgbaMat.height()))
                    {
                        debugMat.Dispose();
                        debugMat = null;
                    }
                    debugMat ??= new Mat(rgbaMat.rows(), rgbaMat.cols(), rgbaMat.type());

                    if (hideImage)
                    {
                        debugMat.setTo(new Scalar(0, 0, 0, 255));
                    }
                    else
                    {
                        rgbaMat.copyTo(debugMat);
                    }

                    if (debugTexture != null && (debugTexture.width != debugMat.width() || debugTexture.height != debugMat.height()))
                    {
                        Object.Destroy(debugTexture);
                        debugTexture = null;
                    }
                    if (debugTexture == null)
                    {
                        debugTexture = new Texture2D(debugMat.width(), debugMat.height(), TextureFormat.RGBA32, false, false);

                        var size = screen.rectTransform.sizeDelta;
                        screen.rectTransform.sizeDelta = new Vector2(size.x, size.x * (float)debugMat.height() / (float)debugMat.width());
                    }

                    if (debugColors != null && debugColors.Length != debugMat.width() * debugMat.height())
                    {
                        debugColors = new Color32[debugMat.width() * debugMat.height()];
                    }
                    screen.texture = debugTexture;
                    screen.enabled = true;
                }
                else
                {
                    if (screen != null)
                        screen.enabled = false;
                }
                
                //detect face rects
                OpenCVUtils.SetImage(faceLandmarkDetector, downScaleRgbaMat);
                var detectResult = faceLandmarkDetector.Detect();

                OpenCVUtils.SetImage(faceLandmarkDetector, rgbaMat);
                if (detectResult.Count > 0)
                {

                    // restore to original size rect
                    var r = detectResult[0];
                    var downscaleRatio = imageOptimizationHelper.downscaleRatio;
                    var rect = new Rect(
                        r.x * downscaleRatio,
                        r.y * downscaleRatio,
                        r.width * downscaleRatio,
                        r.height * downscaleRatio
                    );

                    // detect landmark points
                    var points = faceLandmarkDetector.DetectLandmark(rect);

                    faceLandmarkPoints = points;

                    didUpdateFaceLandmarkPoints = true;

                    if (isDebugMode && screen != null)
                        OpenCVUtils.DrawFaceLandmark(debugMat, points, new Scalar(0, 255, 0, 255), 2);
                }

                //Imgproc.putText (debugMat, "W:" + debugMat.width () + " H:" + debugMat.height () + " SO:" + Screen.orientation, new Point (5, debugMat.rows () - 10), Imgproc.FONT_HERSHEY_SIMPLEX, 0.5, new Scalar (255, 255, 255, 255), 1, Imgproc.LINE_AA, false);
                
                if (isDebugMode && screen != null)
                {
                    OpenCVForUnity.UnityUtils.Utils.matToTexture2D(debugMat, debugTexture, debugColors);
                }
            }
        }

        public void Dispose()
        {
            if (faceLandmarkDetector != null)
                faceLandmarkDetector.Dispose();
            
            if (webcamToMatHelper != null)
                webcamToMatHelper.Dispose();

            if (imageOptimizationHelper != null)
                imageOptimizationHelper.Dispose();

            if (matSource != null)
            {
                matSource.Dispose();
                matSource = null;
            }

            if (matDownScale != null)
            {
                matDownScale.Dispose();
                matDownScale = null;
            }

            if (debugMat != null)
            {
                debugMat.Dispose();
                debugMat = null;
            }

            if (debugTexture == null) return;
            Object.Destroy(debugTexture);
            debugTexture = null;
        }
        
        public List<Vector2> GetFaceLandmarkPoints()
        {
            return didUpdateFaceLandmarkPoints ? faceLandmarkPoints ?? tempList : tempList;
        }
    }
}
