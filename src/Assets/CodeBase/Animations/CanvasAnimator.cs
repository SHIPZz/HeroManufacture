using System;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.Animations
{
    [RequireComponent(typeof(CanvasGroup), typeof(Canvas))]
    public class CanvasAnimator : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Canvas _canvas;
        [SerializeField] private float _fadeInDuration = 0.3f;
        [SerializeField] private float _fadeOutDuration = 0.3f;
        [SerializeField] private Ease _easeType = Ease.InOutQuad;

        private Tweener _fadeInTween;
        private Tweener _fadeOutTween;
        private float _initialAlpha;

        private void Awake()
        {
            _initialAlpha = _canvasGroup.alpha;
            _canvasGroup.alpha = 0;
            _canvas.enabled = false;
            
            CreateTweens();
        }

        private void OnDestroy()
        {
            _fadeInTween?.Kill();
            _fadeOutTween?.Kill();
        }

        public void Show(Action onComplete = null)
        {
            _canvas.enabled = true;
            _fadeOutTween.Pause();
            _fadeInTween.OnComplete(() => onComplete?.Invoke()).Restart();
        }

        public void Hide(Action onComplete = null)
        {
            _fadeInTween.Pause();
            _fadeOutTween.OnComplete(() => onComplete?.Invoke()).Restart();
        }

        private void CreateTweens()
        {
            _fadeInTween = _canvasGroup
                .DOFade(_initialAlpha, _fadeInDuration)
                .SetEase(_easeType)
                .SetAutoKill(false)
                .Pause()
                .OnKill(() => _fadeInTween = null);

            _fadeOutTween = _canvasGroup
                .DOFade(0, _fadeOutDuration)
                .SetEase(_easeType)
                .SetAutoKill(false)
                .Pause()
                .OnComplete(() => _canvas.enabled = false)
                .OnKill(() => _fadeOutTween = null);
        }
    }
} 