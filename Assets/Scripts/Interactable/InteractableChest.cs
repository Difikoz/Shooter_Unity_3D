using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class InteractableChest : InteractableBase
    {
        [SerializeField] private SwitchableAnimatedObject _chest;
        [SerializeField] private List<ItemStack> _stacks = new();

        public override bool CanInteract(PawnController pawn)
        {
            return pawn.StateHolder.CompareStateValue("Is Dead", false) && pawn.StateHolder.CompareStateValue("Is Perfoming Action", false) && _stacks.Count > 0;
        }

        public override void Interact(PawnController pawn)
        {
            _chest.SwitchOn();
            foreach (ItemStack stack in _stacks)
            {
                pawn.Inventory.AddItem(stack);
            }
        }
    }
}