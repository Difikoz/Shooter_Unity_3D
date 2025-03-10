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

        public virtual bool CanAttack()
        {
            return _pawn.StateHolder.CompareStateValue("Is Dead", false) && _pawn.StateHolder.CompareStateValue("Is Perfoming Action", false);
        }

        public abstract void OnAttack();
    }
}