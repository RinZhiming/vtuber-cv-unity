using UnityEngine;

namespace Head
{
    public interface IHeadData
    {
        public bool InvertXAxis { get; set; }
        public bool InvertYAxis { get; set; }
        public bool InvertZAxis { get; set; }
        public bool RotateXAxis { get; set; }
        public bool RotateYAxis { get; set; }
        public bool RotateZAxis { get; set; }
        public bool LeapAngle { get; set; }
        public Vector3 OffsetAngle { get; set; }
    }
}