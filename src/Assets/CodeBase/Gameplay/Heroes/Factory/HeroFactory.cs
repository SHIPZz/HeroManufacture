using CodeBase.Gameplay.Heroes.Configs;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Heroes.Factory
{
    public class HeroFactory : IHeroFactory
    {
        private readonly IInstantiator _instantiator;
        private readonly HeroConfig _heroConfig;

        public HeroFactory(IInstantiator instantiator, HeroConfig heroConfig)
        {
            _heroConfig = heroConfig;
            _instantiator = instantiator;
        }
        
        public Hero Create(Transform parent, Vector3 at, Quaternion rotation)
        {
            return _instantiator.InstantiatePrefabForComponent<Hero>(_heroConfig.Prefab, at, rotation, parent);
        }
    }
}