using Lean.Pool;
using UnityEngine;

namespace WinterUniverse
{
    public class WeaponSlot : MonoBehaviour
    {
        private PawnController _pawn;
        private WeaponItemConfig _config;
        private GameObject _model;
        private ShootPoint _shootPoint;
        private float _lastFireTime;

        public WeaponItemConfig Config => _config;

        public void Initialize()
        {
            _pawn = GetComponentInParent<PawnController>();
        }

        public void ChangeConfig(WeaponItemConfig config)
        {
            if (_config != null)
            {
                _pawn.Status.RemoveStatModifiers(_config.EquipmentData.Modifiers);
            }
            if (_model != null)
            {
                LeanPool.Despawn(_model);
                _model = null;
                _shootPoint = null;
            }
            _config = config;
            if (_config != null)
            {
                _pawn.Status.AddStatModifiers(_config.EquipmentData.Modifiers);
                _model = LeanPool.Spawn(_config.Model, transform);
                _shootPoint = GetComponentInChildren<ShootPoint>();
            }
        }

        public bool CanAttack()
        {
            return Time.time > _config.FireRate / 600f + _lastFireTime && _pawn.StateHolder.CompareStateValue("Is Dead", false) && _pawn.StateHolder.CompareStateValue("Is Perfoming Action", false);
        }

        public void OnAttack()
        {

            _lastFireTime = Time.time;
        }
    }
}