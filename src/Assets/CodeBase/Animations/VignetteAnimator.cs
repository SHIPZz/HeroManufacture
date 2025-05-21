using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace CodeBase.Animations
{
    public class VignetteAnimator : MonoBehaviour
    {
        [SerializeField] private Volume _volume;
        [SerializeField] private float _targetIntensity = 0.4f;
        [SerializeField] private float _inDuration = 0.1f;
        [SerializeField] private float _outDuration = 0.5f;

        private Vignette _vignette;
        private Sequence _sequence;

        private void Awake()
        {
            if (_volume == null || !_volume.profile.TryGet(out _vignette))
            {
                Debug.LogError("Vignette component not found in volume profile!", this);
                return;
            }

            _vignette.intensity.value = 0f;

            _sequence = DOTween.Sequence()
                .Append(DOTween.To(() => _vignette.intensity.value, x => _vignette.intensity.value = x, _targetIntensity, _inDuration))
                .Append(DOTween.To(() => _vignette.intensity.value, x => _vignette.intensity.value = x, 0f, _outDuration))
                .Pause()
                .SetAutoKill(false);
        }

        public void Play()
        {
            _sequence?.Restart();
        }

        private void OnDestroy()
        {
            _sequence?.Kill();
        }
    }
}