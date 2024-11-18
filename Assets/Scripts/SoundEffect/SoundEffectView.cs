using System;
using UnityEngine;
using Zenject;

namespace SoundEffect
{
    public class SoundEffectView : MonoBehaviour
    {
        [Inject] private ISoundEffectRunner soundEffect;
        [SerializeField] private AudioSource source;
        
        private void Awake()
        {
            soundEffect.Init(source);
        }
    }
}