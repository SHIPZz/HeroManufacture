using CodeBase.Infrastructure.Loading;
using CodeBase.Infrastructure.States.StateInfrastructure;
using CodeBase.Infrastructure.States.StateMachine;

namespace CodeBase.Infrastructure.States.States
{
    public class LoadingCharacterSelectState : IState
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly IStateMachine _stateMachine;

        public LoadingCharacterSelectState(ISceneLoader sceneLoader, IStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            _sceneLoader.LoadScene(Scenes.CharacterSelect, () => _stateMachine.Enter<CharacterSelectState>());
        }

        public void Exit()
        {
        }
    }
}