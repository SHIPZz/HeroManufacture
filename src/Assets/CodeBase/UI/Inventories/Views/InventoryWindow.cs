using System;
using System.Collections.Generic;
using CodeBase.Gameplay.Items;
using CodeBase.UI.AbstractWindow;
using CodeBase.UI.Inventories.Factory;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CodeBase.UI.Inventories.Views
{
    public class InventoryWindow : AbstractWindowBase
    {
        [SerializeField] private Transform _itemsContainer;
        [SerializeField] private Button _closeButton;
        [SerializeField] private TextMeshProUGUI _titleText;
        [SerializeField] private GameObject _fullIndicator;

        private readonly List<InventoryItemView> _itemViews = new();
        private IInventoryUIFactory _inventoryUIFactory;

        public IObservable<Unit> OnCloseClicked => _closeButton.OnClickAsObservable();

        [Inject]
        private void Construct(IInventoryUIFactory inventoryUIFactory) => _inventoryUIFactory = inventoryUIFactory;

        public void SetTitle(string title) => _titleText.text = title;

        public void SetFullState(bool isFull)
        {
            if (_fullIndicator != null)
                _fullIndicator.SetActive(isFull);
        }

        public void AddResource(ItemTypeId type, int amount) => CreateItemView(type, amount);

        public void RemoveResource(ItemTypeId type)
        {
            InventoryItemView itemView = _itemViews.Find(view => view.ItemType == type);
            
            if (itemView != null)
            {
                _itemViews.Remove(itemView);
                Destroy(itemView.gameObject);
            }
        }

        public void UpdateResourceAmount(ItemTypeId type, int amount)
        {
            InventoryItemView itemView = _itemViews.Find(view => view.ItemType == type);
            
            if (itemView != null)
            {
                itemView.UpdateAmount(amount);
            }
        }

        public void SetItemsContainerActive(bool isActive) => _itemsContainer.gameObject.SetActive(isActive);

        private void CreateItemView(ItemTypeId type, int amount)
        {
           InventoryItemView item = _inventoryUIFactory.Create(_itemsContainer, type, amount);
            
           _itemViews.Add(item);
        }

        private void OnDestroy()
        {
            foreach (InventoryItemView view in _itemViews)
            {
                if (view != null)
                    Destroy(view.gameObject);
            }
            
            _itemViews.Clear();
        }
    }
} 