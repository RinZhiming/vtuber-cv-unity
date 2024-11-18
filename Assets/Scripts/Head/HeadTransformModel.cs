using Zenject;

namespace Head
{
    public class HeadTransformModel
    {
        [Inject] public HeadTransform HeadTransform { get; set; }
    }
}