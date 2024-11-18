using System.Collections.Generic;
using Facial.FacialDetect;
using OpenCVForUnity.Calib3dModule;
using OpenCVForUnity.CoreModule;
using OpenCVForUnity.UnityUtils;
using OpenCVForUnity.UnityUtils.Helper;
using UnityEngine;
using Zenject;

namespace Head
{
    public class HeadTransform : IHeadTransform
    {
        private readonly IHeadTransformData headTransformData;
        private float PositionLowPass => headTransformData.PositionLowPass;
        private float RotationLowPass => headTransformData.RotationLowPass;
        private bool EnableLowPassFilter => headTransformData.EnableLowPassFilter;
        private float ImageWidth
        {
            get => headTransformData.ImageWidth;
            set => headTransformData.ImageWidth = value;
        }
        private float ImageHeight
        {
            get => headTransformData.ImageHeight;
            set => headTransformData.ImageHeight = value;
        }
        
        private PoseData oldPoseData;
        private Vector3 headPosition;
        private Quaternion headRotation;
        private MatOfPoint3f objectPoints68;
        private MatOfPoint3f objectPoints17;
        private MatOfPoint3f objectPoints6;
        private Mat camMatrix;
        private MatOfDouble distCoeffs;
        private MatOfPoint2f imagePoints;
        private Mat rvec;
        private Mat tvec;
        private Mat rgbaMat;
        private Matrix4x4 invertYM;
        private Matrix4x4 invertZM;
        private Matrix4x4 VP;
        private List<Vector2> points = new();
        private bool canUpdate;
        
        [Inject] private readonly WebCamTextureToMatHelper webCamTextureToMatHelper;
        [Inject] private readonly FacialDetector facialDetector;

        public HeadTransform(IHeadTransformData headTransformData)
        {
            this.headTransformData = headTransformData;
        }
        
        public void Init()
        {
            
            //set 3d face object points.
            objectPoints68 = new MatOfPoint3f(
                new Point3(-34, 90, 83),//l eye (Interpupillary breadth)
                new Point3(34, 90, 83),//r eye (Interpupillary breadth)
                new Point3(0.0, 50, 117),//nose (Tip)
                new Point3(0.0, 32, 97),//nose (Subnasale)
                new Point3(-79, 90, 10),//l ear (Bitragion breadth)
                new Point3(79, 90, 10)//r ear (Bitragion breadth)
            );

            objectPoints17 = new MatOfPoint3f(
                new Point3(-34, 90, 83),//l eye (Interpupillary breadth)
                new Point3(34, 90, 83),//r eye (Interpupillary breadth)
                new Point3(0.0, 50, 117),//nose (Tip)
                new Point3(0.0, 32, 97),//nose (Subnasale)
                new Point3(-79, 90, 10),//l ear (Bitragion breadth)
                new Point3(79, 90, 10)//r ear (Bitragion breadth)
            );

            objectPoints6 = new MatOfPoint3f(
                new Point3(-34, 90, 83),//l eye (Interpupillary breadth)
                new Point3(34, 90, 83),//r eye (Interpupillary breadth)
                new Point3(0.0, 50, 117),//nose (Tip)
                new Point3(0.0, 32, 97)//nose (Subnasale)
            );

            imagePoints = new MatOfPoint2f();

            camMatrix = new Mat(3, 3, CvType.CV_64FC1);
            //Debug.Log ("camMatrix " + camMatrix.dump ());

            distCoeffs = new MatOfDouble(0, 0, 0, 0);
            //Debug.Log ("distCoeffs " + distCoeffs.dump ());

            invertYM = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(1, -1, 1));
            //Debug.Log ("invertYM " + invertYM.ToString ());

