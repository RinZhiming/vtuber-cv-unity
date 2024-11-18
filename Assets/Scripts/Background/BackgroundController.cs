using System;
using R3;
using UnityEngine;
using UnityEngine.UI;
using Utility;
using Zenject;

namespace Background
{
    public class BackgroundController : IBackgroundRunner
    {
        [Inject] private BackgroundModel model;

        public void Init(ToggleGroup group)
        {
            var save = PlayerPrefs.GetInt(SaveState.Background);
            if (save == -1)
            {
                group.GetToggleByChildrenIndex(0).isOn = true;
                return;
            }

            group.GetToggleByChildrenIndex(save).isOn = true;
        }

        public IDisposable SelectToggle(ToggleGroup group, Image image)
        {
            return Observable
                .EveryValueChanged(group, g => g.GetFirstActiveToggle())
                .Subscribe(t => SelectBackground(t, image, group));
        }

        private void SelectBackground(Toggle toggle, Image image, ToggleGroup group)
        {
            var index = group.GetIndexByChildrenToggle(toggle);

            if (index != -1)
            {
                PlayerPrefs.SetInt(SaveState.Background, index);
                PlayerPrefs.Save();
            }
            
            foreach (var background in model.Backgrounds)
                if (toggle == background.Toggle)
                    SetBackGround(background, image);
        }

        private void SetBackGround(Background background, Image image)
        {
            image.sprite = background.SpriteBackground;
        }
    }
}