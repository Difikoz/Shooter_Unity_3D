using UnityEngine;

namespace WinterUniverse
{
    public class ActionEquipWeapon : ActionBase
    {
        private WeaponItemConfig _weapon;

        public override bool CanStart()
        {
            if (_npc.Pawn.Inventory.GetWeapon(out _weapon))
            {
                return base.CanStart();
            }
            return false;
        }

        public override void OnStart()
        {
            base.OnStart();
            _weapon.Use(_npc.Pawn);
            _weapon = null;
        }
    }
}