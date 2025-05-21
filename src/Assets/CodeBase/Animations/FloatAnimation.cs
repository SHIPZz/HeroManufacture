using System;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.Animations
{
    public class FloatAnimation : MonoBehaviour
    {
        [SerializeField] private float _moveHeight = 1f;
        [SerializeField] private float _duration = 1f;

        private Tweener _floatTween;
        private Vector3 _startPosition;

        private void Awake()
        {
            _startPosition = transform.position;

            _floatTween = transform
                .DOMoveY(_startPosition.y + _moveHeight, _duration)
                .SetEase(Ease.InOutSine)
                .SetLoops(-1, LoopType.Yoyo)
                .Pause()
                .OnKill(() => _floatTween = null);
        }

        public void StartFloatAnimation()
        {
            _floatTween?.Restart();
        }

        public void StopFloatAnimation()
        {
            _floatTween?.Pause();
            transform.position = _startPosition;
        }

        private void OnDestroy()
        {
            _floatTween?.Kill();
        }
    }
}