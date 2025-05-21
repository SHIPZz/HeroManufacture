using CodeBase.Gameplay.Heroes.Configs;
using CodeBase.Gameplay.Items.Configs;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Installers
{
    public class ConfigInstaller : MonoInstaller
    {
       [SerializeField] private HeroConfig _heroConfig;
       [SerializeField] private ItemConfig _itemConfig;

        public override void InstallBindings()
        {
            Container.BindInstance(_heroConfig);
            Container.BindInstance(_itemConfig);
        }
    }
}