            invertZM = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(1, 1, -1));
            //Debug.Log ("invertZM " + invertZM.ToString ());

        }

        public void Convert()
        {
            rgbaMat = webCamTextureToMatHelper.GetMat();
            points = facialDetector.GetFaceLandmarkPoints();

            if (points.Count <= 0) return;
            
            if (rgbaMat == null) return;
            if (rgbaMat.width() != ImageWidth || rgbaMat.height() != ImageHeight)
            {
                ImageWidth = rgbaMat.width();
                ImageHeight = rgbaMat.height();
                SetCameraMatrix(camMatrix, ImageWidth, ImageHeight);
            }

            canUpdate = false;
            
            if (points != null)
            {
                MatOfPoint3f objectPoints = null;

                if (points.Count == 68)
                {

                    objectPoints = objectPoints68;

                    imagePoints.fromArray(
                        new Point((points[38].x + points[41].x) / 2, (points[38].y + points[41].y) / 2),//l eye (Interpupillary breadth)
                        new Point((points[43].x + points[46].x) / 2, (points[43].y + points[46].y) / 2),//r eye (Interpupillary breadth)
                        new Point(points[30].x, points[30].y),//nose (Tip)
                        new Point(points[33].x, points[33].y),//nose (Subnasale)
                        new Point(points[0].x, points[0].y),//l ear (Bitragion breadth)
                        new Point(points[16].x, points[16].y)//r ear (Bitragion breadth)
                    );

                }
                else if (points.Count == 17)
                {

                    objectPoints = objectPoints17;

                    imagePoints.fromArray(
                        new Point((points[2].x + points[3].x) / 2, (points[2].y + points[3].y) / 2),//l eye (Interpupillary breadth)
                        new Point((points[4].x + points[5].x) / 2, (points[4].y + points[5].y) / 2),//r eye (Interpupillary breadth)
                        new Point(points[0].x, points[0].y),//nose (Tip)
                        new Point(points[1].x, points[1].y),//nose (Subnasale)
                        new Point(points[6].x, points[6].y),//l ear (Bitragion breadth)
                        new Point(points[8].x, points[8].y)//r ear (Bitragion breadth)
                    );

                }
                else if (points.Count == 6)
                {

                    objectPoints = objectPoints6;

                    imagePoints.fromArray(
                        new Point((points[2].x + points[3].x) / 2, (points[2].y + points[3].y) / 2),//l eye (Interpupillary breadth)
                        new Point((points[4].x + points[5].x) / 2, (points[4].y + points[5].y) / 2),//r eye (Interpupillary breadth)
                        new Point(points[0].x, points[0].y),//nose (Tip)
                        new Point(points[1].x, points[1].y)//nose (Subnasale)
                    );
                }

                // Estimate head pose.
                if (rvec == null || tvec == null)
                {
                    rvec = new Mat(3, 1, CvType.CV_64FC1);
                    tvec = new Mat(3, 1, CvType.CV_64FC1);
                    Calib3d.solvePnP(objectPoints, imagePoints, camMatrix, distCoeffs, rvec, tvec);
                }


                double tvec_x = tvec.get(0, 0)[0], tvec_y = tvec.get(1, 0)[0], tvec_z = tvec.get(2, 0)[0];

                var isNotInViewport = false;
                var pos = VP * new Vector4((float)tvec_x, (float)tvec_y, (float)tvec_z, 1.0f);
                if (pos.w != 0)
                {
                    float x = pos.x / pos.w, y = pos.y / pos.w, z = pos.z / pos.w;
                    if (x < -1.0f || x > 1.0f || y < -1.0f || y > 1.0f || z < -1.0f || z > 1.0f)
                        isNotInViewport = true;
                }

                if (double.IsNaN(tvec_z) || isNotInViewport)
                { // if tvec is wrong data, do not use extrinsic guesses. (the estimated object is not in the camera field of view)
                    Calib3d.solvePnP(objectPoints, imagePoints, camMatrix, distCoeffs, rvec, tvec);
                }
                else
                {
                    Calib3d.solvePnP(objectPoints, imagePoints, camMatrix, distCoeffs, rvec, tvec, true, Calib3d.SOLVEPNP_ITERATIVE);
                }

                //Debug.Log (tvec.dump () + " " + isNotInViewport);

                if (!isNotInViewport)
                {

                    // Convert to unity pose data.
                    var rvecArr = new double[3];
                    rvec.get(0, 0, rvecArr);
                    var tvecArr = new double[3];
                    tvec.get(0, 0, tvecArr);
                    var poseData = ARUtils.ConvertRvecTvecToPoseData(rvecArr, tvecArr);

                    // adjust the position to the scale of real-world space.
                    poseData.pos = new Vector3(poseData.pos.x * 0.001f, poseData.pos.y * 0.001f, poseData.pos.z * 0.001f);

                    // Changes in pos/rot below these thresholds are ignored.
                    if (EnableLowPassFilter)
                    {
                        ARUtils.LowpassPoseData(ref oldPoseData, ref poseData, PositionLowPass, RotationLowPass);
                    }
                    
                    //oldPoseData.pos = Vector3.Lerp(oldPoseData.pos, poseData.pos, 10);
                    oldPoseData.rot = Quaternion.Lerp(oldPoseData.rot, poseData.rot, 10);

                    var transformationM = Matrix4x4.TRS(poseData.pos, poseData.rot, Vector3.one);

                    // right-handed coordinates system (OpenCV) to left-handed one (Unity)
                    // https://stackoverflow.com/questions/30234945/change-handedness-of-a-row-major-4x4-transformation-matrix
                    transformationM = invertYM * transformationM * invertYM;

                    // Apply Y-axis and Z-axis refletion matrix. (Adjust the posture of the AR object)
                    transformationM = transformationM * invertYM * invertZM;

                    //headPosition = ARUtils.ExtractTranslationFromMatrix(ref transformationM);
                    headRotation = ARUtils.ExtractRotationFromMatrix(ref transformationM);

                    canUpdate = true;
                }
            }
        }
        
        public void Dispose()
        {
            if (objectPoints68 != null)
                objectPoints68.Dispose();

            if (camMatrix != null)
                camMatrix.Dispose();
            if (distCoeffs != null)
                distCoeffs.Dispose();

            if (imagePoints != null)
                imagePoints.Dispose();

            if (rvec != null)
                rvec.Dispose();

            if (tvec != null)
                tvec.Dispose();
        }
        
        private void SetCameraMatrix(Mat camMatrix, float width, float height)
        {
            var debug = "";
            var max_d = (double)Mathf.Max(width, height);
            var fx = max_d;
            var fy = max_d;
            var cx = width / 2.0;
            var cy = height / 2.0;
            var arr = new[] { fx, 0, cx, 0, fy, cy, 0, 0, 1 }; // must fixed!!
            camMatrix.put(0, 0, arr);
            // create AR camera P * V Matrix
            var P = ARUtils.CalculateProjectionMatrixFromCameraMatrixValues((float)fx, (float)fy, (float)cx, (float)cy, width, height, 0.01f, 3000f);
            var V = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(1, 1, -1));
            VP = P * V;
        }
        
        public Vector3 GetHeadPosition()
        {
            return canUpdate ? headPosition : Vector3.zero;
        }

        public Quaternion GetHeadRotation()
        {
            return canUpdate ? headRotation : Quaternion.identity;
        }

        public Vector3 GetHeadEulerAngles()
        {
            return canUpdate ? headRotation.eulerAngles : Vector3.zero;
        }
    }
}