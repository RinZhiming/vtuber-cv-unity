using System.Collections.Generic;
using UnityEngine;

namespace Facial.FacialCalculate
{
    public class FacialCalculator
    {
        private float distanceOfLeftEyeHeight;
        private float distanceOfRightEyeHeight;
        private float distanceOfNoseHeight;
        private float distanceBetweenLeftPupliAndEyebrow;
        private float distanceBetweenRightPupliAndEyebrow;
        private float distanceOfMouthHeight;
        private float distanceOfMouthWidth;
        private float distanceBetweenEyes;
        
        public void CalculateFacePartsDistance(List<Vector2> points)
        {
            if (points.Count <= 0) return;
            
            switch (points.Count)
            {
                case 68:
                    distanceOfLeftEyeHeight = new Vector2((points[47].x + points[46].x) / 2 - (points[43].x + points[44].x) / 2, (points[47].y + points[46].y) / 2 - (points[43].y + points[44].y) / 2).sqrMagnitude;
                    distanceOfRightEyeHeight = new Vector2((points[40].x + points[41].x) / 2 - (points[38].x + points[37].x) / 2, (points[40].y + points[41].y) / 2 - (points[38].y + points[37].y) / 2).sqrMagnitude;
                    distanceOfNoseHeight = new Vector2(points[33].x - (points[39].x + points[42].x) / 2, points[33].y - (points[39].y + points[42].y) / 2).sqrMagnitude;
                    distanceBetweenLeftPupliAndEyebrow = new Vector2(points[24].x - (points[42].x + points[45].x) / 2, points[24].y - (points[42].y + points[45].y) / 2).sqrMagnitude;
                    distanceBetweenRightPupliAndEyebrow = new Vector2(points[19].x - (points[39].x + points[36].x) / 2, points[19].y - (points[39].y + points[36].y) / 2).sqrMagnitude;
                    distanceOfMouthHeight = new Vector2(points[51].x - points[57].x, points[51].y - points[57].y).sqrMagnitude;
                    distanceOfMouthWidth = new Vector2(points[48].x - points[54].x, points[48].y - points[54].y).sqrMagnitude;
                    distanceBetweenEyes = new Vector2(points[39].x - points[42].x, points[39].y - points[42].y).sqrMagnitude;
                    break;
                case 17:
                    distanceOfLeftEyeHeight = new Vector2(points[12].x - points[11].x, points[12].y - points[11].y).sqrMagnitude;
                    distanceOfRightEyeHeight = new Vector2(points[10].x - points[9].x, points[10].y - points[9].y).sqrMagnitude;
                    distanceOfNoseHeight = new Vector2(points[1].x - (points[3].x + points[4].x) / 2, points[1].y - (points[3].y + points[4].y) / 2).sqrMagnitude;
                    distanceBetweenLeftPupliAndEyebrow = 0;
                    distanceBetweenRightPupliAndEyebrow = 0;
                    distanceOfMouthHeight = new Vector2(points[14].x - points[16].x, points[14].y - points[16].y).sqrMagnitude;
                    distanceOfMouthWidth = new Vector2(points[13].x - points[15].x, points[13].y - points[15].y).sqrMagnitude;
                    distanceBetweenEyes = new Vector2(points[3].x - points[4].x, points[3].y - points[4].y).sqrMagnitude;
                    break;
            }

        }
        
        public float GetLeftEyeOpenRatio()
        {
            var ratio = distanceOfLeftEyeHeight / distanceOfNoseHeight * 0.8f;
            //Debug.Log ("raw LeftEyeOpen ratio: " + ratio);
            return Mathf.InverseLerp(0.003f, 0.009f, ratio);
        }

        public float GetRightEyeOpenRatio()
        {
            var ratio = distanceOfRightEyeHeight / distanceOfNoseHeight * 0.8f;
            //Debug.Log ("raw RightEyeOpen ratio: " + ratio);
            return Mathf.InverseLerp(0.003f, 0.009f, ratio);
        }

        public float GetLeftEyebrowUpRatio()
        {
            var ratio = distanceBetweenLeftPupliAndEyebrow / distanceOfNoseHeight;
            //Debug.Log ("raw LeftEyebrowUP ratio: " + ratio);
            return Mathf.InverseLerp(0.18f, 0.48f, ratio);
        }

        public float GetRightEyebrowUpRatio()
        {
            var ratio = distanceBetweenRightPupliAndEyebrow / distanceOfNoseHeight;
            //Debug.Log ("raw RightEyebrowUP ratio: " + ratio);
            return Mathf.InverseLerp(0.18f, 0.48f, ratio);
        }

        public float GetMouthOpenYRatio()
        {
            var ratio = distanceOfMouthHeight / distanceOfNoseHeight * 0.75f;
            Debug.Log ("raw MouthOpenY ratio: " + ratio);
            return Mathf.InverseLerp(0.06f, 0.6f, ratio);
        }

        public float GetMouthOpenXRatio()
        {
            var ratio = distanceOfMouthWidth / distanceBetweenEyes;
            //Debug.Log ("raw MouthOpenX ratio: " + ratio);
            return Mathf.InverseLerp(1.8f, 2.0f, ratio);
        }
    }
}