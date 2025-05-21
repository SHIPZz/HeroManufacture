using UnityEngine;

namespace CodeBase.Gameplay.Heroes.Factory
{
    public interface IHeroFactory
    {
        Hero Create(Transform parent, Vector3 at, Quaternion rotation);
    }
}