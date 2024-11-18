using Zenject;

namespace Webcam
{
    public class WebcamController : IWebcamRunner
    {
        [Inject] private WebcamModel model;
        
        public void Switch()
        {
            model.WebcamDetect.SwitchCamera();
        }

        public void Dispose()
        {
            model.WebcamDetect.Dispose();
        }
    }
}