using CodeBase.Gameplay.Heroes.ActionComponents;
using Zenject;

namespace CodeBase.Gameplay.Heroes.States
{
    public class HeroCollectingState : IHeroState
    {
        private readonly HeroStateMachine _stateMachine;
        private readonly HeroAnimator _heroAnimator;
        private readonly HeroMovement _heroMovement;

        [Inject]
        public HeroCollectingState(
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
            // _heroAnimator.SetCollecting();
        }

        public void Exit()
        {
        }

        public void Update()
        {
            if (!_heroAnimator.IsCollectingAnimationPlaying())
            {
                _stateMachine.Enter<HeroIdleState>();
            }
        }
    }
} 