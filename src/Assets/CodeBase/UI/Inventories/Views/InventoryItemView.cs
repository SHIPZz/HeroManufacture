using CodeBase.Gameplay.Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Inventories.Views
{
    public class InventoryItemView : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _amountText;
        [SerializeField] private TextMeshProUGUI _nameText;

        public ItemTypeId ItemType { get; private set; }

        public void Initialize(ItemTypeId type, int amount, Sprite icon)
        {
            _icon.sprite = icon;
            ItemType = type;
            _nameText.text = ItemType.ToString();
            
            UpdateAmount(amount);
        }

        public void UpdateAmount(int amount)
        {
            _amountText.text = amount.ToString();
        }
    }
} 