using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Inventory", menuName = "Winter Universe/Pawn/New Inventory")]
    public class InventoryConfig : ScriptableObject
    {
        [SerializeField] private string _displayName = "Name";
        [SerializeField] private List<ItemStack> _stacks = new();

        public string DisplayName => _displayName;
        public List<ItemStack> Stacks => _stacks;
    }
}