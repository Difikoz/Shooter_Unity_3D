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
            _damageCollider.Initialize(_pawn, _config.DamageTypes, _config.EquipmentData.OwnerEffects, _config.EquipmentData.TargetEffects);
            _pawn.Animator.ToggleLeftHandIK(null);
        }

        public override void OnFire()
        {
            _pawn.Animator.SetFloat("Attack Speed", _pawn.Status.AttackSpeed.CurrentValue / 100f);
            _pawn.Animator.PlayAction("Attack");
        }

        public void EnableCollider()
        {
            _damageCollider.EnableCollider();
        }

        public void DisableCollider()
        {
            _damageCollider.DisableCollider();
        }

        public void ClearTargets()
        {
            _damageCollider.ClearTargets();
        }
    }
}