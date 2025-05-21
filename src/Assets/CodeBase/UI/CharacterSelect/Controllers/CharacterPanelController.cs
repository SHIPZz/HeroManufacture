using System.Collections.Generic;
using CodeBase.Gameplay.Characters.Services;
using CodeBase.UI.CharacterSelect.Configs;
using CodeBase.UI.CharacterSelect.Enums;
using CodeBase.UI.CharacterSelect.Factory;
using CodeBase.UI.CharacterSelect.Services;
using CodeBase.UI.CharacterSelect.Views;
using CodeBase.UI.Controllers;
using UniRx;
using UnityEngine.Pool;

namespace CodeBase.UI.CharacterSelect.Controllers
{
    public class CharacterPanelController : IController<CharacterPanelView>
    {
        private readonly ICharacterSelectionService _characterService;
        private readonly ICharacterUIFactory _characterUIFactory;
        private readonly CompositeDisposable _disposables = new();
        private readonly CharacterConfig _characterConfig;
        private readonly ICharacterProgressService _characterProgressService;

        private CharacterPanelView _window;

        public CharacterPanelController(ICharacterSelectionService characterService,
            ICharacterUIFactory characterUIFactory,
            ICharacterProgressService characterProgressService,
            CharacterConfig characterConfig)
        {
            _characterProgressService = characterProgressService;
            _characterConfig = characterConfig;
            _characterService = characterService;
            _characterUIFactory = characterUIFactory;
        }

        public void Initialize()
        {
            SetupWindow();
            
            _characterService.CurrentCharacterId
                .Subscribe(characterId => _window.RaiseCharacter(characterId))
                .AddTo(_disposables);

            _window
                .OnCharacterSelected
                .Subscribe(characterId => _characterService.SetCharacter(characterId))
                .AddTo(_disposables);
            
            _characterProgressService
                .ProgressUpdated
                .Subscribe(characterProgress => _window.UpdateCharacterProgress(characterProgress.Item1, characterProgress.Item2))
                .AddTo(_disposables);
        }

        public void BindView(CharacterPanelView window) => _window = window;

        public void Dispose() => _disposables.Dispose();

        private void CreateCharacters()
        {
            using (ListPool<CharacterView>.Get(out List<CharacterView> characterViews))
            {
                foreach (CharacterData characterData in _characterConfig.Characters)
                {
                    CharacterView createdView = _characterUIFactory.CreateCharacterView(_window.CharacterLayout, characterData);
                    characterViews.Add(createdView);
                }

                _window.SetCharacters(characterViews);
            }
        }

        private void SetupWindow()
        {
            CreateCharacters();

            _window.ForceLayoutRebuild();
            _window.RaiseCharacter(_characterService.CurrentCharacterId.Value);
        }
    }
}