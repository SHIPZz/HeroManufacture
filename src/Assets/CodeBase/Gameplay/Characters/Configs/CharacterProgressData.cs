using System;
using CodeBase.UI.CharacterSelect.Enums;

namespace CodeBase.Gameplay.Characters.Configs
{
    [Serializable]
    public class CharacterProgressData
    {
        public CharacterTypeId Id;
        public float Progress;
    }
}