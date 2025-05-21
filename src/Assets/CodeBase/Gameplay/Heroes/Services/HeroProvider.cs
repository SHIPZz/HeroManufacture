using CodeBase.Gameplay.Heroes;
using UnityEngine;

namespace CodeBase.Gameplay.Heroes.Services
{
    public interface IHeroProvider
    {
        Hero Hero { get; }
        void SetHero(Hero hero);
        void ClearHero();
    }

    public class HeroProvider : IHeroProvider
    {
        private Hero _hero;

        public Hero Hero => _hero;

        public void SetHero(Hero hero)
        {
            _hero = hero;
        }

        public void ClearHero()
        {
            _hero = null;
        }
    }
} 