using CodeBase.UI.CharacterSelect.Configs;
using CodeBase.UI.CharacterSelect.Views;
using UnityEngine;

namespace CodeBase.UI.CharacterSelect.Factory
{
    public interface ICharacterUIFactory
    {
        CharacterView CreateCharacterView(Transform parent, CharacterData characterData);
    }
}