using UnityEngine;

namespace CodeBase.Common.Services.Levels
{
    public class LevelProvider : ILevelProvider
    {
        public Transform HeroSpawnPoint { get;  set; }
    }
}