using CodeBase.Gameplay.Heroes.ActionComponents;
using Zenject;

namespace CodeBase.Gameplay.Heroes.States
{
    public class HeroMovingState : IHeroState
    {
        private readonly HeroStateMachine _stateMachine;
        private readonly HeroAnimator _heroAnimator;
        private readonly HeroMovement _heroMovement;

        public HeroMovingState(
            HeroMovement heroMovement,
            HeroAnimator heroAnimator,
            HeroStateMachine stateMachine)
        {
            _heroMovement = heroMovement;
            _heroAnimator = heroAnimator;
            _stateMachine = stateMachine;
        }

        public void Enter()
        {
            // _heroAnimator.SetMoving();
        }

        public void Exit()
        {
        }

        public void Update()
        {
            if (_heroMovement.IsMoving)
                return;

            _stateMachine.Enter<HeroIdleState>();
        }
    }
} 