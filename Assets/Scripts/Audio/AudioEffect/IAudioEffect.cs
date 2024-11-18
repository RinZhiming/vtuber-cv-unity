namespace Audio.AudioEffect
{
    public interface IAudioEffect
    {
        public AudioEffectData Init();
        public void MixerAdjust(float value, string effectName);
        public void VolumeAdjust(float value);
        public void PitchAdjust(float value);
        public void FFTSizeAdjust(float value);
        public void OverlapAdjust(float value);
        public void MaxChannelsAdjust(float value);
        public void ThresholdAdjust(float value);
        public void CenterFrequencyAdjust(float value);
        public void OctaveRangeAdjust(float value);
        public void FrequencyOutputAdjust(float value);
        public void LowPassAdjust(float value);
        public void HighPassAdjust(float value);
        public void AttackAdjust(float value);
        public void ReleaseAdjust(float value);
        public void MakeUpGainAdjust(float value);
        public void Reset();
    }
}