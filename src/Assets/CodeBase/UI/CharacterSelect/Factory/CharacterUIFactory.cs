using CodeBase.Constants;
using CodeBase.Extensions;
using CodeBase.Gameplay.Characters.Services;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.UI.CharacterSelect.Configs;
using CodeBase.UI.CharacterSelect.Views;
using UnityEngine;
using Zenject;

namespace CodeBase.UI.CharacterSelect.Factory
{
    public class CharacterUIFactory : ICharacterUIFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IInstantiator _instantiator;
        private readonly ICharacterProgressService _characterProgressService;

        public CharacterUIFactory(IAssetProvider assetProvider, IInstantiator instantiator, ICharacterProgressService characterProgressService)
        {
            _characterProgressService = characterProgressService;
            _assetProvider = assetProvider;
            _instantiator = instantiator;
        }

        public CharacterView CreateCharacterView(Transform parent, CharacterData characterData)
        {
            CharacterView prefab = _assetProvider.LoadAsset<CharacterView>(AssetPath.CharacterView);

            float progress =_characterProgressService.GetProgress(characterData.TypeId);
            
           return _instantiator.InstantiatePrefabForComponent<CharacterView>(prefab, parent)
                .With(x => x.Init(characterData.TypeId, characterData.Icon, characterData.Background,progress));
        }
    }
}