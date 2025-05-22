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
            
            if (!hasValidInput)
                return; 

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
    }
}