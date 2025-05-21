using CodeBase.Gameplay.Heroes.States.Factory;
using Zenject;

namespace CodeBase.Gameplay.Heroes.States
{
    public class HeroStateMachine : ITickable
    {
        private readonly IHeroStateFactory _heroStateFactory;
        
        private IHeroState _currentState;

        public HeroStateMachine(IHeroStateFactory heroStateFactory) => _heroStateFactory = heroStateFactory;

        public void Tick() => _currentState?.Update();

        public void Enter<TState>() where TState : IHeroState
        {
            if (_currentState != null && _currentState.GetType() == typeof(TState))
                return;

            _currentState?.Exit();
            _currentState = _heroStateFactory.GetState<TState>();
            _currentState.Enter();
        }
    }
} 