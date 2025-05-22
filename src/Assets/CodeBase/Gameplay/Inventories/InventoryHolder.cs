using UnityEngine;

namespace CodeBase.Gameplay.Inventories
{
    public class InventoryHolder : MonoBehaviour
    {
        [SerializeField] private int _maxSlots = 20;
        [SerializeField] private string _inventoryName = "";
        
        private Inventory _inventory;

        public Inventory Inventory => _inventory;

        private void Awake()
        {
            _inventory = new Inventory(_inventoryName, _maxSlots);
        }

        private void OnDestroy()
        {
            _inventory.Dispose();
        }
    }
} 