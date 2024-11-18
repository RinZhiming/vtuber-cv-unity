using System;
using UnityEngine;
using Zenject;

namespace Facial.FacialMovement
{
    public class FacialMovementView : MonoBehaviour, IFacialMovementData
    {
        [Inject] private IFacialMovementRunner facialMovement;
        
        [Range(0, 1)]
        [SerializeField] private float browParam;
        [Range(0, 1)]
        [SerializeField] private float eyeParam;
        [Range(0, 1)]
        [SerializeField] private float mouthOpenParam;
        [Range(0, 1)]
        [SerializeField] private float mouthSizeParam;
        [Range(0, 1)]
        [SerializeField] private float browLeapT = 0.6f;
        [Range(0, 1)]
        [SerializeField] private float eyeLeapT = 0.6f;
        [Range(0, 1)]
        [SerializeField] private float mouthLeapT = 0.6f;

        private void Update()
        {
            facialMovement.UpdateMapper();
            facialMovement.Map();
        }

        public float BrowParam
        {
            get => browParam;
            set => browParam = value;
        }

        public float EyeParam
        {
            get => eyeParam;
            set => eyeParam = value;
        }

        public float MouthOpenParam
        {
            get => mouthOpenParam;
            set => mouthOpenParam = value;
        }

        public float MouthSizeParam
        {
            get => mouthSizeParam;
            set => mouthSizeParam = value;
        }

        public float BrowLeapT
        {
            get => browLeapT;
            set => browLeapT = value;
        }

        public float EyeLeapT
        {
            get => eyeLeapT;
            set => eyeLeapT = value;
        }

        public float MouthLeapT
        {
            get => mouthLeapT;
            set => mouthLeapT = value;
        }
    }
}