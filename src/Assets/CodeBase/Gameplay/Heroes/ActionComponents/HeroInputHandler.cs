using UnityEngine;
using UnityEngine.AI;
using Zenject;
using CodeBase.Common.Services.Input;
using CodeBase.Gameplay.Heroes.Configs;
using CodeBase.Gameplay.Heroes.States;

namespace CodeBase.Gameplay.Heroes.ActionComponents
{
    public class HeroInputHandler : ITickable
    {
        private readonly IInputService _inputService;
        private readonly HeroMovement _heroMovement;
        private readonly HeroStateMachine _stateMachine;
        private readonly Camera _mainCamera;
        private readonly LayerMask _mask;
        private readonly NavMeshAgent _navMeshAgent;
        private Vector3 _position;
        private const float MaxDistance = 1f;

        public HeroInputHandler(
            IInputService inputService,
            HeroMovement heroMovement,
            HeroStateMachine stateMachine,
            HeroConfig heroConfig,
            NavMeshAgent navMeshAgent)
        {
            _inputService = inputService;
            _heroMovement = heroMovement;
            _stateMachine = stateMachine;
            _mainCamera = _inputService.CameraMain;
            _mask = heroConfig.Mask;
            _navMeshAgent = navMeshAgent;
        }

        public void Tick()
        {
            if (TryGetClickPosition(out Vector3 position))
            {
                _position = position;
                _heroMovement.SetDestination(_position);
                _stateMachine.Enter<HeroMovingState>();
            }
        }

        private bool TryGetClickPosition(out Vector3 position)
        {
            position = Vector3.zero;

            if (!_inputService.GetLeftMouseButtonDown())
                return false;

            Ray ray = _mainCamera.ScreenPointToRay(_inputService.GetScreenMousePosition());

            if (!Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _mask)) 
                return false;
            
            if (NavMesh.SamplePosition(hit.point, out NavMeshHit navHit, MaxDistance, NavMesh.AllAreas))
            {
                Debug.Log($"Found walkable position near: {hit.point} at: {navHit.position}");
                position = navHit.position;
                return true;
            }

            Debug.Log($"No walkable position found near: {hit.point}");
            return false;

        }
    }
}