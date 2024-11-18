using Zenject;

namespace Head.HeadLookAt
{
    public class HeadLookAtController : IHeadLookAtRunner
    {
        [Inject] private HeadLookAtModel model;
        
        public void Init()
        {
            model.HeadLookAt.Init();
        }

        public void LookAt()
        {
            model.HeadLookAt.LooAt();
        }
    }
}