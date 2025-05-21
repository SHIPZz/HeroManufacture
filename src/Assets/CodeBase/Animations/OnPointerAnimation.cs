using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CodeBase.Animations
{
    [RequireComponent(typeof(RectTransform))]
    public class OnPointerAnimation : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private Vector2 _targetAnchoredScale = new Vector2(0.8f, 0.8f);
        [SerializeField] private float _animationDuration = 0.2f;

        private RectTransform _targetRectTransform;
        private Tween _tween;
        private Vector2 _initialAnchoredScale;

        private void Awake()
        {
            _targetRectTransform = GetComponent<RectTransform>();
            _initialAnchoredScale = _targetRectTransform.localScale;
        }

        private void OnDestroy() => _tween?.Kill();

        public void OnPointerDown(PointerEventData eventData)
        {
            _tween?.Kill(true);
            _tween = _targetRectTransform.DOScale(_targetAnchoredScale, _animationDuration).SetUpdate(true);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _tween?.Kill(true);
            _tween = _targetRectTransform.DOScale(_initialAnchoredScale, _animationDuration).SetUpdate(true);
        }
    }
}