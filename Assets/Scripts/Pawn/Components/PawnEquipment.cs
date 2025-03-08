using System;
using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class PawnEquipment : MonoBehaviour
    {
        public Action OnEquipmentChanged;

        private PawnController _pawn;
        private WeaponSlot _weaponSlot;
        private List<ArmorSlot> _armorSlots = new();

        public WeaponSlot WeaponSlot => _weaponSlot;
        public List<ArmorSlot> ArmorSlots => _armorSlots;

        public void Initialize()
        {
            _pawn = GetComponentInParent<PawnController>();
            _weaponSlot = GetComponentInChildren<WeaponSlot>();
            ArmorSlot[] armorSlots = GetComponentsInChildren<ArmorSlot>();
            foreach (ArmorSlot slot in armorSlots)
            {
                _armorSlots.Add(slot);
                slot.Initialize();
            }
            _weaponSlot.Initialize();
        }

        public void ResetComponent()
        {
            _armorSlots.Clear();
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
            foreach (ArmorSlot slot in _armorSlots)
            {
                if (slot.Type == config.ArmorType)
                {
                    if (removeNewFromInventory)
                    {
                        _pawn.Inventory.RemoveItem(config);
                    }
                    if (addOldToInventory && slot.Config != null)
                    {
                        _pawn.Inventory.AddItem(slot.Config);
                    }
                    slot.ChangeConfig(config);
                    _pawn.StateHolder.SetState($"Equipped {slot.Type.DisplayName}", true);
                    break;
                }
            }
            //if (config.PlayAnimationOnUse)
            //{
            //    _pawn.Animator.PlayAction(config.AnimationOnUse);
            //}
            OnEquipmentChanged?.Invoke();
        }

        public void UnequipArmor(ArmorTypeConfig type, bool addOldToInventory = true)
        {
            UnequipArmor(type.DisplayName, addOldToInventory);
        }

        public void UnequipArmor(int index, bool addOldToInventory = true)
        {
            if (index >= _armorSlots.Count)
            {
                return;
            }
            UnequipArmor(_armorSlots[index], addOldToInventory);
        }

        public void UnequipArmor(string type, bool addOldToInventory = true)
        {
            foreach (ArmorSlot slot in _armorSlots)
            {
                if (slot.Type.DisplayName == type)
                {
                    UnequipArmor(slot, addOldToInventory);
                    break;
                }
            }
        }

        public void UnequipArmor(ArmorSlot slot, bool addOldToInventory = true)
        {
            if (addOldToInventory && slot.Config != null)
            {
                _pawn.Inventory.AddItem(slot.Config);
            }
            slot.ChangeConfig(null);
            _pawn.StateHolder.SetState($"Equipped {slot.Type.DisplayName}", false);
            OnEquipmentChanged?.Invoke();
        }

        public WeaponItemConfig GetWeaponInSlot()
        {
            return _weaponSlot.Config;
        }

        public ArmorItemConfig GetArmorInSlot(string type)
        {
            foreach (ArmorSlot slot in _armorSlots)
            {
                if (slot.Type.DisplayName == type)
                {
                    return slot.Config;
                }
            }
            return null;
        }
    }
}