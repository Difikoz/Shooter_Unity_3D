using Lean.Pool;
using TMPro;
using UnityEngine;

namespace WinterUniverse
{
    public class InventoryBarUI : MonoBehaviour
    {
        [SerializeField] private Transform _contentRoot;
        [SerializeField] private GameObject _slotPrefab;
        [SerializeField] private TMP_Text _infoBarNameText;
        [SerializeField] private TMP_Text _infoBarDescriptionText;
        [SerializeField] private WeaponSlotUI _weaponSlot;
        [SerializeField] private ArmorSlotUI _armorSlot;

        public void Initialize()
        {
            GameManager.StaticInstance.PlayerManager.Pawn.Inventory.OnInventoryChanged += OnInventoryChanged;
            GameManager.StaticInstance.PlayerManager.Pawn.Equipment.OnEquipmentChanged += OnEquipmentChanged;
            OnInventoryChanged();
            OnEquipmentChanged();
        }

        public void ResetComponent()
        {
            GameManager.StaticInstance.PlayerManager.Pawn.Inventory.OnInventoryChanged -= OnInventoryChanged;
            GameManager.StaticInstance.PlayerManager.Pawn.Equipment.OnEquipmentChanged -= OnEquipmentChanged;
        }

        private void OnInventoryChanged()
        {
            while (_contentRoot.childCount > 0)
            {
                LeanPool.Despawn(_contentRoot.GetChild(0).gameObject);
            }
            foreach (ItemStack stack in GameManager.StaticInstance.PlayerManager.Pawn.Inventory.Stacks)
            {
                ShowFullInformation(stack.Item);
                LeanPool.Spawn(_slotPrefab, _contentRoot).GetComponent<InventorySlotUI>().Initialize(stack);
            }
        }

        public void OnEquipmentChanged()
        {
            _weaponSlot.Initialize(GameManager.StaticInstance.PlayerManager.Pawn.Equipment.WeaponSlot.Config);
            _armorSlot.Initialize(GameManager.StaticInstance.PlayerManager.Pawn.Equipment.ArmorSlot.Config);
        }

        public void ShowFullInformation(ItemConfig config)
        {
            _infoBarNameText.text = config.DisplayName;
            _infoBarDescriptionText.text = config.Description;
        }
    }
}