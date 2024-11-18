using Facial.FacialCalculate;
using Zenject;

namespace Facial.FacialDetect
{
    public class FacialDetectModel
    {
        [Inject] public FacialDetector Detector { get; set; }
        [Inject] public FacialCalculator Calculator { get; set; }
    }
}