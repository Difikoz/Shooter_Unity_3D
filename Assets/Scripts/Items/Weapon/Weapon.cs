using Lean.Pool;
using UnityEngine;

namespace WinterUniverse
{
    public abstract class Weapon : MonoBehaviour
    {
        protected PawnController _pawn;
        protected WeaponItemConfig _config;
        private GameObject _model;
        private LeftHandTarget _leftHandTarget;

        public virtual void Initialize(WeaponItemConfig config)
        {
            _pawn = GetComponentInParent<PawnController>();
            if (_model != null)
            {
                LeanPool.Despawn(_model);
            }
            _config = config;
            _model = LeanPool.Spawn(_config.Model, transform);
            _leftHandTarget = GetComponentInChildren<LeftHandTarget>();
            if (_config.UseLeftHandIK)
            {
                _pawn.Animator.ToggleLeftHandIK(_leftHandTarget.transform);
            }
            else
            {
                _pawn.Animator.ToggleLeftHandIK(null);
            }
            _pawn.Animator.ToggleAimingIK(_config.UseAiming);
        }

        public virtual bool CanFire()
        {
            if (_pawn.StateHolder.CompareStateValue("Is Dead", true) || _pawn.StateHolder.CompareStateValue("Is Perfoming Action", true))
            {
                return false;
            }
            return true;
        }

        public abstract void OnFire();
    }
}