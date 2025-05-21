namespace CodeBase.Gameplay.Heroes.States
{
    public interface IHeroState
    {
        void Enter();
        void Exit();
        void Update();
    }
} 