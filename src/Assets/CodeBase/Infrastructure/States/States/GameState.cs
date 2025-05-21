using CodeBase.Common.Services.Levels;
using CodeBase.Gameplay.Heroes.Factory;
using CodeBase.Infrastructure.States.StateInfrastructure;
using CodeBase.UI.Game;
using CodeBase.UI.Services.Window;
using UnityEngine;

namespace CodeBase.Infrastructure.States.States
{
    public class GameState : IState
    {
        private readonly IWindowService _windowService;
        private readonly IHeroFactory _heroFactory;
        private readonly ILevelProvider _levelProvider;

        public GameState(IWindowService windowService, IHeroFactory heroFactory, ILevelProvider levelProvider)
        {
            _levelProvider = levelProvider;
            _heroFactory = heroFactory;
            _windowService = windowService;
        }

        public void Enter()
        {
            _windowService.OpenWindow<GameWindow>();

            _heroFactory.Create(null, _levelProvider.HeroSpawnPoint.position, Quaternion.identity);
        }

        public void Exit()
        {
        }
    }
}