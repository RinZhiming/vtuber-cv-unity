using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Head
{
    public class HeadTransformView : MonoBehaviour
    {
        [Inject] private IHeadTransformRunner headTransform;

        private void Awake()
        {
            
            headTransform.Init();
        }

        private void Update()
        {
            headTransform.Convert();
        }

        private void OnDestroy()
        {
            headTransform.Dispose();
        }
    }
}