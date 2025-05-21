using System;
using System.Collections.Generic;
using CodeBase.Gameplay.Characters.Configs;
using CodeBase.UI.CharacterSelect.Enums;

namespace CodeBase.Data
{
    [Serializable]
    public class ProgressData
    {
        public SettingsData SettingsData = new();
        public PlayerData PlayerData = new();
    }

    [Serializable]
    public class SettingsData
    {
        public bool IsSoundEnabled = true;
    }
    
    [Serializable]
    public class PlayerData
    {
        public CharacterTypeId LastSelectedCharacter = CharacterTypeId.FirstHero;

        public List<CharacterProgressData> CharacterProgressDatas = new();
    }
}