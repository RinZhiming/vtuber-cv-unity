using Cysharp.Threading.Tasks;
using OpenCVForUnity.UnityUtils.Helper;
using UnityEngine;
using Zenject;

namespace Facial.FacialDetect
{
    public class FacialDetectView : MonoBehaviour
    {
        [Inject] private IFacialDetectRunner faceDetect;

        private async void Awake()
        {
            
            faceDetect.Init();
        }

        private async void Start()
        {
            
            faceDetect.Begin();
        }

        private async void Update()
        {
            
            faceDetect.Detect();
        }

        private void OnDestroy()
        {
            faceDetect.Dispose();
        }
    }
}