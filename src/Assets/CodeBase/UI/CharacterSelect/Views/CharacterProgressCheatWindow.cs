using CodeBase.UI.AbstractWindow;
using CodeBase.UI.CharacterSelect.Enums;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace CodeBase.UI.CharacterSelect.Views
{
    public class CharacterProgressCheatWindow : AbstractWindowBase
    {
        [SerializeField] private TMP_Dropdown _characterIdDropdown;
        [SerializeField] private TMP_InputField _progressInput;
        [SerializeField] private Button _updateButton;
        [SerializeField] private Button _closeButton;

        public TMP_Dropdown CharacterIdDropdown => _characterIdDropdown;
        public TMP_InputField ProgressInput => _progressInput;
        public Button UpdateButton => _updateButton;
        public Button CloseButton => _closeButton;
    }
} 