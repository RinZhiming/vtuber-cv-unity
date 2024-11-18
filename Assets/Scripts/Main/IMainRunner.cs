using UnityEngine;
using UnityEngine.UI;

namespace Main
{
    public interface IMainRunner
    {
        public void Init();
        public void Camera(Button cameraButton, Button hideCameraButton, CanvasGroup cameraGroup);
        public void Tab(Button tabButton, Button hideButton, CanvasGroup tabGroup);
    }
}