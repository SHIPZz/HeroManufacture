using System.Collections.Generic;
using CodeBase.Gameplay.Items;
using CodeBase.Gameplay.Resource;
using UniRx;
using UnityEngine;

namespace CodeBase.Gameplay.Inventories
{
    public class InventoryResourceCollector : MonoBehaviour
    {
        [SerializeField] private ResourceCollector _resourceCollector;
        [SerializeField] private InventoryHolder _inventoryHolder;
        
        private void Start()
        {
            _resourceCollector.OnResourcesCollected
                .Subscribe(AddResourcesToInventory)
                .AddTo(this);
        }

        private void AddResourcesToInventory(Dictionary<ItemTypeId, int> resources)
        {
            foreach (var resource in resources)
            {
                _inventoryHolder.Inventory.TryAddResource(resource.Key, resource.Value);
            }
        }
    }
}