using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace WinterUniverse
{
    public class InventorySlotUI : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, ISelectHandler, ISubmitHandler
    {
        [SerializeField] private Button _thisButton;
        [SerializeField] private Image _iconImage;
        [SerializeField] private TMP_Text _amountText;

        private ItemStack _stack;

        public void Initialize(ItemStack stack)
        {
            _stack = stack;
            _iconImage.sprite = _stack.Item.Icon;
            _amountText.text = $"{(_stack.Amount > 1 ? _stack.Amount : "")}";
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _thisButton.Select();
        }

        public void OnSelect(BaseEventData eventData)
        {
            GameManager.StaticInstance.UIManager.StatusBar.InventoryBar.ShowFullInformation(_stack.Item);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _stack.Item.Use(GameManager.StaticInstance.PlayerManager.Pawn);
        }

        public void OnSubmit(BaseEventData eventData)
        {
            _stack.Item.Use(GameManager.StaticInstance.PlayerManager.Pawn);
        }
    }
}