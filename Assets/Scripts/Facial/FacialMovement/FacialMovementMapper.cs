using Facial.FacialCalculate;
using UnityEngine;
using Zenject;

namespace Facial.FacialMovement
{
    public class FacialMovementMapper : IFacialMovement
    {
        private readonly SkinnedMeshRenderer faceBlendShapes;
        private readonly FacialCalculator calculator;
        private readonly IFacialMovementData facialMovementData;

        #region MapData
            private float BrowParam
            {
                get => facialMovementData.BrowParam;
                set => facialMovementData.BrowParam = value;
            }

            private float EyeParam
            {
                get => facialMovementData.EyeParam;
                set => facialMovementData.EyeParam = value;
            }

            private float MouthOpenParam
            {
                get => facialMovementData.MouthOpenParam;
                set => facialMovementData.MouthOpenParam = value;
            }

            private float MouthSizeParam
            {
                get => facialMovementData.MouthSizeParam;
                set => facialMovementData.MouthSizeParam = value;
            }

            private float BrowLeapT
            {
                get => facialMovementData.BrowLeapT;
                set => facialMovementData.BrowLeapT = value;
            }

            private float EyeLeapT
            {
                get => facialMovementData.EyeLeapT;
                set => facialMovementData.EyeLeapT = value;
            }

            private float MouthLeapT
            {
                get => facialMovementData.MouthLeapT;
                set => facialMovementData.MouthLeapT = value;
            }
        #endregion

        [Inject]
        public FacialMovementMapper(SkinnedMeshRenderer faceBlendShapes, FacialCalculator calculator, IFacialMovementData facialMovementData)
        {
            this.faceBlendShapes = faceBlendShapes;
            this.calculator = calculator;
            this.facialMovementData = facialMovementData;
        }

        public void UpdateMapper()
        {
            var eyeOpen = (calculator.GetLeftEyeOpenRatio() + calculator.GetRightEyeOpenRatio()) / 2.0f;

            eyeOpen = eyeOpen >= 0.4f ? 1.0f : 0.0f;
            EyeParam = Mathf.Lerp(EyeParam, 1 - eyeOpen, EyeLeapT);
            
            var mouthOpen = calculator.GetMouthOpenYRatio();

            switch (mouthOpen)
            {
                case >= 0.7f:
                    mouthOpen = 1.0f;
                    break;
                case >= 0.25f:
                    mouthOpen = 0.5f;
                    break;
                default:
                    mouthOpen = 0.0f;
                    break;
            }
            
            MouthOpenParam = Mathf.Lerp(MouthOpenParam, mouthOpen, MouthLeapT);
        }

        public void Map()
        {
            if (!faceBlendShapes)
                return;
            
            
            
            faceBlendShapes.SetBlendShapeWeight(14, EyeParam * 100);
            faceBlendShapes.SetBlendShapeWeight(15, EyeParam * 100);
            
            if (MouthOpenParam >= 0.7f)
            {
                faceBlendShapes.SetBlendShapeWeight(39, MouthOpenParam * 100);
            }
            else if (MouthOpenParam >= 0.25f)
            {
                faceBlendShapes.SetBlendShapeWeight(39, MouthOpenParam * 80);
            }
            else
            {
                faceBlendShapes.SetBlendShapeWeight(39, 0);
            }
        }
    }
}
