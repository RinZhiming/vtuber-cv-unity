using System.Globalization;
using UnityEngine;
using Zenject;

namespace Audio.AudioEffect
{
    public class AudioEffectController : IAudioEffect
    {
        [Inject] private AudioEffectModel model;
        
        public AudioEffectData Init()
        {
            return model.CurrentData;
        }

        public void MixerAdjust(float value, string effectName)
        {
            model.AudioMixer.SetFloat(effectName, value);
        }

        public void VolumeAdjust(float value)
        {
            model.CurrentData.Volume.volume = value;
        }
        
        public void PitchAdjust(float value)
        {
            model.CurrentData.Pitch.pitch = value;
        }
        
        public void FFTSizeAdjust(float value)
        {
            model.CurrentData.Pitch.fftSize = value;
        }
        
        public void OverlapAdjust(float value)
        {
            model.CurrentData.Pitch.overlap = value;
        }
        
        public void MaxChannelsAdjust(float value)
        {
            model.CurrentData.Pitch.maxChannels = value;
        }
        
        public void ThresholdAdjust(float value)
        {
            model.CurrentData.Compressor.threshold = value;
        }
        
        public void CenterFrequencyAdjust(float value)
        {
            model.CurrentData.Equalization.centerFrequency = value;
        }
        
        public void OctaveRangeAdjust(float value)
        {
            model.CurrentData.Equalization.octaveRange = value;
        }
        
        public void FrequencyOutputAdjust(float value)
        {
            model.CurrentData.Equalization.frequencyOutput = value;
        }
        
        public void LowPassAdjust(float value)
        {
            model.CurrentData.Equalization.lowPass = value;
        }
        
        public void HighPassAdjust(float value)
        {
            model.CurrentData.Equalization.highPass = value;
        }
        
        public void AttackAdjust(float value)
        {
            model.CurrentData.Compressor.attack = value;
        }
        
        public void ReleaseAdjust(float value)
        {
            model.CurrentData.Compressor.release = value;
        }
        
        public void MakeUpGainAdjust(float value)
        {
            model.CurrentData.Compressor.makeUpGain = value;
        }
        
        public void Reset()
        {
            
        }
    }
}