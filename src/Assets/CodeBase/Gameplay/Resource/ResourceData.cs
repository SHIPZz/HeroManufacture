using CodeBase.Gameplay.Items;
using UnityEngine;

namespace CodeBase.Gameplay.Resource
{
    [System.Serializable]
    public class ResourceData
    {
        public ItemTypeId Type;
        public int Amount;
        [Range(0, 1)] public float Chance = 1f;
    }
}