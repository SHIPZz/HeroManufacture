using CodeBase.Common.Services.Levels;
using CodeBase.Common.Services.Heroes;
using CodeBase.Gameplay.Heroes;
using CodeBase.Gameplay.Heroes.Factory;
using CodeBase.Infrastructure.States.StateInfrastructure;
using CodeBase.UI.Game;
using CodeBase.UI.InputWindows;
using CodeBase.UI.Services.Window;
using UnityEngine;

namespace CodeBase.Infrastructure.States.States
{
    public class GameState : IState
    {
        private readonly IWindowService _windowService;
        private readonly IHeroFactory _heroFactory;
        private readonly ILevelProvider _levelProvider;
        private readonly IHeroProvider _heroProvider;
        private Hero _hero;

        public GameState(
            IWindowService windowService, 
            IHeroFactory heroFactory, 
            ILevelProvider levelProvider,
            IHeroProvider heroProvider)
        {
            _levelProvider = levelProvider;
            _heroFactory = heroFactory;
            _windowService = windowService;
            _heroProvider = heroProvider;
        }

        public void Enter()
        {
#if !UNITY_EDITOR  && (UNITY_ANDROID || UNITY_IOS)
            _windowService.OpenWindow<InputWindow>(onTop: true);
#endif
            
            _windowService.OpenWindow<GameWindow>();

            _hero = _heroFactory.Create(null, _levelProvider.HeroSpawnPoint.position, Quaternion.identity);
            _heroProvider.SetHero(_hero);
        }

        public void Exit()
        {
            _windowService.Close<GameWindow>();
            _windowService.Close<InputWindow>();
            
            _heroProvider.SetHero(null);
            Object.Destroy(_hero.gameObject);
        }
    }
}