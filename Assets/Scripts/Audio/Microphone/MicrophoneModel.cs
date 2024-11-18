using UnityEngine;
using Zenject;

namespace Audio.Microphone
{
    public class MicrophoneModel
    {
        [Inject] public AudioSource AudioSource { get; set; }
        public string DeviceName { get; set; }
    }
}