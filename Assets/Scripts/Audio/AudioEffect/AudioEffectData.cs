using System;
using System.Collections.Generic;
using UnityEngine;

namespace Audio.AudioEffect
{
    [CreateAssetMenu(fileName = "new AudioEffectData", menuName = "Snow/Audio/Audio Effect Data", order = 0)]
    public class AudioEffectData : ScriptableObject
    {
        [SerializeField] private CompressorAudioEffectData compressor;
        [SerializeField] private VolumeAudioEffectData volume;
        [SerializeField] private PitchAudioEffectData pitch;
        [SerializeField] private EqualizationAudioEffectData equalization;

        public CompressorAudioEffectData Compressor => compressor;

        public VolumeAudioEffectData Volume => volume;

        public PitchAudioEffectData Pitch => pitch;

        public EqualizationAudioEffectData Equalization => equalization;
    }
}