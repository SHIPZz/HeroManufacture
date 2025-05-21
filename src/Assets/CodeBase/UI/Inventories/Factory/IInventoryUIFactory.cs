using CodeBase.Gameplay.Items;
using CodeBase.UI.Inventories.Views;
using UnityEngine;

namespace CodeBase.UI.Inventories.Factory
{
    public interface IInventoryUIFactory
    {
        InventoryItemView Create(Transform parent, ItemTypeId typeId, int amount);
    }
} 