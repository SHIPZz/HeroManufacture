using CodeBase.Gameplay.Heroes;
using CodeBase.Gameplay.Inventories;
using UnityEngine;
using Zenject;

namespace CodeBase.Common.Services.Heroes
{
    public class HeroProvider : IHeroProvider
    {
        private Hero _currentHero;
        private HeroInventoryController _heroInventory;

        public Hero CurrentHero => _currentHero;
        
        public HeroInventoryController HeroInventory => _heroInventory;

        public void SetHero(Hero hero)
        {
            _currentHero = hero;
            _heroInventory = hero.GetComponent<HeroInventoryController>();
        }
    }
} 