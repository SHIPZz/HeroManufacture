using System.Collections.Generic;
using CodeBase.UI.CharacterSelect.Enums;
using UnityEngine;

namespace CodeBase.UI.CharacterSelect.Configs
{
    [CreateAssetMenu(fileName = "CharacterConfig", menuName = "CharacterConfig")]
    public class CharacterConfig : ScriptableObject
    {
        [SerializeField] private List<CharacterData> _characters = new();

        public IReadOnlyList<CharacterData> Characters => _characters;

        public CharacterData Get(CharacterTypeId value)
        {
            return _characters.Count == 0 ? default : _characters.Find(x => x.TypeId == value);
        }

        public CharacterVisualData GetVisualData(CharacterTypeId id)
        {
            CharacterData characterData = Get(id);

            return new CharacterVisualData()
            {
                Background = characterData.Background,
                Icon = characterData.Icon,
                MainBackground = characterData.MainBackground,
                Id = id
            };
        }
    }
}