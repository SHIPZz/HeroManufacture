using System.Collections.Generic;
using CodeBase.UI.CharacterSelect.Configs;
using CodeBase.UI.CharacterSelect.Enums;
using UniRx;

namespace CodeBase.UI.CharacterSelect.Services
{
    public interface ICharacterSelectionService
    {
        IReadOnlyList<CharacterTypeId> Characters { get; }
        IReadOnlyReactiveProperty<CharacterTypeId> CurrentCharacterId { get; }
        
        void SwitchToNextCharacter();
        void SwitchToPreviousCharacter();
        void SetCharacter(CharacterTypeId characterId);
    }
} 