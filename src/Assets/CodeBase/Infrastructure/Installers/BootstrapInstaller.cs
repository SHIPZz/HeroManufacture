using CodeBase.Common.Services.Levels;
using CodeBase.Common.Services.Persistent;
using CodeBase.Common.Services.Raycast;
using CodeBase.Common.Services.SaveLoad;
using CodeBase.Common.Services.Heroes;
using CodeBase.Common.Services.Inputs;
using CodeBase.Gameplay.Heroes.Factory;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Loading;
using CodeBase.Infrastructure.States.Factory;
using CodeBase.Infrastructure.States.StateMachine;
using CodeBase.Infrastructure.States.States;
using CodeBase.StaticData;
using CodeBase.UI.Inventories.Factory;
using CodeBase.UI.Services;
using CodeBase.UI.Services.Window;
using CodeBase.UI.Sound.Services;
using Zenject;
using UnityEngine;

namespace CodeBase.Infrastructure.Installers
{
    public class BootstrapInstaller : MonoInstaller, ICoroutineRunner, IInitializable
    {
        public override void InstallBindings()
        {
            BindInfrastructureServices();
            BindAssetManagementServices();
            BindCommonServices();
            BindGameplayServices();
            BindGameplayFactories();
            BindUIServices();
            BindUIFactories();
            BindStates();

            Container.BindInterfacesAndSelfTo<StateMachine>().AsSingle();
            Container.BindInterfacesTo<LevelProvider>().AsSingle();
        }

        private void BindGameplayFactories()
        {
            Container.Bind<IHeroFactory>().To<HeroFactory>().AsSingle();
        }

        private void BindUIFactories()
        {
            Container.Bind<IInventoryUIFactory>().To<InventoryUIFactory>().AsSingle();
        }

        private void BindGameplayServices()
        {
            Container.BindInterfacesTo<SoundService>().AsSingle();
            Container.BindInterfacesTo<SoundFactory>().AsSingle();
        }

        private void BindUIServices()
        {
            Container.Bind<IWindowService>().To<WindowService>().AsSingle();
            Container.Bind<IUIProvider>().To<UIProvider>().AsSingle();
            Container.BindInterfacesTo<StaticDataService>().AsSingle();
        }

        private void BindStates()
        {
            Container.BindInterfacesAndSelfTo<BootstrapState>().AsSingle();
            Container.BindInterfacesAndSelfTo<LoadingMenuState>().AsSingle();
            Container.BindInterfacesAndSelfTo<MenuState>().AsSingle();
            Container.BindInterfacesAndSelfTo<LoadGameState>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameState>().AsSingle();
        }

        private void BindInfrastructureServices()
        {
            Container.Bind<IStateFactory>().To<StateFactory>().AsSingle();
            Container.BindInterfacesTo<BootstrapInstaller>().FromInstance(this).AsSingle();
        }

        private void BindAssetManagementServices()
        {
            Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();
        }

        private void BindCommonServices()
        {
            Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
            Container.Bind<IPersistentService>().To<PersistentService>().AsSingle();
            
            Container.Bind<IInputService>().To<MobileInputService>().AsSingle();
            #if UNITY_ANDROID || UNITY_IOS
            #else
            //Container.Bind<IInputService>().To<StandaloneInputService>().AsSingle();
            #endif
            
            Container.Bind<ISaveLoadSystem>().To<PlayerPrefsSaveLoadSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<SaveOnApplicationFocusChangedSystem>().AsSingle();
            Container.Bind<IRaycastService>().To<RaycastService>().AsSingle();
            Container.Bind<IHeroProvider>().To<HeroProvider>().AsSingle();
        }

        public void Initialize()
        {
            Container.Resolve<IStateMachine>().Enter<BootstrapState>();
        }
    }
}