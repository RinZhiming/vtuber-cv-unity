namespace Facial.FacialDetect
{
    public interface IFacialDetectRunner
    {
        public void Init();
        public void Begin();
        public void Detect();
        public void Dispose();
    }
}