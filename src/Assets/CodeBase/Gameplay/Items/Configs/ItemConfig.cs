using System.Collections.Generic;
using CodeBase.Gameplay.Items.Data;
using CodeBase.UI.Inventories.Views;
using UnityEngine;

namespace CodeBase.Gameplay.Items.Configs
{
    [CreateAssetMenu(fileName = "ItemConfig", menuName = "Configs/Items/ItemConfig", order = 1)]
    public class ItemConfig : ScriptableObject
    {
        public List<ItemData> Items = new();

        public InventoryItemView InventoryItemViewPrefab;
    }
} 