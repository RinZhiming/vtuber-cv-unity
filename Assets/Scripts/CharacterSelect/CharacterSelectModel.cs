using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CharacterSelect
{
    public class CharacterSelectModel
    {
        [Inject(Id = "characters")] public Transform[] Characters { get; set; }
        [Inject(Id = "icons")] public Transform[] Icons { get; set; }
        [Inject(Id = "iconspress")] public Sprite[] IconsPress { get; set; }
        [Inject(Id = "iconsnormal")] public Sprite[] IconsNormal { get; set; }
        [Inject] public ScrollRect ScrollRect { get; set; }
        public int CurrentIndex { get; set; }
        public bool IsSwipe { get; set; }
    }
}