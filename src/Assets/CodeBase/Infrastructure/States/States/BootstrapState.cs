using CodeBase.Common.Services.Persistent;
using CodeBase.Common.Services.SaveLoad;
using CodeBase.Infrastructure.States.StateInfrastructure;
using CodeBase.Infrastructure.States.StateMachine;
using CodeBase.StaticData;
using CodeBase.UI.CharacterSelect.Controllers;
using CodeBase.UI.CharacterSelect.Views;
using CodeBase.UI.Game;
using CodeBase.UI.LoadingCurtains;
using CodeBase.UI.Menu;
using CodeBase.UI.Services.Window;
using CodeBase.UI.Settings;

namespace CodeBase.Infrastructure.States.States
{
    public class BootstrapState : IState
    {
        private readonly IStateMachine _stateMachine;
        private readonly IWindowService _windowService;
        private readonly IPersistentService _persistentService;
        private readonly SaveOnApplicationFocusChangedSystem _saveOnApplicationPauseSystem;
        private readonly IStaticDataService _staticDataService;

        public BootstrapState(IStateMachine stateMachine,
            IWindowService windowService,
            IPersistentService persistentService,
            SaveOnApplicationFocusChangedSystem saveOnApplicationPauseSystem,
            IStaticDataService staticDataService)
        {
            _windowService = windowService;
            _persistentService = persistentService;
            _saveOnApplicationPauseSystem = saveOnApplicationPauseSystem;
            _stateMachine = stateMachine;
            _staticDataService = staticDataService;
        }

        public void Enter()
        {
            LoadData();
            
            LoadStaticData();

            BindWindows();
            
            _saveOnApplicationPauseSystem.Initialize();

            _windowService.OpenWindow<LoadingCurtainWindow>(onTop: true, onOpened: () => _stateMachine.Enter<LoadingMenuState>());
        }

        private void LoadData() => _persistentService.LoadAll();

        private void LoadStaticData() => _staticDataService.LoadAll();

        private void BindWindows()
        {
            _windowService.Bind<MenuWindow, MenuWindowController>();
            _windowService.Bind<LoadingCurtainWindow, LoadingCurtainWindowController>();
            _windowService.Bind<SettingsWindow, SettingsWindowController>();
            _windowService.Bind<CharacterSelectWindow, CharacterSelectWindowController>();
            _windowService.Bind<CharacterPanelView, CharacterPanelController>();
            _windowService.Bind<GameWindow, GameWindowController>();
            _windowService.Bind<CharacterProgressCheatWindow, CharacterProgressCheatController>();
        }

        public void Exit() { }
    }
}