using System;
using CodeBase.UI.CharacterSelect.Enums;
using UnityEngine;

namespace CodeBase.UI.CharacterSelect.Configs
{
    [Serializable]
    public struct CharacterVisualData
    {
        public float Progress;

        public Sprite Icon;
        public Sprite Background;
        public Sprite MainBackground;
        public CharacterTypeId Id;
    }
}