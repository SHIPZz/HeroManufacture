using System.Collections.Generic;
using CodeBase.Common.Services.Persistent;
using CodeBase.Data;
using CodeBase.Gameplay.Characters.Configs;
using CodeBase.UI.CharacterSelect.Configs;
using CodeBase.UI.CharacterSelect.Enums;
using UniRx;
using UnityEngine;

namespace CodeBase.Gameplay.Characters.Services
{
    public class CharacterProgressService : IProgressWatcher, ICharacterProgressService
    {
        private const float MaxProgress = 1f;
        private const float MinProgress = 0.1f;

        private readonly Dictionary<CharacterTypeId, float> _characterProgresses = new();

        private readonly ReactiveProperty<(CharacterTypeId, float)> _progressUpdated = new();

        private readonly CharacterConfig _characterConfig;

        public IReadOnlyReactiveProperty<(CharacterTypeId, float)> ProgressUpdated => _progressUpdated;

        public CharacterProgressService(CharacterConfig characterConfig)
        {
            _characterConfig = characterConfig;
        }

        public void Save(ProgressData progressData)
        {
            progressData.PlayerData.CharacterProgressDatas.Clear();

            foreach ((CharacterTypeId characterTypeId, var progress) in _characterProgresses)
            {
                progressData.PlayerData.CharacterProgressDatas.Add(new CharacterProgressData
                {
                    Id = characterTypeId,
                    Progress = progress
                });
            }
        }

        public void UpdateProgress(CharacterTypeId characterTypeId, float progress)
        {
            if (_characterProgresses.ContainsKey(characterTypeId))
            {
                _characterProgresses[characterTypeId] =
                    Mathf.Clamp(_characterProgresses[characterTypeId] + progress, 0, MaxProgress);
                _progressUpdated.Value = (characterTypeId, _characterProgresses[characterTypeId]);
                return;
            }

            Debug.LogError($"{characterTypeId} not found in character progresses.");
        }

        public float GetProgress(CharacterTypeId characterTypeId)
        {
            if (_characterProgresses.TryGetValue(characterTypeId, out var progress))
                return progress;

            Debug.LogError($"{characterTypeId} not found in character progresses.");
            return 0;
        }

        public void Load(ProgressData progressData)
        {
            if (progressData.PlayerData.CharacterProgressDatas.Count <= 0)
                FillProgressByRandom();
            else
                FillFromData(progressData);
        }

        private void FillFromData(ProgressData progressData)
        {
            foreach (CharacterProgressData characterProgressData in progressData.PlayerData.CharacterProgressDatas)
            {
                _characterProgresses[characterProgressData.Id] = characterProgressData.Progress;
            }
        }

        private void FillProgressByRandom()
        {
            foreach (CharacterData characterData in _characterConfig.Characters)
            {
                _characterProgresses.Add(characterData.TypeId, Random.Range(MinProgress, MaxProgress));
            }
        }
    }
}