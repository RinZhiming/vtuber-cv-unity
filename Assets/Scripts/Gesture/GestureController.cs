using System;
using CharacterSelect;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;
using Zenject;

namespace Gesture
{
    public class GestureController : IGestureRunner
    {
        [Inject] private GestureModel model;
        
        public void Init()
        {
            foreach (var gesture in model.Gestures)
            {
                gesture.Button.onClick.AddListener(() =>
                {
                    if (model.CanPress) Pose(gesture.Animation, gesture.Name);
                });
            }
        }

        private async void Pose(AnimationClip animation, string poseName)
        {
            model.CanPress = false;
            
            foreach (var gesture in model.Gestures)
            {
                gesture.Button.interactable = false;
                gesture.Button.image.sprite = gesture.Name == poseName ? gesture.Press : gesture.NotPress;
            }
            
            var animator = CharacterSpawnInstaller.currentCharacter.GetComponent<Animator>();
            animator.SetTrigger(poseName);
            
            await UniTask.WaitForSeconds(animation.length + 0.5f);
            
            foreach (var gesture in model.Gestures)
            {
                gesture.Button.interactable = true;
                gesture.Button.image.sprite = gesture.NotPress;
            }
            
            model.CanPress = true;
        }
    }
}