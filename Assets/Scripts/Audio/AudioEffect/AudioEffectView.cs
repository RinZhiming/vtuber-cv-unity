using System;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Audio.AudioEffect
{
    public class AudioEffectView : MonoBehaviour
    {
        [Inject] private IAudioEffect audioEffect;
        [SerializeField] private Slider volumeEffectSlider;
        [SerializeField] private Slider 
            pitchEffectSlider,
            fftSizeEffectSlider,
            overlapEffectSlider,
            maxChannelsEffectSlider;
        
        [SerializeField] private Slider 
            thresholdEffectSlider,
            attackEffectSlider,
            releaseEffectSlider,
            makeUpGainEffectSlider;

        [SerializeField] private Slider 
            lowPassEffectSlider,
            highPassEffectSlider,
            centerFrequencyEffectSlider,
            octaveRangeEffectSlider,
            frequencyOutputEffectSlider;
        
        [SerializeField] private Text volumeText;
        [SerializeField] private Text 
            pitchText,
            fftSizeText,
            overlapText,
            maxChannelsText;
        
        [SerializeField] private Text 
            thresholdText,
            attackText,
            releaseText,
            makeUpGainText;

        [SerializeField] private Text 
            lowPassText,
            highPassText,
            centerFrequencyText,
            octaveRangeText,
            frequencyOutputText;

        public static Action OnValueChange;
        private bool isPresetSelect;
        
        private void Awake()
        {
            isPresetSelect = true;
            
            volumeEffectSlider.onValueChanged.AddListener(f =>
            {
                if(!isPresetSelect) OnValueChange?.Invoke();
                VolumeAdjust(f, "volume");
            });
            pitchEffectSlider.onValueChanged.AddListener(f =>
            {
                if(!isPresetSelect) OnValueChange?.Invoke();
                PitchAdjust(f, "pitch");
            });
            fftSizeEffectSlider.onValueChanged.AddListener(f =>
            {
                if(!isPresetSelect) OnValueChange?.Invoke();
                FFTSizeAdjust(f, "fftSize");
            });
            overlapEffectSlider.onValueChanged.AddListener(f =>
            {
                if(!isPresetSelect) OnValueChange?.Invoke();
                OverlapAdjust(f, "overlap");
            });
            maxChannelsEffectSlider.onValueChanged.AddListener(f =>
            {
                if(!isPresetSelect) OnValueChange?.Invoke();
                MaxChannelsAdjust(f, "maxChannels");
            });
            thresholdEffectSlider.onValueChanged.AddListener(f =>
            {
                if(!isPresetSelect) OnValueChange?.Invoke();
                ThresholdAdjust(f, "threshold");
            });
            attackEffectSlider.onValueChanged.AddListener(f =>
            {
                if(!isPresetSelect) OnValueChange?.Invoke();
                AttackAdjust(f, "attack");
            });
            releaseEffectSlider.onValueChanged.AddListener(f =>
            {
                if(!isPresetSelect) OnValueChange?.Invoke();
                ReleaseAdjust(f, "release");
            });
            makeUpGainEffectSlider.onValueChanged.AddListener(f =>
            {
                if(!isPresetSelect) OnValueChange?.Invoke();
                MakeUpGainAdjust(f, "makeUpGain");
            });
            lowPassEffectSlider.onValueChanged.AddListener(f =>
            {
                if(!isPresetSelect) OnValueChange?.Invoke();
                LowPassAdjust(f, "lowPass");
            });
            highPassEffectSlider.onValueChanged.AddListener(f =>
            {
                if(!isPresetSelect) OnValueChange?.Invoke();
                HighPassAdjust(f, "highPass");
            });
            centerFrequencyEffectSlider.onValueChanged.AddListener(f =>
            {
                if(!isPresetSelect) OnValueChange?.Invoke();
                CenterFrequencyAdjust(f, "centerFrequency");
            });
            octaveRangeEffectSlider.onValueChanged.AddListener(f =>
            {
                if(!isPresetSelect) OnValueChange?.Invoke();
                OctaveRangeAdjust(f, "octaveRange");
            });
            frequencyOutputEffectSlider.onValueChanged.AddListener(f =>
            {
                if(!isPresetSelect) OnValueChange?.Invoke();
                FrequencyOutputAdjust(f, "frequencyOutput");
            });

            isPresetSelect = false;
        }

        public async void SetAudioData(AudioEffectData data, bool init = false)
        {
            isPresetSelect = true;
            
            await UniTask.WaitUntil(() => isPresetSelect);
            
            VolumeAdjust(data.Volume.volume, init ? string.Empty : "volume");
            PitchAdjust(data.Pitch.pitch, init ? string.Empty : "pitch");
            FFTSizeAdjust(data.Pitch.fftSize, init ? string.Empty : "fftSize");
            OverlapAdjust(data.Pitch.overlap, init ? string.Empty : "overlap");
            MaxChannelsAdjust(data.Pitch.maxChannels, init ? string.Empty : "maxChannels");
            ThresholdAdjust(data.Compressor.threshold, init ? string.Empty : "threshold");
            AttackAdjust(data.Compressor.attack, init ? string.Empty : "attack");
            ReleaseAdjust(data.Compressor.release, init ? string.Empty : "release");
            MakeUpGainAdjust(data.Compressor.makeUpGain, init ? string.Empty : "makeUpGain");
            LowPassAdjust(data.Equalization.lowPass, init ? string.Empty : "lowPass");
            HighPassAdjust(data.Equalization.highPass, init ? string.Empty : "highPass");
            CenterFrequencyAdjust(data.Equalization.centerFrequency, init ? string.Empty : "centerFrequency");
            OctaveRangeAdjust(data.Equalization.octaveRange, init ? string.Empty : "octaveRange");
            FrequencyOutputAdjust(data.Equalization.frequencyOutput, init ? string.Empty : "frequencyOutput");

            volumeEffectSlider.value = data.Volume.volume;
            pitchEffectSlider.value = data.Pitch.pitch;
            fftSizeEffectSlider.value = data.Pitch.fftSize;
            overlapEffectSlider.value = data.Pitch.overlap;
            maxChannelsEffectSlider.value = data.Pitch.maxChannels;
            thresholdEffectSlider.value = data.Compressor.threshold;
            attackEffectSlider.value = data.Compressor.attack;
            releaseEffectSlider.value = data.Compressor.release;
            makeUpGainEffectSlider.value = data.Compressor.makeUpGain;
            lowPassEffectSlider.value = data.Equalization.lowPass;
            highPassEffectSlider.value = data.Equalization.highPass;
            centerFrequencyEffectSlider.value = data.Equalization.centerFrequency;
            octaveRangeEffectSlider.value = data.Equalization.octaveRange;
            frequencyOutputEffectSlider.value = data.Equalization.frequencyOutput;
            
            isPresetSelect = false;
        }

        private void VolumeAdjust(float value, string effectName)
        {
            audioEffect.VolumeAdjust(value);
            volumeText.text = value + " db";
            if (!string.IsNullOrEmpty(effectName))
                audioEffect.MixerAdjust(value, effectName);
        }
        
        private void PitchAdjust(float value, string effectName)
        {
            value = MathF.Round(value, 1);
            audioEffect.PitchAdjust(value);
            pitchText.text = value + " x";
            if(!string.IsNullOrEmpty(effectName)) 
                audioEffect.MixerAdjust(value, effectName);
        }
        
        private void FFTSizeAdjust(float value, string effectName)
        {
            audioEffect.FFTSizeAdjust(value);
            fftSizeText.text = value + "";
            if(!string.IsNullOrEmpty(effectName)) 
                audioEffect.MixerAdjust(value, effectName);
        }
        
        private void OverlapAdjust(float value, string effectName)
        {
            value = MathF.Round(value, 2);
            audioEffect.OverlapAdjust(value);
            overlapText.text = value + "";
            if(!string.IsNullOrEmpty(effectName)) 
                audioEffect.MixerAdjust(value, effectName);
        }
        
        private void MaxChannelsAdjust(float value, string effectName)
        {
            value = MathF.Round(value, 2);
            audioEffect.MaxChannelsAdjust(value);
            maxChannelsText.text = value + " cn";
            if(!string.IsNullOrEmpty(effectName)) 
                audioEffect.MixerAdjust(value, effectName);
        }
        
        private void ThresholdAdjust(float value, string effectName)
        {
            value = MathF.Round(value, 1);
            audioEffect.ThresholdAdjust(value);
            thresholdText.text = value + " db";
            if(!string.IsNullOrEmpty(effectName)) 
                audioEffect.MixerAdjust(value, effectName);
        }
        
        private void CenterFrequencyAdjust(float value, string effectName)
        {
            audioEffect.CenterFrequencyAdjust(value);
            centerFrequencyText.text = value + " db";
            if(!string.IsNullOrEmpty(effectName)) 
                audioEffect.MixerAdjust(value, effectName);
        }
        
        private void OctaveRangeAdjust(float value, string effectName)
        {
            value = MathF.Round(value, 1);
            audioEffect.OctaveRangeAdjust(value);
            octaveRangeText.text = value + " otv";
            if(!string.IsNullOrEmpty(effectName)) 
                audioEffect.MixerAdjust(value, effectName);
        }
        
        private void FrequencyOutputAdjust(float value, string effectName)
        {
            value = MathF.Round(value, 2);
            audioEffect.FrequencyOutputAdjust(value);
            frequencyOutputText.text = value + "";
            if(!string.IsNullOrEmpty(effectName)) 
                audioEffect.MixerAdjust(value, effectName);
        }
        
        private void LowPassAdjust(float value, string effectName)
        {
            audioEffect.LowPassAdjust(value);
            lowPassText.text = value + " hz";
            if(!string.IsNullOrEmpty(effectName)) 
                audioEffect.MixerAdjust(value, effectName);
        }
        
        private void HighPassAdjust(float value, string effectName)
        {
            audioEffect.HighPassAdjust(value);
            highPassText.text = value + " hz";
            if(!string.IsNullOrEmpty(effectName)) 
                audioEffect.MixerAdjust(value, effectName);
        }
        
        private void AttackAdjust(float value, string effectName)
        {
            value = MathF.Round(value, 1);
            audioEffect.AttackAdjust(value);
            attackText.text = value + " ms";
            if(!string.IsNullOrEmpty(effectName)) 
                audioEffect.MixerAdjust(value, effectName);
        }
        
        private void ReleaseAdjust(float value, string effectName)
        {
            value = MathF.Round(value, 1);
            audioEffect.ReleaseAdjust(value);
            releaseText.text = value + " ms";
            if(!string.IsNullOrEmpty(effectName)) 
                audioEffect.MixerAdjust(value, effectName);
        }
        
        private void MakeUpGainAdjust(float value, string effectName)
        {
            audioEffect.MakeUpGainAdjust(value);
            makeUpGainText.text = value + " db";
            if(!string.IsNullOrEmpty(effectName)) 
                audioEffect.MixerAdjust(value, effectName);
        }
    }
}