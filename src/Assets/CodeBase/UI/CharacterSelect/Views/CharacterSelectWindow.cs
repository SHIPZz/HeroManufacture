using System;
using System.Collections.Generic;
using CodeBase.Animations;
using CodeBase.UI.AbstractWindow;
using CodeBase.UI.CharacterSelect.Configs;
using CodeBase.UI.CharacterSelect.Enums;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.CharacterSelect.Views
{
    public class CharacterSelectWindow : AbstractWindowBase
    {
        [SerializeField] private Button _previousCharacterButton;
        [SerializeField] private Button _nextCharacterButton;
        [SerializeField] private Button _backToMenuButton;
        [SerializeField] private CharacterView _mainSelectedCharacter;
        [SerializeField] private Transform _characterPanelViewParent;

        [SerializeField] private ScaleAnimator _windowScaleAnimator;
        [SerializeField] private ScaleAnimator _characterScaleAnimator;

        public Transform CharacterPanelViewParent => _characterPanelViewParent;

        public IObservable<Unit> OnPreviousCharacterClicked => _previousCharacterButton.OnClickAsObservable();
        public IObservable<Unit> OnNextCharacterClicked => _nextCharacterButton.OnClickAsObservable();
        public IObservable<Unit> OnBackToMenuClicked => _backToMenuButton.OnClickAsObservable();

        protected override void OnOpenStarted() => transform.localScale = Vector3.zero;

        protected override void OnOpen() => AnimateWindowShow();

        public void SwitchCharacter(CharacterVisualData characterVisualData)
        {
            _characterScaleAnimator.AnimateToZero();

            _characterScaleAnimator.OnAnimationComplete(() =>
            {
                _mainSelectedCharacter.Init(characterVisualData.Id, characterVisualData.Icon, characterVisualData.MainBackground, characterVisualData.Progress);
                _characterScaleAnimator.AnimateToOne();
            });
        }

        public void UpdateMainCharacterProgress(CharacterTypeId id, float progress)
        {
            if(_mainSelectedCharacter.Id == id)
                _mainSelectedCharacter.SetProgress(progress);
        }

        private void AnimateWindowShow() => _windowScaleAnimator.AnimateToOne();
    }
}