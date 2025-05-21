using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace CodeBase.Gameplay.Heroes.ActionComponents
{
    public class HeroMovement : ITickable
    {
        private const float SpeedChangeRate = 5f;

        private readonly HeroAnimator _heroAnimator;
        private readonly NavMeshAgent _navMeshAgent;

        private bool _isMoving;
        private float _currentSpeed;

        public HeroMovement(NavMeshAgent navMeshAgent, HeroAnimator heroAnimator)
        {
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
                SetIdle();
                Debug.Log("Reached destination");
            }
            else if (_navMeshAgent.pathStatus == NavMeshPathStatus.PathInvalid)
            {
                Debug.LogWarning("Invalid path!");
                SetIdle();
            }
        }

        private void SetIdle()
        {
            _navMeshAgent.isStopped = true;
            _isMoving = false;
        }
    }
}