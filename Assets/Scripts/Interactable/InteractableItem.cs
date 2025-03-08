using Lean.Pool;
using UnityEngine;

namespace WinterUniverse
{
    [RequireComponent(typeof(Rigidbody))]
    public class InteractableItem : InteractableBase
    {
        private ItemConfig _item;
        private int _amount = 1;
        private GameObject _model;

        public void Initialize(ItemStack stack)
        {
            Initialize(stack.Item, stack.Amount);
        }

        public void Initialize(ItemConfig item, int amount = 1)
        {
            _item = item;
            _amount = amount;
            if (_model != null)
            {
                LeanPool.Despawn(_model);
            }
            _model = LeanPool.Spawn(_item.Model, transform);
        }

        public override bool CanInteract(PawnController pawn)
        {
            return _item != null && _amount > 0 && pawn.StateHolder.CompareStateValue("Is Dead", false) && pawn.StateHolder.CompareStateValue("Is Perfoming Action", false);
        }

        public override void Interact(PawnController pawn)
        {
            pawn.Inventory.AddItem(_item, _amount);
            LeanPool.Despawn(gameObject);
        }
    }
}