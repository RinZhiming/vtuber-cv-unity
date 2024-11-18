using System;
using System.Linq;
using UnityEngine;
using Utility;
using Zenject;

namespace CharacterSelect
{
    public class CharacterSpawnInstaller : MonoInstaller
    {
        [SerializeField] private GameObject[] characters;
        public static GameObject currentCharacter;
        
        public override void InstallBindings()
        {
            var characterIndex = PlayerPrefs.GetInt(SaveState.Character);
            currentCharacter = characters[characterIndex];

            foreach (var character in characters)
            {
                if(character != currentCharacter) character.SetActive(false);
            }
            
            var skinnedMeshRenderer = currentCharacter.GetComponentsInChildren<SkinnedMeshRenderer>().
                FirstOrDefault(s => s.gameObject.name == "Face");
            
            var animator = currentCharacter.GetComponent<Animator>();
            
            Container.Bind<SkinnedMeshRenderer>().FromInstance(skinnedMeshRenderer).AsSingle();
            Container.Bind<Animator>().FromInstance(animator).AsSingle();
        }
    }
}