using System;
using System.Linq;
using R3;
using UnityEngine;
using Zenject;

namespace Audio.Microphone
{
    public class MicrophoneController : IMicrophone
    {
        [Inject] private MicrophoneModel model;
        
        public void Begin()
        {
            model.DeviceName = UnityEngine.Microphone.devices[0];

            var clip = UnityEngine.Microphone.Start(model.DeviceName, true, 10, AudioSettings.outputSampleRate);
            while (!(UnityEngine.Microphone.GetPosition(null) > 0)) { }

            model.AudioSource.loop = true;
            model.AudioSource.clip = clip;
            model.AudioSource.Play();
        }

        public void Mute(bool mute)
        {
            model.AudioSource.mute = mute;
        }

        public void End()
        {
            model.AudioSource.loop = false;
            model.AudioSource.clip = null;
            if(model.AudioSource.isPlaying) model.AudioSource.Stop();
            if(UnityEngine.Microphone.IsRecording(model.DeviceName))
                UnityEngine.Microphone.End(model.DeviceName);
        }

        public float SpectreData()
        {
            var data = new float[256];
            model.AudioSource.GetSpectrumData(data, 0, FFTWindow.Rectangular);
            var dBValue = SpectreData(data);
            
            return Mathf.InverseLerp(-80, 0, dBValue);
        }

        public string GetDeviceName()
        {
            return model.DeviceName;
        }

        public IDisposable HeadphoneDetection(Action<bool> action)
        {
            return Observable.EveryValueChanged(Headphone.HeadphoneDetection.IsFound, e => e.Value).Skip(1).Subscribe(action);
        }

        private float SpectreData(float[] data)
        {
            var sum = data.Sum(t => t * t);
            var rmsValue = Mathf.Sqrt(sum / data.Length);
            var dBValue = 20 * Mathf.Log10(rmsValue / 0.1f);

            return Mathf.Clamp(dBValue, -80, 0); 
        }
    }
}