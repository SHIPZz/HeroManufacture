using System.Linq;
using CodeBase.Animations;
using CodeBase.Extensions;
using CodeBase.Gameplay.Items;
using CodeBase.Gameplay.Items.Configs;
using CodeBase.Gameplay.Items.Data;
using CodeBase.UI.Inventories.Views;
using UnityEngine;
using Zenject;

namespace CodeBase.UI.Inventories.Factory
{
    public class InventoryUIFactory : IInventoryUIFactory
    {
        private readonly IInstantiator _instantiator;
        private readonly ItemConfig _itemConfig;

        public InventoryUIFactory(IInstantiator instantiator, ItemConfig itemConfig)
        {
            _instantiator = instantiator;
            _itemConfig = itemConfig;
        }

        public InventoryItemView Create(Transform parent, ItemTypeId typeId, int amount)
        {
            ItemData itemData = _itemConfig.Items.FirstOrDefault(x => x.TypeId == typeId);
            
            if (itemData == null)
                throw new System.ArgumentException($"Item with type {typeId} not found in config");

            var itemView = _instantiator.InstantiatePrefabForComponent<InventoryItemView>(_itemConfig.InventoryItemViewPrefab, parent);

            itemView.Initialize(typeId, amount, itemData.Icon);
            itemView.Appear();
            
            return itemView;
        }
    }
} 