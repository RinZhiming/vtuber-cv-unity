using UnityEngine;
using Zenject;

namespace Head.HeadLookAt
{
    public class HeadLookAt : IHeadLookAt
    {
        [Inject] private Animator avatarAnimator;
        [Inject(Id = "lookAtTarget")] private Transform lookAtRoot;
        [Inject(Id = "lookAtRoot")] private Transform lookAtTarget;
        private Vector3 headEulerAngles;
        private Vector3 oldHeadEulerAngle;
        private const float leapT = 0.6f;
        private readonly IHeadData headData;

        #region MapData
            private Vector3 OffsetAngle => headData.OffsetAngle;
            private bool InvertXAxis => headData.InvertXAxis;
            private bool InvertYAxis => headData.InvertYAxis;
            private bool InvertZAxis => headData.InvertZAxis;
            private bool RotateXAxis => headData.RotateXAxis;
            private bool RotateYAxis => headData.RotateYAxis;
            private bool RotateZAxis => headData.RotateZAxis;
            private bool LeapAngle => headData.LeapAngle;

        #endregion

        [Inject]
        public HeadLookAt(IHeadData headData)
        {
            this.headData = headData;
        }
        
        public void Init()
        {
            if (lookAtRoot != null)
            {
                oldHeadEulerAngle = lookAtRoot.localEulerAngles;
            }
        }

        public void LooAt()
        {
            if (lookAtRoot == null)
                return;

            if (oldHeadEulerAngle != Vector3.zero)
            {
            
                headEulerAngles = 
                    new Vector3(headEulerAngles.x + OffsetAngle.x, headEulerAngles.y + OffsetAngle.y, headEulerAngles.z + OffsetAngle.z);
                headEulerAngles = 
                    new Vector3(InvertXAxis ? -headEulerAngles.x : headEulerAngles.x, InvertYAxis ? -headEulerAngles.y : headEulerAngles.y, InvertZAxis ? -headEulerAngles.z : headEulerAngles.z);
                headEulerAngles = 
                    Quaternion.Euler(RotateXAxis ? 90 : 0, RotateYAxis ? 90 : 0, RotateZAxis ? 90 : 0) * headEulerAngles;
            }

            lookAtRoot.localEulerAngles = LeapAngle ? 
                new Vector3(Mathf.LerpAngle(oldHeadEulerAngle.x, headEulerAngles.x, leapT), 
                    Mathf.LerpAngle(oldHeadEulerAngle.y, headEulerAngles.y, leapT), 
                    Mathf.LerpAngle(oldHeadEulerAngle.z, headEulerAngles.z, leapT)) : 
                headEulerAngles;

            oldHeadEulerAngle = lookAtRoot.localEulerAngles;
        }
    }
}