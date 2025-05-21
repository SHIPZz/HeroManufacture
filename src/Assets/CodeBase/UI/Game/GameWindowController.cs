using CodeBase.Infrastructure.States.StateMachine;
using CodeBase.Infrastructure.States.States;
using CodeBase.UI.Controllers;
using CodeBase.UI.Services.Window;
using UniRx;

namespace CodeBase.UI.Game
{
    public class GameWindowController : IController<GameWindow>
    {
        private readonly IWindowService _windowService;
        private readonly IStateMachine _stateMachine;
        private readonly CompositeDisposable _disposables = new();
        
        private GameWindow _window;

        public GameWindowController(IWindowService windowService, IStateMachine stateMachine)
        {
            _windowService = windowService;
            _stateMachine = stateMachine;
        }

        public void Initialize()
        {
            _window.OnMenuClicked
                .Subscribe(_ => OnMenuClicked())
                .AddTo(_disposables);
        }

        public void BindView(GameWindow window) => _window = window;

        public void Dispose() => _disposables.Dispose();

        private void OnMenuClicked()
        {
            _windowService.Close<GameWindow>();
            
            _stateMachine.Enter<LoadingMenuState>();
        }
    }
} 