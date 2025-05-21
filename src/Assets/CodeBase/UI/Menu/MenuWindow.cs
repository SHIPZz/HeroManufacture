using System;
using UnityEngine;
using UnityEngine.UI;
using CodeBase.UI.AbstractWindow;
using UniRx;

namespace CodeBase.UI.Menu
{
    public class MenuWindow : AbstractWindowBase
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _characterSelectButton;
        [SerializeField] private Button _quitButton;

        public IObservable<Unit> OnPlayClicked => _playButton.OnClickAsObservable();
        public IObservable<Unit> OnSettingsClicked => _settingsButton.OnClickAsObservable();
        public IObservable<Unit> OnCharacterSelectClicked => _characterSelectButton.OnClickAsObservable();
        public IObservable<Unit> OnQuitClicked => _quitButton.OnClickAsObservable();
    }
}