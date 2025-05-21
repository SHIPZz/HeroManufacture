using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace CodeBase.Gameplay.Resource
{
    public class ResourceGenerator : MonoBehaviour
    {
        [SerializeField] private List<ResourceData> _resources = new List<ResourceData>();
        [SerializeField] private Cooldown _cooldown;

        public bool IsResourceReady => _cooldown.IsReady;

        public Dictionary<ResourceType, int> CollectResource()
        {
            if (!IsResourceReady)
                return null;

            _cooldown.StartCooldown();
            
            var collectedResources = DictionaryPool<ResourceType, int>.Get();
            
            foreach (var resource in _resources)
            {
                if (Random.value <= resource.Chance)
                {
                    collectedResources[resource.Type] = resource.Amount;
                }
            }

            return collectedResources;
        }

        private void OnValidate()
        {
            foreach (var resource in _resources)
            {
                resource.Amount = Mathf.Max(1, resource.Amount);
            }
        }
    }
} 