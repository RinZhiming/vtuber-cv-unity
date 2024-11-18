using UnityEngine;
using Zenject;

namespace SoundEffect
{
    public class SoundEffectController : ISoundEffectRunner
    {
        [Inject] private SoundEffectModel model;
        
        public void Init(AudioSource source)
        {
            source.playOnAwake = false;
            source.loop = false;
            
            foreach (var soundEffect in model.SoundEffects)
            {
                soundEffect.Button.onClick.AddListener(() => AudioPlay(soundEffect.Clip, source));
            }
        }

        private void AudioPlay(AudioClip clip, AudioSource source)
        {
            source.Stop();
            source.PlayOneShot(clip);
        }
    }
}