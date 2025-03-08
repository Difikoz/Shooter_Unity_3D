using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace WinterUniverse
{
    public class ArmorSlotUI : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, ISelectHandler, ISubmitHandler
    {
        [SerializeField] private Button _thisButton;
        [SerializeField] private Image _iconImage;
        [SerializeField] private ArmorTypeConfig _type;

        private ArmorItemConfig _armor;
        public ArmorTypeConfig Type => _type;

        public void Initialize(ArmorItemConfig weapon)
        {
            _armor = weapon;
            if (_armor != null)
            {
                _iconImage.sprite = _armor.Icon;
            }
            else
            {
                _iconImage.sprite = _type.Icon;
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _thisButton.Select();
        }

        public void OnSelect(BaseEventData eventData)
        {
            if (_armor == null)
            {
                return;
            }
            GameManager.StaticInstance.UIManager.StatusBar.InventoryBar.ShowFullInformation(_armor);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_armor == null)
            {
                return;
            }
            GameManager.StaticInstance.PlayerManager.Pawn.Equipment.UnequipArmor(_type);
        }

        public void OnSubmit(BaseEventData eventData)
        {
            if (_armor == null)
            {
                return;
            }
            GameManager.StaticInstance.PlayerManager.Pawn.Equipment.UnequipArmor(_type);
        }
    }
}