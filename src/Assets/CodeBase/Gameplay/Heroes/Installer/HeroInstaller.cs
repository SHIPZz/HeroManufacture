using CodeBase.Gameplay.Heroes.ActionComponents;
using CodeBase.Gameplay.Inventories;
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
        [SerializeField] private ResourceCollector _resourceCollector;
        [SerializeField] private Inventory _inventory;
        
        public override void InstallBindings()
        {
            BindAnimator();

            Container.BindInstance(_hero);
            Container.BindInstance(_navMeshAgent);
            
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
            Container.BindInterfacesAndSelfTo<SetHeroMovementDestinationOnClick>().AsSingle();
        }

        private void BindAnimator()
        {
            Container.BindInstance(_animator);
            Container.BindInterfacesAndSelfTo<HeroAnimator>().AsSingle();
        }
    }
} 