using Zenject;

namespace Head
{
    public class HeadTransformController : IHeadTransformRunner
    {
        [Inject] private readonly HeadTransformModel model;

        public void Init()
        {
            model.HeadTransform.Init();    
        }

        public void Convert()
        {
            model.HeadTransform.Convert();    
        }

        public void Dispose()
        {
            model.HeadTransform.Dispose();    
        }
    }
}