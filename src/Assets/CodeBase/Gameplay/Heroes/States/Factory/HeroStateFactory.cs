using Zenject;

namespace CodeBase.Gameplay.Heroes.States.Factory
{
    public class HeroStateFactory : IHeroStateFactory
    {
        private readonly DiContainer _container;

        public HeroStateFactory(DiContainer container)
        {
            _container = container;
        }

        public T GetState<T>() where T : IHeroState
        {
            return _container.Resolve<T>();
        }
    }
}