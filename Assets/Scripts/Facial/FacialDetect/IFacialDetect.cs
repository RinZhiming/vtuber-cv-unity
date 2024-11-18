namespace Facial.FacialDetect
{
    public interface IFacialDetect
    {
        public void Init();
        public void Begin();
        public void Detect(bool isDebugMode = true, bool hideImage = false);
        public void Dispose();
    }
}