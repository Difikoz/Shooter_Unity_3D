using System;
using UnityEngine;

namespace WinterUniverse
{
    public class PawnEquipment : MonoBehaviour
    {
        public Action OnEquipmentChanged;

        private PawnController _pawn;
        private WeaponSlot _weaponSlot;
        private ArmorSlot _armorSlot;

        public WeaponSlot WeaponSlot => _weaponSlot;
        public ArmorSlot ArmorSlot => _armorSlot;

        public void Initialize()
        {
            _pawn = GetComponentInParent<PawnController>();
            _weaponSlot = GetComponentInChildren<WeaponSlot>();
            _armorSlot = GetComponentInChildren<ArmorSlot>();
            _weaponSlot.Initialize();
            _armorSlot.Initialize();
        }

        public void ResetComponent()
        {

        }

        public void EquipWeapon(WeaponItemConfig config, bool removeNewFromInventory = true, bool addOldToInventory = true)
        {
            if (config == null || _pawn.StateHolder.CompareStateValue("Is Dead", true) || _pawn.StateHolder.CompareStateValue("Is Perfoming Action", true))
            {
                return;
            }
            if (removeNewFromInventory)
            {
                _pawn.Inventory.RemoveItem(config);
            }
            if (addOldToInventory && _weaponSlot.Config != null)
            {
                _pawn.Inventory.AddItem(_weaponSlot.Config);
            }
            _weaponSlot.ChangeConfig(config);
            _pawn.StateHolder.SetState("Equipped Weapon", true);
            //if (config.PlayAnimationOnUse)
            //{
            //    _pawn.Animator.PlayAction(config.AnimationOnUse);
            //}
            OnEquipmentChanged?.Invoke();
        }

        public void UnequipWeapon(bool addOldToInventory = true)
        {
            if (addOldToInventory && _weaponSlot.Config != null)
            {
                _pawn.Inventory.AddItem(_weaponSlot.Config);
            }
            _weaponSlot.ChangeConfig(null);
            _pawn.StateHolder.SetState("Equipped Weapon", false);
            OnEquipmentChanged?.Invoke();
        }

        public void EquipArmor(ArmorItemConfig config, bool removeNewFromInventory = true, bool addOldToInventory = true)
        {
            if (config == null || _pawn.StateHolder.CompareStateValue("Is Dead", true) || _pawn.StateHolder.CompareStateValue("Is Perfoming Action", true))
            {
                return;
            }
            if (removeNewFromInventory)
            {
                _pawn.Inventory.RemoveItem(config);
            }
            if (addOldToInventory && _armorSlot.Config != null)
            {
                _pawn.Inventory.AddItem(_armorSlot.Config);
            }
            _armorSlot.ChangeConfig(config);
            _pawn.StateHolder.SetState($"Equipped Armor", true);
            //if (config.PlayAnimationOnUse)
            //{
            //    _pawn.Animator.PlayAction(config.AnimationOnUse);
            //}
            OnEquipmentChanged?.Invoke();
        }

        public void UnequipArmor(bool addOldToInventory = true)
        {
            if (addOldToInventory && _armorSlot.Config != null)
            {
                _pawn.Inventory.AddItem(_armorSlot.Config);
            }
            _armorSlot.ChangeConfig(null);
            _pawn.StateHolder.SetState($"Equipped Armor", false);
            OnEquipmentChanged?.Invoke();
        }
    }
}