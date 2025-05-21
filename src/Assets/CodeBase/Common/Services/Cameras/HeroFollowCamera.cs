using CodeBase.Common.Services.Heroes;
using CodeBase.Common.Services.Input;
using UnityEngine;
using Zenject;

namespace CodeBase.Common.Services.Cameras
{
    public class HeroFollowCamera : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private Vector3 _offset = new Vector3(0, 5, -5);
        [SerializeField] private float _maxDistanceFromHero = 30f;

        private IInputService _inputService;
        private IHeroProvider _heroProvider;
        private Transform _cameraTransform;
        private Vector3 _lastValidOffset;

        [Inject]
        private void Construct(IInputService inputService, IHeroProvider heroProvider)
        {
            _inputService = inputService;
            _heroProvider = heroProvider;
        }

        private void Start()
        {
            _cameraTransform = transform;
            _lastValidOffset = _offset;
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
            float horizontalInput = _inputService.GetHorizontalAxis();
            float verticalInput = _inputService.GetVerticalAxis();

            if (Mathf.Approximately(horizontalInput, 0) && Mathf.Approximately(verticalInput, 0))
                return;

            Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput).normalized;
            Vector3 worldMoveDirection = _cameraTransform.TransformDirection(moveDirection);
            worldMoveDirection.y = 0;

            if (worldMoveDirection.sqrMagnitude > 0.01f)
            {
                worldMoveDirection.Normalize();
                Vector3 offsetChange = worldMoveDirection * (_moveSpeed * Time.deltaTime);
                Vector3 newOffset = _lastValidOffset + offsetChange;

                Vector3 projectedPosition = _heroProvider.CurrentHero.transform.position + newOffset;
                float distanceFromHero =
                    Vector3.Distance(projectedPosition, _heroProvider.CurrentHero.transform.position);

                if (distanceFromHero <= _maxDistanceFromHero)
                {
                    _offset = newOffset;
                    _lastValidOffset = _offset;
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