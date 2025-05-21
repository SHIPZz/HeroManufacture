using CodeBase.Common.Services.Heroes;
using CodeBase.Common.Services.Input;
using UnityEngine;
using Zenject;

namespace CodeBase.Common.Services.Cameras
{
    public class PlayerFollowCamera : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private Vector3 _offset = new Vector3(0, 5, -5);
        [SerializeField] private float _maxDistanceFromHero = 10f; 
        
        private IInputService _inputService;
        private IHeroProvider _heroProvider;
        private Transform _cameraTransform;

        [Inject]
        private void Construct(IInputService inputService, IHeroProvider heroProvider)
        {
            _inputService = inputService;
            _heroProvider = heroProvider;
        }

        private void Start()
        {
            _cameraTransform = transform;
        }

        private void LateUpdate()
        {
            if (_heroProvider.CurrentHero == null)
                return;

            HandleMovement();
            UpdateCameraPosition();
        }

        private void HandleMovement()
        {
            float horizontal = _inputService.GetHorizontalAxis();
            float vertical = _inputService.GetVerticalAxis();

            Vector3 moveDirection = new Vector3(horizontal, 0, vertical);
            
            if (moveDirection.magnitude > 0)
            {
                moveDirection.Normalize();
                Vector3 newOffset = _offset + moveDirection * (_moveSpeed * Time.deltaTime);
                
                float distanceFromHero = Vector3.Distance(
                    _heroProvider.CurrentHero.transform.position + newOffset,
                    _heroProvider.CurrentHero.transform.position
                );

                if (distanceFromHero <= _maxDistanceFromHero)
                {
                    _offset = newOffset;
                }
            }
        }

        private void UpdateCameraPosition()
        {
            Vector3 targetPosition = _heroProvider.CurrentHero.transform.position + _offset;
            _cameraTransform.position = targetPosition;
            _cameraTransform.LookAt(_heroProvider.CurrentHero.transform.position);
        }
    }
} 