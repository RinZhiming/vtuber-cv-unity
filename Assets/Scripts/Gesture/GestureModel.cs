using Zenject;

namespace Gesture
{
    public class GestureModel
    {
        [Inject] public Gesture[] Gestures { get; set; }
        public bool CanPress { get; set; } = true;
    }
}