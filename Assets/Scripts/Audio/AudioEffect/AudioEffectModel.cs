using UnityEngine;
using UnityEngine.Audio;
using Zenject;

namespace Audio.AudioEffect
{
    public class AudioEffectModel
    {
        [Inject] public AudioMixer AudioMixer { get; private set; }
        public AudioEffectData InitData { get; private set; } = Resources.Load<AudioEffectData>("Setting/AudioEffectInitData");
        public AudioEffectData CurrentData { get; set; } = Resources.Load<AudioEffectData>("Setting/AudioEffectData");
    }
}