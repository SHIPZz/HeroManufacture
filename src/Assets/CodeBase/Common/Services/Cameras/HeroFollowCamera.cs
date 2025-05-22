using UnityEngine;
using Zenject;
using CodeBase.Common.Services.Heroes;
using CodeBase.Common.Services.Inputs;
using Unity.Cinemachine;

namespace CodeBase.Common.Services.Cameras
{
    public class HeroFollowCamera : MonoBehaviour
    {
        [SerializeField] private CinemachineCamera _cinemachineCamera;
        [SerializeField] private float _moveSpeed = 10f;
        [SerializeField] private float _minDistanceToHero = 10f;
        [SerializeField] private float _maxDistanceToHero = 20f;
        [SerializeField] private float _returnSpeed = 5f;
        [SerializeField] private float _minHeight = 10f;
        [SerializeField] private float _heightAdjustSpeed = 5f;

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

            Transform heroTransform = _heroProvider.CurrentHero.transform;
            _cinemachineCamera.LookAt = heroTransform;

            Vector3 input = _inputService.GetAxis();
            bool hasValidInput = Mathf.Abs(input.x) > 0.1f || Mathf.Abs(input.z) > 0.1f;

            float currentDistance = Vector3.Distance(_cameraTransform.position, heroTransform.position);

            if (hasValidInput)
            {
                Vector3 moveDirection = _cameraTransform.forward * input.z + _cameraTransform.right * input.x;
                Vector3 desiredMove = moveDirection * _moveSpeed * Time.deltaTime;
                Vector3 newPosition = _cameraTransform.position + desiredMove;

                float newDistance = Vector3.Distance(newPosition, heroTransform.position);

                if (newDistance >= _minDistanceToHero && newDistance <= _maxDistanceToHero)
                {
                    _cameraTransform.position = newPosition;
                }
                else
                {
                    Vector3 horizontalOnlyMove = _cameraTransform.right * input.x * _moveSpeed * Time.deltaTime;
                    _cameraTransform.position += horizontalOnlyMove;
                }
            }
            else if (currentDistance > _maxDistanceToHero || currentDistance < _minDistanceToHero)
            {
                ReturnToHeroSmoothly(heroTransform, currentDistance);
            }
            else
            {
                MaintainMinimumHeight();
            }
        }

        private void MaintainMinimumHeight()
        {
            if (_cameraTransform.position.y < _minHeight)
            {
                Vector3 position = _cameraTransform.position;
                position.y = Mathf.MoveTowards(position.y, _minHeight, _heightAdjustSpeed * Time.deltaTime);
                _cameraTransform.position = position;
            }
        }

        private void ReturnToHeroSmoothly(Transform heroTransform, float currentDistance)
        {
            Vector3 directionToHero = (heroTransform.position - _cameraTransform.position).normalized;
            float targetDistance = currentDistance > _maxDistanceToHero ? _maxDistanceToHero : _minDistanceToHero;
            Vector3 targetPosition = heroTransform.position - directionToHero * targetDistance;

            if (targetPosition.y < _minHeight)
            {
                targetPosition.y = _minHeight;
            }

            _cameraTransform.position =
                Vector3.MoveTowards(_cameraTransform.position, targetPosition, _returnSpeed * Time.deltaTime);
        }
    }
}