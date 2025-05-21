using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Animations
{
    public class SliderAnimator : MonoBehaviour
    {
        [SerializeField] private float _setValueDuration = 0.3f;
        [SerializeField] private Slider _slider;

        private Tween _setValueTween;

        private float _targetValue;

        private void OnDestroy() => _setValueTween?.Kill();

        public void SetValue(float value)
        {
            _targetValue = value;
            
            _setValueTween?.Kill();
            _setValueTween = _slider.DOValue(_targetValue, _setValueDuration);
        }

        public void Reset()
        {
            _targetValue = 0f;
            _slider.value = _targetValue;
            _setValueTween?.Pause();
        }
    }
}