using UnityEngine;

namespace WinterUniverse
{
    public class WeaponMelee : Weapon
    {
        private DamageCollider _damageCollider;

        public override void Initialize(WeaponItemConfig config)
        {
            base.Initialize(config);
            _damageCollider = GetComponentInChildren<DamageCollider>();
        }

        public override void OnFire()
        {
            _pawn.Animator.PlayAction("Attack");
        }
    }
}