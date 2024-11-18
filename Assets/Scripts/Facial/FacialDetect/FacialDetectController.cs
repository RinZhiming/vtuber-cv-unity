using Zenject;

namespace Facial.FacialDetect
{
    public class FacialDetectController : IFacialDetectRunner
    {
        [Inject] private FacialDetectModel model;

        public void Init()
        {
            model.Detector.Init();
        }

        public void Begin()
        {
            model.Detector.Begin();
        }

        public void Detect()
        {
            model.Detector.Detect();
            
            model.Calculator.CalculateFacePartsDistance(model.Detector.GetFaceLandmarkPoints());
        }

        public void Dispose()
        {
            model.Detector.Dispose();
        }
    }
}
