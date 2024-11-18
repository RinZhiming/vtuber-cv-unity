using System;

namespace Audio.Microphone
{
    public interface IMicrophone
    {
        public void Begin();
        public void Mute(bool mute);
        public void End();
        public float SpectreData();
        public string GetDeviceName();
        public IDisposable HeadphoneDetection(Action<bool> action);
    }
}