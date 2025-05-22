using CodeBase.Extensions;
using CodeBase.UI.Inventories.Views;
using CodeBase.UI.Services.Window;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Inventories
{
    public class HeroInventoryController : MonoBehaviour
    {
        private const KeyCode INVENTORY_KEY = KeyCode.F;

        [SerializeField] private InventoryHolder _inventoryHolder;

        private IWindowService _windowService;

        [Inject]
        private void Construct(IWindowService windowService) => _windowService = windowService;

        private void Update()
        {
            if (!Input.GetKeyDown(INVENTORY_KEY))
                return;

            if (_windowService.IsWindowOpen<InventoryWindow>())
                _windowService.Close<InventoryWindow>();
            else
                OpenInventory();
        }

        public void OpenInventory()
        {
            Inventory inventory = _inventoryHolder.Inventory;
            var windowModel = inventory.ToWindowModel();

            _windowService.OpenWindow<InventoryWindow, InventoryWindowModel>(windowModel, onTop: true);
        }
    }
}