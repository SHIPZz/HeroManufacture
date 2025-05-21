using CodeBase.Gameplay.Heroes.ActionComponents;
using CodeBase.Gameplay.Heroes.States;
using CodeBase.Gameplay.Heroes.States.Factory;
using CodeBase.Gameplay.Resource;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace CodeBase.Gameplay.Heroes.Installer
{
    public class HeroInstaller : MonoInstaller<HeroInstaller>
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Hero _hero;
        [SerializeField] private NavMeshAgent _navMeshAgent;
        
        public override void InstallBindings()
        {
            BindAnimator();

            Container.BindInterfacesTo<HeroStateFactory>().AsSingle();

            Container.BindInstance(_hero);
            Container.BindInstance(_navMeshAgent);
            
            BindStates();
            HeroActions();
            BindResourceComponents();
        }

        private void BindResourceComponents()
        {
            Container.BindInterfacesAndSelfTo<ResourceCollector>().AsSingle();
        }

        private void HeroActions()
        {
            Container.BindInterfacesAndSelfTo<HeroMovement>().AsSingle();
            Container.BindInterfacesAndSelfTo<HeroInputHandler>().AsSingle();
        }

        private void BindAnimator()
        {
            Container.BindInstance(_animator);
            Container.BindInterfacesAndSelfTo<HeroAnimator>().AsSingle();
        }

        private void BindStates()
        {
            Container.BindInterfacesAndSelfTo<HeroStateMachine>().AsSingle();
            Container.BindInterfacesAndSelfTo<HeroIdleState>().AsSingle();
            Container.BindInterfacesAndSelfTo<HeroMovingState>().AsSingle();
            Container.BindInterfacesAndSelfTo<HeroCollectingState>().AsSingle();
        }
    }
} 