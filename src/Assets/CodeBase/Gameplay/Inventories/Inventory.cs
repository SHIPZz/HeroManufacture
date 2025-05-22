using System;
using System.Collections.Generic;
using CodeBase.Gameplay.Items;
using CodeBase.Gameplay.Resource;
using CodeBase.UI.AbstractWindow;
using UniRx;

namespace CodeBase.Gameplay.Inventories
{
    public struct Inventory :  IDisposable
    {
        private readonly ReactiveDictionary<ItemTypeId, int> _resources;
        private readonly CompositeDisposable _disposables;
        private readonly ReactiveProperty<string> _title;
        private readonly ReactiveProperty<bool> _isFull;

        public IReadOnlyReactiveDictionary<ItemTypeId, int> Resources => _resources;
        public IReadOnlyReactiveProperty<string> Title => _title;
        public IReadOnlyReactiveProperty<bool> IsFull => _isFull;

        public int MaxSlots { get; }
        public int CurrentSlots => _resources.Count;

        public Inventory(string name, int maxSlots)
        {
            _resources = new ReactiveDictionary<ItemTypeId, int>();
            _disposables = new CompositeDisposable();
            _title = new ReactiveProperty<string>();
            _isFull = new ReactiveProperty<bool>();
            MaxSlots = maxSlots;
            _title.Value = name;
        }
        
        public bool TryAddResource(ItemTypeId type, int amount)
        {
            if (amount <= 0)
                return false;

            if (_resources.Count >= MaxSlots && !_resources.ContainsKey(type))
                return false;

            if (!_resources.TryAdd(type, amount))
            {
                _resources[type] += amount;
            }

            UpdateFullState();
            return true;
        }

        public bool TryRemoveResource(ItemTypeId type, int amount)
        {
            if (amount <= 0 || !_resources.TryGetValue(type, out var resourceCount))
                return false;

            if (resourceCount < amount)
                return false;

            _resources[type] -= amount;

            if (_resources[type] <= 0)
                _resources.Remove(type);

            UpdateFullState();
            return true;
        }
        
        private void UpdateFullState()
        {
            _isFull.Value = _resources.Count >= MaxSlots;
        }

        public void Dispose()
        {
            _disposables.Dispose();
            _resources.Dispose();
            _title.Dispose();
            _isFull.Dispose();
        }
    }
} 