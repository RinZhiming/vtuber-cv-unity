using System;
using UnityEngine;
using Zenject;

namespace Head.HeadLookAt
{
    public class LookAtIKAnimator : MonoBehaviour
    {
        [Inject] private Animator animator;
        [Inject(Id = "lookAtTarget")]private Transform lookAtTarget;

        [Range(0, 1.0f), SerializeField] private float weight = 1.0f;
        [Range(0, 1.0f), SerializeField] private float bodyWeight = 0.5f;
        [Range(0, 1.0f), SerializeField] private float headWeight = 0.0f;
        [Range(0, 1.0f), SerializeField] private float eyesWeight = 0.5f;
        [Range(0, 1.0f), SerializeField] private float clampWeight = 0.0f;


        public void OnAnimatorIK(int layerIndex)
        {
            if(!animator) return;
            
            animator.SetLookAtWeight(weight, bodyWeight, headWeight, eyesWeight, clampWeight);
            animator.SetLookAtPosition(lookAtTarget.position);
        }
    }
}