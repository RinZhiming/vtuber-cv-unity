using UnityEngine;
using Zenject;
using Vector3 = UnityEngine.Vector3;

namespace Head.HeadRotate
{
    public class HeadRotate : IHeadRotate
    {
        [Inject] private readonly Transform HeadTransform;
        [Inject] private readonly HeadTransform head;
        private readonly IHeadData headData;
        private const float leapT = 0.6f;
        private Vector3 headEulerAngles;
        private Vector3 oldHeadEulerAngle;
        
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
        public HeadRotate(IHeadData headData)
        {
            this.headData = headData;
        }
        
        public void Init()
        {
            if (HeadTransform != null)
            {
                oldHeadEulerAngle = HeadTransform.localEulerAngles;
            }
        }

        public void Rotate()
        {
            if (head == null)
                return;
            if (!HeadTransform)
                return;

            if (head.GetHeadEulerAngles() != Vector3.zero)
            {
                headEulerAngles = head.GetHeadEulerAngles();

                headEulerAngles = new Vector3(
                    headEulerAngles.x + OffsetAngle.x, 
                    headEulerAngles.y + OffsetAngle.y, 
                    headEulerAngles.z + OffsetAngle.z);
                
                headEulerAngles = new Vector3(
                    InvertXAxis ? -headEulerAngles.x : headEulerAngles.x, 
                    InvertYAxis ? -headEulerAngles.y : headEulerAngles.y, 
                    InvertZAxis ? -headEulerAngles.z : headEulerAngles.z);
                
                headEulerAngles = Quaternion.Euler(
                    RotateXAxis ? 90 : 0, RotateYAxis ? 90 : 0, RotateZAxis ? 90 : 0) * headEulerAngles;
            }

            if (LeapAngle)
            {
                var oldPos = HeadTransform.localPosition;
                var newpos = new Vector3(
                    Mathf.LerpAngle(oldHeadEulerAngle.y, headEulerAngles.y, 2.5f), 
                    Mathf.LerpAngle(-oldHeadEulerAngle.x, -headEulerAngles.x, 2.5f), 
                    HeadTransform.localPosition.z).normalized;

                HeadTransform.localPosition = Vector3.Lerp(oldPos, new Vector3(newpos.x,0,0), 3 * Time.deltaTime);
            }
            else
            {
                HeadTransform.localPosition = headEulerAngles;
            }

            oldHeadEulerAngle = HeadTransform.localPosition;
        }
    }
}