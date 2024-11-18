using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Head.HeadLookAt
{
    public class HeadLookAtView : MonoBehaviour, IHeadData
    {
        [SerializeField] private bool invertXAxis;
        [SerializeField] private bool invertYAxis;
        [SerializeField] private bool invertZAxis;
        [SerializeField] private bool rotateXAxis;
        [SerializeField] private bool rotateYAxis;
        [SerializeField] private bool rotateZAxis;
        [SerializeField] private bool leapAngle;
        [SerializeField] private Vector3 offsetAngle;

        [Inject] private IHeadLookAtRunner headLookAt;
        
        private async void Awake()
        {
            headLookAt.Init();
        }

        private void Update()
        {
            headLookAt.LookAt();
        }

        public bool InvertXAxis
        {
            get => invertXAxis;
            set => invertXAxis = value;
        }

        public bool InvertYAxis
        {
            get => invertYAxis;
            set => invertYAxis = value;
        }

        public bool InvertZAxis
        {
            get => invertZAxis;
            set => invertZAxis = value;
        }

        public bool RotateXAxis
        {
            get => rotateXAxis;
            set => rotateXAxis = value;
        }

        public bool RotateYAxis
        {
            get => rotateYAxis;
            set => rotateYAxis = value;
        }

        public bool RotateZAxis
        {
            get => rotateZAxis;
            set => rotateZAxis = value;
        }

        public bool LeapAngle
        {
            get => leapAngle;
            set => leapAngle = value;
        }

        public Vector3 OffsetAngle
        {
            get => offsetAngle;
            set => offsetAngle = value;
        }
    }
}