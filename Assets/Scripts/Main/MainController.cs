using UnityEngine;
using UnityEngine.UI;
using Utility;
using Zenject;

namespace Main
{
    public class MainController : IMainRunner
    {
        [Inject] private MainModel model;
        
        public void Init()
        {
            foreach (var mainTab in model.MainTabs)
            {
                mainTab.Group.SetActive(false);
                mainTab.Button.onClick.AddListener(() => SelectTab(mainTab));
            }
        }

        public void Camera(Button cameraButton, Button hideCameraButton, CanvasGroup cameraGroup)
        {
            cameraButton.onClick.AddListener(() =>
            {
                cameraGroup.SetActive(true);
                cameraButton.gameObject.SetActive(false);
            });
            hideCameraButton.onClick.AddListener(() =>
            {
                cameraGroup.SetActive(false);
                cameraButton.gameObject.SetActive(true);
            });
        }

        public void Tab(Button tabButton, Button hideButton, CanvasGroup tabGroup)
        {
            tabButton.onClick.AddListener(() => TabButton(tabButton, true, tabGroup));
            hideButton.onClick.AddListener(() => TabButton(tabButton, false, tabGroup));
        }

        private void TabButton(Button tabButton, bool isShow, CanvasGroup tabGroup)
        {
            tabButton.gameObject.SetActive(!isShow);
            tabGroup.SetActive(isShow);
            
            if (!isShow) 
                HideTabs();
            else
            {
                if (model.CurrentTab) 
                    model.CurrentTab.SetActive(true);
            }
        }

        private void SelectTab(MainTab mt)
        {
            foreach (var mainTab in model.MainTabs)
            {
                if (mainTab == mt)
                    mt.Group.SetActive(!mt.Group.IsActiveSelf());
                else
                    mainTab.Group.SetActive(false);
            }
            
            model.CurrentTab = mt.Group.IsActiveSelf() ? mt.Group : null;
        }

        private void HideTabs()
        {
            foreach (var mainTab in model.MainTabs)
                mainTab.Group.SetActive(false);
        }
    }
}