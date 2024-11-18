using Zenject;

namespace Head.HeadRotate
{
    public class HeadRotateController : IHeadRotateRunner
    {
        [Inject] private HeadRotateModel model;
        
        public void Init()
        {
            model.HeadRotate.Init();
        }

        public void Rotate()
        {
            model.HeadRotate.Rotate();
        }
    }
}