using System;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.Animations
{
    public class ScaleAnimator : MonoBehaviour
    {
        [SerializeField] private float _duration = 0.5f;
        [SerializeField] private Ease _ease = Ease.OutQuad;
        [SerializeField] private Transform _targetTransform;
        
        private Tween _currentTween;

        public void AnimateTo(Vector3 targetScale)
        {
            StopCurrentAnimation(true);
            
            _currentTween = _targetTransform.DOScale(targetScale, _duration)
                .SetEase(_ease);
        }

        public void OnAnimationComplete(Action onComplete) => _currentTween?.OnComplete(() => onComplete?.Invoke());

        public void AnimateToZero() => AnimateTo(Vector3.zero);

        public void AnimateToOne() => AnimateTo(Vector3.one);

        public void StopCurrentAnimation(bool complete = false)
        {
            _currentTween?.Kill(complete);
            _currentTween = null;
        }

        private void OnDestroy() => StopCurrentAnimation();
    }
} 