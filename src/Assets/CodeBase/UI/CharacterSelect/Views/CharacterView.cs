using System;
using CodeBase.Animations;
using CodeBase.UI.CharacterSelect.Enums;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.CharacterSelect.Views
{
    public class CharacterView : MonoBehaviour
    {
        [field: SerializeField] public CharacterTypeId Id { get; set; }
        
        [SerializeField] private Image _icon;
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private Image _background;
        [SerializeField] private VerticalMoveAnimator _verticalMoveAnimator;
        [SerializeField] private SliderAnimator _sliderAnimator;
        [SerializeField] private Button _selectedButton;
        
        public IObservable<Unit> OnSelectedButtonClicked => _selectedButton?.OnClickAsObservable();

        public RectTransform RectTransform => _rectTransform;

        public void Init(CharacterTypeId id, Sprite icon, Sprite background, float progress)
        {
            Id = id;
            _icon.sprite = icon;
            _background.sprite = background;
            _sliderAnimator.Reset();
            _sliderAnimator.SetValue(progress);
        }
        
        public void SetProgress(float progress)
        {
            _sliderAnimator.SetValue(progress);
        }

        public void Raise() => _verticalMoveAnimator.Raise();

        public void PutDown() => _verticalMoveAnimator.Down();
    }
}