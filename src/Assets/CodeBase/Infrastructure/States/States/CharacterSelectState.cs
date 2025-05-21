using CodeBase.Infrastructure.States.StateInfrastructure;
using CodeBase.UI.CharacterSelect;
using CodeBase.UI.CharacterSelect.Views;
using CodeBase.UI.Services.Window;
using UnityEngine;

namespace CodeBase.Infrastructure.States.States
{
    public class CharacterSelectState : IState, IUpdateable
    {
        private readonly IWindowService _windowService;

        public CharacterSelectState(IWindowService windowService)
        {
            _windowService = windowService;
        }

        public void Update()
        {
            if (!CheatWindowOpeningRequested())
                return;

            if (!_windowService.IsWindowOpen<CharacterProgressCheatWindow>())
                _windowService.OpenWindow<CharacterProgressCheatWindow>();
            else
                _windowService.Close<CharacterProgressCheatWindow>();
        }

        public void Enter()
        {
            _windowService.OpenWindow<CharacterSelectWindow>();
        }

        public void Exit()
        {
            _windowService.Close<CharacterSelectWindow>();
            _windowService.Close<CharacterProgressCheatWindow>();
        }

        private static bool CheatWindowOpeningRequested()
        {
            return Input.GetKeyDown(KeyCode.F);
        }
    }
}