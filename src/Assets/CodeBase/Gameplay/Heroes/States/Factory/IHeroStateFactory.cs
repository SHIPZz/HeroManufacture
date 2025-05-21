namespace CodeBase.Gameplay.Heroes.States.Factory
{
    public interface IHeroStateFactory
    {
        T GetState<T>() where T :  IHeroState;
    }
}