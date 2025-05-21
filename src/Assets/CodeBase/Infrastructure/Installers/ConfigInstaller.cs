using CodeBase.Gameplay.Heroes.Configs;
using CodeBase.UI.CharacterSelect.Configs;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Installers
{
    public class ConfigInstaller : MonoInstaller
    {
        [SerializeField] private CharacterConfig _characterConfig;
       [SerializeField] private HeroConfig _heroConfig;

        public override void InstallBindings()
        {
            Container.BindInstance(_heroConfig);
            Container.BindInstance(_characterConfig);
        }
    }
}