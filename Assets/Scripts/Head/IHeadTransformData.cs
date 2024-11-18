namespace Head
{
    public interface IHeadTransformData
    {
        public bool EnableLowPassFilter { get; set; }
        public float PositionLowPass { get; set; }
        public float RotationLowPass { get; set; }
        public float ImageWidth { get; set; }
        public float ImageHeight { get; set; }
    }
}