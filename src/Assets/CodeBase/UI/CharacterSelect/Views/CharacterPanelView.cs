using System;
using System.Collections.Generic;
using System.Linq;
using CodeBase.UI.AbstractWindow;
using CodeBase.UI.CharacterSelect.Enums;
using DG.Tweening;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.CharacterSelect.Views
{
    public class CharacterPanelView : AbstractWindowBase
    {
        [SerializeField] private List<CharacterView> _characterViews;
        [SerializeField] private Transform _characterLayout;
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private float _scrollDuration = 0.3f;
        [SerializeField] private float _initialXPosition = 260f;

        private readonly Subject<CharacterTypeId> _onCharacterSelected = new();
        
        private CharacterView _currentCharacter;
        private RectTransform _scrollContent;
        private Tweener _scrollTween;

        public Transform CharacterLayout => _characterLayout;

        public IObservable<CharacterTypeId> OnCharacterSelected => _onCharacterSelected;

        protected override void OnAwake()
        {
            _scrollContent = _scrollRect.content;
            _scrollContent.anchoredPosition = new Vector2(_initialXPosition, _scrollContent.anchoredPosition.y);
        }

        private void OnDestroy() => _scrollTween?.Kill();
        
        public void ForceLayoutRebuild() => LayoutRebuilder.ForceRebuildLayoutImmediate(_scrollContent);

        public void UpdateCharacterProgress(CharacterTypeId id, float progress) => _characterViews.Find(x => x.Id == id)?.SetProgress(progress);

        public void SetCharacters(IEnumerable<CharacterView> characterViews)
        {
            _characterViews.AddRange(characterViews);

            SubscribeCharacterSelectedEvent();
        }

        public void RaiseCharacter(CharacterTypeId id)
        {
            _currentCharacter?.PutDown();

            _currentCharacter = _characterViews.FirstOrDefault(x => x.Id == id);

            _currentCharacter?.Raise();
            
            ScrollToCharacter(_currentCharacter);
        }

        private void ScrollToCharacter(CharacterView character)
        {
            if(character == null)
                return;

            Vector3 characterPosition = character.RectTransform.position;
            Vector3 viewportPosition = _scrollRect.viewport.position;
            
            bool isVisible = RectTransformUtility.RectangleContainsScreenPoint(_scrollRect.viewport, characterPosition, null);

            if (!isVisible)
                MoveScrollToSeeItem(characterPosition, viewportPosition);
        }

        private void MoveScrollToSeeItem(Vector3 characterPosition, Vector3 viewportPosition)
        {
            _scrollTween?.Kill();
            
            float targetX = _scrollContent.anchoredPosition.x - (characterPosition.x - viewportPosition.x);

            _scrollTween = DOTween.To(
                    () => _scrollContent.anchoredPosition.x,
                    x => _scrollContent.anchoredPosition = new Vector2(x, _scrollContent.anchoredPosition.y),
                    targetX,
                    _scrollDuration)
                .SetEase(Ease.OutQuad)
                .OnKill(() => _scrollTween = null);
        }

        private void SubscribeCharacterSelectedEvent()
        {
            foreach (CharacterView characterView in _characterViews)
            {
                characterView.OnSelectedButtonClicked?.Subscribe(_ => 
                        _onCharacterSelected?.OnNext(characterView.Id))
                    .AddTo(this);
            }
        }
    }
}