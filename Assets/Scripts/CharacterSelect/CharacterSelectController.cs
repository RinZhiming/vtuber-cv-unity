using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utility;
using Zenject;

namespace CharacterSelect
{
    public class CharacterSelectController : MonoBehaviour, ICharacterSelect
    {
        [Inject] private CharacterSelectModel model;
        
        public void Init()
        {
            PlayerPrefs.DeleteKey(SaveState.Character);

            for (var i = 0; i < model.Icons.Length; i++)
            {
                var i1 = i;
                model.Icons[i].GetComponent<Button>().onClick.AddListener(() => SelectButton(i1));
            }
            
            for (var i = 0; i < model.Characters.Length; i++)
            {
                var character = model.Characters[i];
                character.localPosition = new Vector3(-i, character.localPosition.y, 0);
            }
            
            Icon();
        }

        private void SelectButton(int i)
        {
            if (!model.IsSwipe)
            {
                CharacterSlide(i - model.CurrentIndex);
            }
        }

        public void SwipeLeft()
        {
            if(model.CurrentIndex < model.Characters.Length - 1)
                CharacterSlide(1);
        }

        public void SwipeRight()
        {
            if(model.CurrentIndex > 0)
                CharacterSlide(-1);
        }

        private async void CharacterSlide(int i)
        {
            if (!model.IsSwipe)
            {
                model.CurrentIndex += i;
                model.IsSwipe = true;
                
                BoundaryCheck();
                
                foreach (var character in model.Characters)
                {
                    character.DOLocalMoveX(character.localPosition.x + i, 0.5f).OnComplete(() =>
                    {
                        character.localPosition = new Vector3(Mathf.FloorToInt(character.localPosition.x), character.localPosition.y, 0);
                    }).OnKill(() =>
                    {
                        character.localPosition = new Vector3(Mathf.FloorToInt(character.localPosition.x), character.localPosition.y, 0);
                    });
                }

                // foreach (var icon in model.Icons)
                // {
                //     icon.DOLocalMoveX(icon.localPosition.x - spaceIcons * i, 0.5f).OnComplete(() =>
                //     {
                //         icon.localPosition = new Vector3(Mathf.FloorToInt(icon.localPosition.x), icon.localPosition.y, 0);
                //     }).OnKill(() =>
                //     {
                //         icon.localPosition = new Vector3(Mathf.FloorToInt(icon.localPosition.x), icon.localPosition.y, 0);
                //     });
                //     
                // }
                
                await UniTask.WaitForSeconds(0.55f);
                model.IsSwipe = false;
            }
        }

        private void BoundaryCheck()
        {
            Icon();
            var viewportRect = model.ScrollRect.viewport;
            var contentRect = model.ScrollRect.content;

            var viewportBounds = RectTransformUtility.CalculateRelativeRectTransformBounds(viewportRect);
            
            var visibleArea = new Bounds(viewportBounds.center, viewportBounds.size + new Vector3(2 * 300, 2 * 300, 0));
            
            for (var i = 0; i < model.Icons.Length; i++)
            {
                var icon = model.Icons[i];
                var itemBounds = RectTransformUtility.CalculateRelativeRectTransformBounds(contentRect, icon);
                if (visibleArea.Intersects(itemBounds))
                {
                    if (model.Icons[i].GetComponent<Button>().image.sprite == model.IconsPress[i])
                    {
                        SetScrollRect(i is 0 or 1 ? 0 : 1);
                    }
                }
            }
            Icon();
        }

        private void SetScrollRect(int i)
        {
            DOVirtual.Float(model.ScrollRect.horizontalNormalizedPosition, i, 0.35f, value =>
            {
                model.ScrollRect.horizontalNormalizedPosition = value;
            });
        }
        
        private void Icon()
        {
            for (var i = 0; i < model.Icons.Length; i++)
            {
                model.Icons[i].GetComponent<Button>().image.sprite = model.IconsNormal[i];
            }
            
            foreach (var icon in model.Icons)
            {
                if(model.Icons[model.CurrentIndex] == icon)
                {
                    icon.GetComponent<Button>().image.sprite = model.IconsPress[model.CurrentIndex];
                }
            }
        }

        public void Select()
        {
            PlayerPrefs.SetInt(SaveState.Character, model.CurrentIndex);
            PlayerPrefs.Save();
            
            SceneManager.LoadScene(Scenes.MainScene);
        }
    }
}