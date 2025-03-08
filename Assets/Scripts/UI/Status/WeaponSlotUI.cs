using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace WinterUniverse
{
    public class WeaponSlotUI : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, ISelectHandler, ISubmitHandler
    {
        [SerializeField] private Button _thisButton;
        [SerializeField] private Image _iconImage;
        [SerializeField] private Sprite _emptySprite;

        private WeaponItemConfig _weapon;

        public void Initialize(WeaponItemConfig weapon)
        {
            _weapon = weapon;
            if (_weapon != null)
            {
                _iconImage.sprite = _weapon.Icon;
            }
            else
            {
                _iconImage.sprite = _emptySprite;
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _thisButton.Select();
        }

        public void OnSelect(BaseEventData eventData)
        {
            if (_weapon == null)
            {
                return;
            }
            GameManager.StaticInstance.UIManager.StatusBar.InventoryBar.ShowFullInformation(_weapon);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_weapon == null)
            {
                return;
            }
            GameManager.StaticInstance.PlayerManager.Pawn.Equipment.UnequipWeapon();
        }

        public void OnSubmit(BaseEventData eventData)
        {
            if (_weapon == null)
            {
                return;
            }
            GameManager.StaticInstance.PlayerManager.Pawn.Equipment.UnequipWeapon();
        }
    }
}