using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Player", menuName = "Winter Universe/Data/New Player")]
    public class PlayerConfig : ScriptableObject
    {
        [SerializeField] private string _displayName = "Player";
        [SerializeField] private VisualConfig _visual;
        [SerializeField] private VoiceConfig _voice;
        [SerializeField] private InventoryConfig _inventory;
        [SerializeField] private StateHolderConfig _stateHolder;
        [SerializeField] private WeaponItemConfig _weapon;
        [SerializeField] private ArmorItemConfig _armor;
        [SerializeField] private List<ItemStack> _stacks = new();

        public PawnData GetPawnData()
        {
            PawnData data = new()
            {
                DisplayName = _displayName,
                Visual = _visual.DisplayName,
                Voice = _voice.DisplayName,
                Inventory = _inventory.DisplayName,
                StateHolder = _stateHolder.DisplayName,
            };
            return data;
        }

        public PlayerData GetPlayerData()
        {
            PlayerData data = new()
            {
                Weapon = _weapon.DisplayName,
                Armor = _armor.DisplayName,
                Stacks = new(),
            };
            foreach (ItemStack stack in _stacks)
            {
                if (data.Stacks.ContainsKey(stack.Item.DisplayName))
                {
                    data.Stacks[stack.Item.DisplayName] += stack.Amount;
                }
                else
                {
                    data.Stacks.Add(stack.Item.DisplayName, stack.Amount);
                }
            }
            return data;
        }
    }
}