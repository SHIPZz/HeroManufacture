using CodeBase.Gameplay.Heroes.States;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Heroes
{
    public class Hero : MonoBehaviour
    {
        private HeroStateMachine _stateMachine;

        [Inject]
        private void Construct(HeroStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        private void Start()
        {
            _stateMachine.Enter<HeroIdleState>();
        }
    }
}