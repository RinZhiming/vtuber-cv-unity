using OpenCVForUnity.UnityUtils.Helper;
using UnityEngine;
using Zenject;

namespace Webcam
{
    public class WebcamDetect : IWebcamDetect
    {
        private readonly WebCamTextureToMatHelper webcamToMatHelper;

        [Inject]
        public WebcamDetect(WebCamTextureToMatHelper webcamToMatHelper)
        {
            this.webcamToMatHelper = webcamToMatHelper;
        }

        public void SwitchCamera()
        {
            if (!webcamToMatHelper.IsInitialized()) return;
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL

            var deviceName = webcamToMatHelper.GetDeviceName();
            int nextCameraIndex = -1;
            for (int cameraIndex = 0; cameraIndex < WebCamTexture.devices.Length; cameraIndex++)
            {
                if (WebCamTexture.devices[cameraIndex].name == deviceName)
                {
                    nextCameraIndex = ++cameraIndex % WebCamTexture.devices.Length;
                    break;
                }
            }
            if (nextCameraIndex != -1)
            {
                webcamToMatHelper.requestedDeviceName = nextCameraIndex.ToString();
            }
            else
            {
                webcamToMatHelper.requestedIsFrontFacing = !webcamToMatHelper.requestedIsFrontFacing;
            }
#else
            webcamToMatHelper.requestedIsFrontFacing = !webcamToMatHelper.requestedIsFrontFacing;
#endif
        }

        public void Dispose()
        {
            if (webcamToMatHelper != null)
                webcamToMatHelper.Dispose();
        }
    }
}