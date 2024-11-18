using System;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;

namespace Audio.Headphone
{
    public class HeadphoneDetection : MonoBehaviour
    {
        public static ReactiveProperty<bool> IsFound { get; set; }= new();
        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            if (DetectHeadset.CanDetect()) 
                IsFound.Value = DetectHeadset.Detect();
        }
    }
}
