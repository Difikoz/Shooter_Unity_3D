using Lean.Pool;
using UnityEngine;

namespace WinterUniverse
{
    public abstract class Weapon : MonoBehaviour
    {
        protected PawnController _pawn;
        protected WeaponItemConfig _config;
        private GameObject _model;

        public virtual void Initialize(WeaponItemConfig config)
        {
            _pawn = GetComponentInParent<PawnController>();
            if (_model != null)
            {
                LeanPool.Despawn(_model);
            }
            _config = config;
            _model = LeanPool.Spawn(_config.Model, transform);
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