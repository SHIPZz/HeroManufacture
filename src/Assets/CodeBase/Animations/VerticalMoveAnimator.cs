using DG.Tweening;
using UnityEngine;

namespace CodeBase.Animations
{
    public class VerticalMoveAnimator : MonoBehaviour
    {
        [SerializeField] private float _raiseAnimationDuration = 0.3f;
        [SerializeField] private float _downAnimationDuration = 0.3f;
        [SerializeField] private Ease _ease = Ease.InOutQuad;
        [SerializeField] private float _highlightOffset = 50f;
        [SerializeField] private Transform _targetTransform;

        private Vector3 _initialPosition;
        private Tweener _tween;

        private void Awake() => _initialPosition = _targetTransform.localPosition;

        public void Raise()
        {
            _tween?.Kill();

            _tween = _targetTransform
                .DOLocalMoveY(_initialPosition.y + _highlightOffset, _raiseAnimationDuration)
                .SetEase(_ease);
        }

        public void Down()
        {
            _tween?.Kill();

            _tween = _targetTransform
                .DOLocalMoveY(_initialPosition.y, _downAnimationDuration)
                .SetEase(_ease);
        }

        private void OnDestroy() => _tween?.Kill();
    }
}