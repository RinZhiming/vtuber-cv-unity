using Zenject;

namespace Webcam
{
    public class WebcamModel
    {
        [Inject] public WebcamDetect WebcamDetect { get; set; }
    }
}