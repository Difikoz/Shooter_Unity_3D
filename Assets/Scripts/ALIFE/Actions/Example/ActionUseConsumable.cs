using UnityEngine;

namespace WinterUniverse
{
    public class ActionUseConsumable : ActionBase
    {
        private ConsumableItemConfig _consumable;

        public override bool CanStart()
        {
            if (_npc.Pawn.Inventory.GetConsumable(out _consumable))
            {
                return base.CanStart();
            }
            return false;
        }

        public override void OnStart()
        {
            base.OnStart();
            _consumable.Use(_npc.Pawn);
            _consumable = null;
        }
    }
}