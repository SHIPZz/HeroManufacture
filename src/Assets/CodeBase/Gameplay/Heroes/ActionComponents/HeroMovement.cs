using CodeBase.Gameplay.Heroes.Configs;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace CodeBase.Gameplay.Heroes.ActionComponents
{
    public class HeroMovement : ITickable
    {
        private const float SpeedChangeRate = 5f;
        
        private readonly Hero _hero;
        private readonly HeroAnimator _heroAnimator;
        private readonly NavMeshAgent _navMeshAgent;

        private bool _isMoving;
        private float _currentSpeed;

        public bool IsMoving => _isMoving;

        public HeroMovement(
             Hero hero,
             NavMeshAgent navMeshAgent,
             HeroAnimator heroAnimator)
        {
            _hero = hero;
            _heroAnimator = heroAnimator;
            _navMeshAgent = navMeshAgent;
        }
        public void SetDestination(Vector3 position)
        {
            if (!_navMeshAgent.isOnNavMesh)
            {
                Debug.LogError("NavMeshAgent is not on NavMesh!");
                return;
            }

            _navMeshAgent.isStopped = false;
            _navMeshAgent.SetDestination(position);
            _isMoving = true;
            
            Debug.Log($"Setting destination to: {position}, Current position: {_hero.transform.position}");
        }

        public void SetIdle()
        {
            _navMeshAgent.isStopped = true;
            _isMoving = false;
        }

        public void Tick()
        {
            float targetSpeed = _isMoving ? 1f : 0f;
            _currentSpeed = Mathf.Lerp(_currentSpeed, targetSpeed, SpeedChangeRate * Time.deltaTime);
            _heroAnimator.SetSpeed(_currentSpeed);

            if (!_isMoving)
                return;

            if (!_navMeshAgent.pathPending && _navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
            {
                _isMoving = false;
                Debug.Log("Reached destination");
            }
            else if (_navMeshAgent.pathStatus == NavMeshPathStatus.PathInvalid)
            {
                Debug.LogWarning("Invalid path!");
                _isMoving = false;
            }
        }
    }
}