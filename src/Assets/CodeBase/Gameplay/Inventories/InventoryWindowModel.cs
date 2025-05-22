using CodeBase.Gameplay.Items;
using CodeBase.UI.AbstractWindow;
using UniRx;

namespace CodeBase.Gameplay.Inventories
{
    public readonly struct InventoryWindowModel : IWindowModel
    {
        public IReadOnlyReactiveDictionary<ItemTypeId, int> Resources { get; }
        public IReadOnlyReactiveProperty<string> Title { get; }
        public IReadOnlyReactiveProperty<bool> IsFull { get; }
        public int MaxSlots { get; }
        public int CurrentSlots { get; }

        public InventoryWindowModel(
            IReadOnlyReactiveDictionary<ItemTypeId, int> resources,
            IReadOnlyReactiveProperty<string> title,
            IReadOnlyReactiveProperty<bool> isFull,
            int maxSlots,
            int currentSlots)
        {
            Resources = resources;
            Title = title;
            IsFull = isFull;
            MaxSlots = maxSlots;
            CurrentSlots = currentSlots;
        }
    }
} 