using System;
using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class PawnInventory : MonoBehaviour
    {
        public Action OnInventoryChanged;

        private PawnController _pawn;
        private List<ItemStack> _stacks = new();

        public List<ItemStack> Stacks => _stacks;

        public void Initialize(PawnData data)
        {
            _pawn = GetComponent<PawnController>();
            _stacks.Clear();
            foreach (ItemStack stack in GameManager.StaticInstance.ConfigsManager.GetInventory(data.Inventory).Stacks)
            {
                AddItem(stack);
            }
        }

        public void Initialize(SerializableDictionary<string, int> stacks)
        {
            _pawn = GetComponent<PawnController>();
            _stacks.Clear();
            foreach (KeyValuePair<string, int> stack in stacks)
            {
                AddItem(GameManager.StaticInstance.ConfigsManager.GetItem(stack.Key), stack.Value);
            }
        }

        public void ResetComponent()
        {
            _stacks.Clear();
        }

        public void AddItem(ItemStack stack)
        {
            AddItem(stack.Item, stack.Amount);
        }

        public void AddItem(ItemConfig item, int amount = 1)
        {
            if (item.StackSize > 1)
            {
                foreach (ItemStack stack in _stacks)
                {
                    if (stack.Item.DisplayName == item.DisplayName)
                    {
                        while (stack.HasFreeSpace() && amount > 0)
                        {
                            stack.AddToStack();
                            amount--;
                        }
                        if (amount <= 0)
                        {
                            break;
                        }
                    }
                }
                while (amount > 0)
                {
                    ItemStack stack = new(item, 0);
                    while (stack.HasFreeSpace() && amount > 0)
                    {
                        stack.AddToStack();
                        amount--;
                    }
                    _stacks.Add(stack);
                }
            }
            else
            {
                while (amount > 0)
                {
                    _stacks.Add(new(item));
                    amount--;
                }
            }
            _pawn.StateHolder.SetState("Has New Items", true);
            UpdateInventory();
        }

        public bool RemoveItem(ItemStack stack)
        {
            return RemoveItem(stack.Item, stack.Amount);
        }

        public bool RemoveItem(ItemConfig item, int amount = 1)
        {
            if (AmountOfItem(item) < amount)
            {
                Debug.LogError($"Not Enough {amount} {item.DisplayName} in {_pawn.gameObject.name} Inventory (Has {AmountOfItem(item)}).");
                return false;
            }
            for (int i = _stacks.Count - 1; i >= 0; i--)
            {
                if (_stacks[i].Item.DisplayName == item.DisplayName)
                {
                    while (_stacks[i].Amount > 0 && amount > 0)
                    {
                        _stacks[i].RemoveFromStack();
                        amount--;
                    }
                    if (_stacks[i].Amount <= 0)
                    {
                        _stacks.RemoveAt(i);
                    }
                    if (amount <= 0)
                    {
                        break;
                    }
                }
            }
            UpdateInventory();
            return true;
        }

        public bool DropItem(ItemStack stack)
        {
            return DropItem(stack.Item, stack.Amount);
        }

        public bool DropItem(ItemConfig item, int amount = 1)
        {
            if (AmountOfItem(item) < amount)
            {
                Debug.LogError($"Not Enough {amount} {item.DisplayName} in {_pawn.gameObject.name} Inventory (Has {AmountOfItem(item)}).");
                return false;
            }
            for (int i = _stacks.Count - 1; i >= 0; i--)
            {
                if (_stacks[i].Item.DisplayName == item.DisplayName)
                {
                    if (_stacks[i].Amount < amount)
                    {
                        amount -= _stacks[i].Amount;
                        GameManager.StaticInstance.PrefabsManager.GetItem(transform.position, Quaternion.identity).Initialize(item, _stacks[i].Amount);
                        _stacks.RemoveAt(i);
                    }
                    else
                    {
                        GameManager.StaticInstance.PrefabsManager.GetItem(transform.position, Quaternion.identity).Initialize(item, amount);
                        _stacks[i].RemoveFromStack(amount);
                        if (_stacks[i].Amount == 0)
                        {
                            _stacks.RemoveAt(i);
                        }
                        break;
                    }
                }
            }
            UpdateInventory();
            return true;
        }

        public int AmountOfItem(ItemConfig item)
        {
            int amount = 0;
            foreach (ItemStack stack in _stacks)
            {
                if (stack.Item.DisplayName == item.DisplayName)
                {
                    amount++;
                }
            }
            return amount;
        }

        public bool GetWeapon(out WeaponItemConfig weapon)
        {
            weapon = null;
            int price = 0;
            foreach (ItemStack stack in _stacks)
            {
                if (stack.Item.ItemType == ItemType.Weapon && stack.Item.Price > price)
                {
                    weapon = (WeaponItemConfig)stack.Item;
                    price = weapon.Price;
                }
            }
            return weapon != null;
        }

        public bool GetArmor(out ArmorItemConfig armor)
        {
            armor = null;
            int price = 0;
            foreach (ItemStack stack in _stacks)
            {
                if (stack.Item.ItemType == ItemType.Armor && stack.Item.Price > price)
                {
                    armor = (ArmorItemConfig)stack.Item;
                    price = armor.Price;
                }
            }
            return armor != null;
        }

        public bool GetConsumable(out ConsumableItemConfig consumable)// get best
        {
            consumable = null;
            int price = 0;
            foreach (ItemStack stack in _stacks)
            {
                if (stack.Item.ItemType == ItemType.Consumable && stack.Item.Price > price)
                {
                    consumable = (ConsumableItemConfig)stack.Item;
                    price = consumable.Price;
                }
            }
            return consumable != null;
        }

        //public bool GetConsumable(out ConsumableItemConfig consumable)// get junk
        //{
        //    consumable = null;
        //    int price = int.MaxValue;
        //    foreach (ItemStack stack in _stacks)
        //    {
        //        if (stack.Item.ItemType == ItemType.Consumable && stack.Item.Price < price)
        //        {
        //            consumable = (ConsumableItemConfig)stack.Item;
        //            price = consumable.Price;
        //        }
        //    }
        //    return consumable != null;
        //}

        public bool GetResource(string name, out ResourceItemConfig resource)
        {
            resource = null;
            foreach (ItemStack stack in _stacks)
            {
                if (stack.Item.ItemType == ItemType.Resource && stack.Item.DisplayName == name)
                {
                    resource = (ResourceItemConfig)stack.Item;
                    break;
                }
            }
            return resource != null;
        }

        private void UpdateInventory()
        {
            OnInventoryChanged?.Invoke();
        }
    }
}