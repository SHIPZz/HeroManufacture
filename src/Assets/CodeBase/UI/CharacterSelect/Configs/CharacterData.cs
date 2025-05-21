using System;
using CodeBase.UI.CharacterSelect.Enums;
using Newtonsoft.Json;
using UnityEngine;

namespace CodeBase.UI.CharacterSelect.Configs
{
    [Serializable]
    public struct CharacterData : IEquatable<CharacterData>
    {
        public CharacterTypeId TypeId;
        public string Name;
        
        [JsonIgnore] public Sprite Icon;
        [JsonIgnore] public Sprite Background;
        [JsonIgnore] public Sprite MainBackground;

        public CharacterData(CharacterTypeId typeId, string name, Sprite icon, Sprite background, float progress, Sprite mainBackground)
        {
            TypeId = typeId;
            Name = name;
            Icon = icon;
            Background = background;
            MainBackground = mainBackground;
        }

        public void SetIcons(Sprite icon, Sprite background, Sprite mainBackground)
        {
            Icon = icon;
            Background = background;
            MainBackground = mainBackground;
        }

        public bool Equals(CharacterData other) => TypeId == other.TypeId && Name == other.Name &&
                                                   Equals(Icon, other.Icon);

        public override bool Equals(object obj) => obj is CharacterData other && Equals(other);

        public override int GetHashCode() => HashCode.Combine((int)TypeId, Name, Icon);
    }
}