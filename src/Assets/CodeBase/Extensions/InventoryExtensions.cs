using CodeBase.Gameplay.Inventories;

namespace CodeBase.Extensions
{
    public static class InventoryExtensions
    {
        public static InventoryWindowModel ToWindowModel(this Inventory inventory)
        {
            return new InventoryWindowModel(
                inventory.Resources,
                inventory.Title,
                inventory.IsFull,
                inventory.MaxSlots,
                inventory.CurrentSlots);
        }
    }
}