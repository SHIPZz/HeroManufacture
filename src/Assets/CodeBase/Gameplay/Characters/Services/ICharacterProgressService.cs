using CodeBase.UI.CharacterSelect.Enums;
using UniRx;

namespace CodeBase.Gameplay.Characters.Services
{
    public interface ICharacterProgressService
    {
        void UpdateProgress(CharacterTypeId characterTypeId, float progress);
        float GetProgress(CharacterTypeId characterTypeId);
        IReadOnlyReactiveProperty<(CharacterTypeId, float)> ProgressUpdated { get; }
    }
}