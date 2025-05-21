using UniRx;
using UnityEngine;

namespace CodeBase.Gameplay.Cooldowns
{
    public class Cooldown : MonoBehaviour
    {
        [SerializeField] private float _cooldownTime = 5f;
        
        private float _currentCooldown;
        
        private ReactiveProperty<bool> _isReady = new(true);

        public IReactiveProperty<bool> IsReady => _isReady;

        private void Update()
        {
            if (!_isReady.Value)
            {
                _currentCooldown -= Time.deltaTime;
                
                if (_currentCooldown <= 0)
                {
                    _isReady.Value = true;
                }
            }
        }

        public void StartCooldown()
        {
            _isReady.Value = false;
            _currentCooldown = _cooldownTime;
        }

        private void OnValidate()
        {
            _cooldownTime = Mathf.Max(0.1f, _cooldownTime);
        }
    }
} 