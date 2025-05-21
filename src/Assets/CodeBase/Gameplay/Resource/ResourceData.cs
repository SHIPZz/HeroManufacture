using UnityEngine;

namespace CodeBase.Gameplay.Resource
{
    [System.Serializable]
    public class ResourceData
    {
        public ResourceType Type;
        public int Amount;
        [Range(0, 1)] public float Chance = 1f;
    }
}