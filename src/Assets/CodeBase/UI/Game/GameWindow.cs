using System;
using UnityEngine;
using UnityEngine.UI;
using CodeBase.UI.AbstractWindow;
using UniRx;

namespace CodeBase.UI.Game
{
    public class GameWindow : AbstractWindowBase
    {
        [SerializeField] private Button _menuButton;

        public IObservable<Unit> OnMenuClicked => _menuButton.OnClickAsObservable();
    }
} 