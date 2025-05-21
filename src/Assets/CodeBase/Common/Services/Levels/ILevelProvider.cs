using UnityEngine;

namespace CodeBase.Common.Services.Levels
{
    public interface ILevelProvider
    {
        Transform HeroSpawnPoint { get; set; }
    }
}