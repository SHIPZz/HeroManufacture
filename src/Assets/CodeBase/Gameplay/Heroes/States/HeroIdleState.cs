using CodeBase.Common.Services.Input;
using CodeBase.Gameplay.Heroes.ActionComponents;
using Zenject;

namespace CodeBase.Gameplay.Heroes.States
{
    public class HeroIdleState : IHeroState
    {
        private readonly HeroStateMachine _stateMachine;
        private readonly HeroAnimator _heroAnimator;
        private readonly HeroMovement _heroMovement;

        [Inject]
        public HeroIdleState(
            HeroMovement heroMovement,
            IInputService inputService,
            HeroStateMachine stateMachine)
        {
            _heroMovement = heroMovement;
            _stateMachine = stateMachine;
        }

        public void Enter()
        {
            _heroMovement.SetIdle();
        }

        public void Exit()
        {
        }

        public void Update()
        {
        }
    }
} 