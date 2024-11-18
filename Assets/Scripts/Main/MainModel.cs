using UnityEngine;
using Zenject;

namespace Main
{
    public class MainModel
    {
        [Inject] public MainTab[] MainTabs { get; set; }
        public CanvasGroup CurrentTab { get; set; }
    }
}