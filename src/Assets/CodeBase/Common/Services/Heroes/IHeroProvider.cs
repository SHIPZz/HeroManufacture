using CodeBase.Gameplay.Heroes;
using CodeBase.Gameplay.Inventories;

namespace CodeBase.Common.Services.Heroes
{
    public interface IHeroProvider
    {
        Hero CurrentHero { get; }
        HeroInventoryController HeroInventory { get; }
        void SetHero(Hero hero);
    }
}