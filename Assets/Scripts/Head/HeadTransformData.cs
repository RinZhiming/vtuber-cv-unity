using UnityEngine;

namespace Head
{
    public class HeadTransformData : MonoBehaviour, IHeadTransformData
    {
        [SerializeField] private bool enableLowPassFilter;
        [SerializeField] private float positionLowPass = 2f;
        [SerializeField] private float rotationLowPass = 1f;
        [SerializeField] private float imageWidth = 640;
        [SerializeField] private float imageHeight = 640;

        public float PositionLowPass
        {
            get => positionLowPass;
            set => positionLowPass = value;
        }

        public float RotationLowPass
        {
            get => rotationLowPass;
            set => rotationLowPass = value;
        }

        public float ImageWidth
        {
            get => imageWidth;
            set => imageWidth = value;
        }

        public float ImageHeight
        {
            get => imageHeight;
            set => imageHeight = value;
        }

        public bool EnableLowPassFilter
        {
            get => enableLowPassFilter;
            set => enableLowPassFilter = value;
        }
    }
}