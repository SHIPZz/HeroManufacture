using System;
using System.Linq;
using CodeBase.Gameplay.Characters.Configs;
using CodeBase.Gameplay.Characters.Services;
using CodeBase.UI.CharacterSelect.Configs;
using CodeBase.UI.CharacterSelect.Enums;
using CodeBase.UI.CharacterSelect.Views;
using CodeBase.UI.Controllers;
using TMPro;
using UniRx;
using UnityEngine;

namespace CodeBase.UI.CharacterSelect.Controllers
{
    public class CharacterProgressCheatController : IController<CharacterProgressCheatWindow>
    {
        private readonly ICharacterProgressService _characterProgressService;
        private readonly CharacterConfig _characterConfig;
        private readonly CompositeDisposable _disposables = new();
        
        private CharacterProgressCheatWindow _window;

        public CharacterProgressCheatController(
            ICharacterProgressService characterProgressService,
            CharacterConfig characterConfig)
        {
            _characterProgressService = characterProgressService;
            _characterConfig = characterConfig;
        }

        public void Initialize()
        {
            SetupDropdown();
            SubscribeToButtons();
        }

        public void BindView(CharacterProgressCheatWindow window)
        {
            _window = window;
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }

        private void SetupDropdown()
        {
            var options = Enum.GetValues(typeof(CharacterTypeId))
                .Cast<CharacterTypeId>()
                .Except(new [] { CharacterTypeId.None })
                .Select(id => new TMP_Dropdown.OptionData(id.ToString()))
                .ToList();

            _window.CharacterIdDropdown.ClearOptions();
            _window.CharacterIdDropdown.AddOptions(options);
        }

        private void SubscribeToButtons()
        {
            _window.UpdateButton
                .OnClickAsObservable()
                .Subscribe(_ => UpdateProgress())
                .AddTo(_disposables);

            _window.CloseButton
                .OnClickAsObservable()
                .Subscribe(_ => _window.Close())
                .AddTo(_disposables);
        }

        private void UpdateProgress()
        {
            if (!float.TryParse(_window.ProgressInput.text,System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out float progress))
            {
                Debug.LogError("Invalid progress value");
                return;
            }

            var selectedId = (CharacterTypeId)Enum.Parse(typeof(CharacterTypeId), 
                _window.CharacterIdDropdown.options[_window.CharacterIdDropdown.value].text);
            
            _characterProgressService.UpdateProgress(selectedId, progress);
        }
    }
} 