using System;
using UnityEngine.Serialization;

namespace Audio.AudioEffect
{
    [Serializable]
    public class CompressorAudioEffectData
    {
        public float threshold;
        public float attack;
        public float release;
        public float makeUpGain;
    }
    
    [Serializable]
    public class VolumeAudioEffectData
    {
        public float volume;
    }
    
    [Serializable]
    public class PitchAudioEffectData
    {
        public float pitch;
        public float fftSize;
        public float overlap;
        public float maxChannels;
    }
    
    [Serializable]
    public class EqualizationAudioEffectData
    {
        public float lowPass;
        public float highPass;
        public float centerFrequency;
        public float octaveRange;
        public float frequencyOutput;
    }
}