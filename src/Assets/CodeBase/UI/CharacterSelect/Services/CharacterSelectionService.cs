using System.Collections.Generic;
using System.Linq;
using CodeBase.Common.Services.Persistent;
using CodeBase.Data;
using CodeBase.UI.CharacterSelect.Configs;
using CodeBase.UI.CharacterSelect.Enums;
using UniRx;
using Zenject;

namespace CodeBase.UI.CharacterSelect.Services
{
    public class CharacterSelectionService : ICharacterSelectionService, IInitializable, IProgressWatcher
    {
        private readonly List<CharacterTypeId> _characters = new();
        private readonly ReactiveProperty<CharacterTypeId> _currentCharacter = new();
        private readonly CharacterConfig _characterConfig;

        public IReadOnlyList<CharacterTypeId> Characters => _characters;

        public IReadOnlyReactiveProperty<CharacterTypeId> CurrentCharacterId => _currentCharacter;

        public CharacterSelectionService(CharacterConfig characterConfig) => _characterConfig = characterConfig;

        public void Initialize() => _characters.AddRange(_characterConfig.Characters.Select(x => x.TypeId));

        public void SwitchToNextCharacter()
        {
            if (_characters.Count == 0)
                return;

            int currentIndex = _characters.IndexOf(_currentCharacter.Value);
            
            currentIndex = (currentIndex + 1) % _characters.Count;
            _currentCharacter.Value = _characters[currentIndex];
        }

        public void SwitchToPreviousCharacter()
        {
            if (_characters.Count == 0)
                return;

            int currentIndex = _characters.IndexOf(_currentCharacter.Value);
            
            currentIndex = (currentIndex - 1 + _characters.Count) % _characters.Count;
            _currentCharacter.Value = _characters[currentIndex];
        }

        public void SetCharacter(CharacterTypeId characterId)
        {
            CharacterTypeId targetCharacter = _characters.Find(x => x == characterId);

            _currentCharacter.Value = targetCharacter;
        }

        public void Save(ProgressData progressData)
        {
            progressData.PlayerData.LastSelectedCharacter = _currentCharacter.Value;
        }

        public void Load(ProgressData progressData)
        {
            CharacterTypeId lastSavedCharacterId = progressData.PlayerData.LastSelectedCharacter;

            CharacterData characterData = _characterConfig.Characters.FirstOrDefault(x => x.TypeId == lastSavedCharacterId);
            characterData.SetIcons(characterData.Icon, characterData.Background, characterData.MainBackground);

            _currentCharacter.Value = characterData.TypeId;
        }
    }
}