using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CharacterSelect
{
    public class CharacterSelectInstaller : MonoInstaller
    {
        [SerializeField] private Transform[] characters;
        [SerializeField] private Transform[] icons;
        [SerializeField] private Sprite[] iconsPress;
        [SerializeField] private Sprite[] iconsNormal;
        [SerializeField] private GameObject characterSelectPrefab;
        [SerializeField] private ScrollRect scrollRect;
        public override void InstallBindings()
        {
            Container.Bind<Transform[]>().WithId("characters").FromInstance(characters).AsCached().WhenInjectedInto<CharacterSelectModel>();
            Container.Bind<Transform[]>().WithId("icons").FromInstance(icons).AsCached().WhenInjectedInto<CharacterSelectModel>();
            Container.Bind<Sprite[]>().WithId("iconspress").FromInstance(iconsPress).AsCached().WhenInjectedInto<CharacterSelectModel>();
            Container.Bind<Sprite[]>().WithId("iconsnormal").FromInstance(iconsNormal).AsCached().WhenInjectedInto<CharacterSelectModel>();
            Container.Bind<ScrollRect>().FromInstance(scrollRect).AsCached().WhenInjectedInto<CharacterSelectModel>();
            Container.Bind<ICharacterSelect>().To<CharacterSelectController>().FromNewComponentOnNewPrefab(characterSelectPrefab).AsSingle();
            Container.Bind<CharacterSelectModel>().AsSingle();
        }
    }
}