using UnityEngine;

namespace CodeBase.Gameplay.Resource
{
    public class Cooldown : MonoBehaviour
    {
        [SerializeField] private float _cooldownTime = 5f;
        
        private float _currentCooldown;
        private bool _isReady = true;

        public bool IsReady => _isReady;

        private void Update()
        {
            if (!_isReady)
            {
                _currentCooldown -= Time.deltaTime;
                
                if (_currentCooldown <= 0)
                {
                    _isReady = true;
                }
            }
        }

        public void StartCooldown()
        {
            _isReady = false;
            _currentCooldown = _cooldownTime;
        }

        private void OnValidate()
        {
            _cooldownTime = Mathf.Max(0.1f, _cooldownTime);
        }
    }
} 