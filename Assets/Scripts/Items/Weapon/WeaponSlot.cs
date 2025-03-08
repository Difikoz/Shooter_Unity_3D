using Lean.Pool;
using UnityEngine;

namespace WinterUniverse
{
    public class WeaponSlot : MonoBehaviour
    {
        private PawnController _pawn;
        private WeaponItemConfig _config;
        private GameObject _model;
        private DamageCollider _damageCollider;

        public WeaponItemConfig Config => _config;
        public DamageCollider DamageCollider => _damageCollider;

        public void Initialize()
        {
            _pawn = GetComponentInParent<PawnController>();
        }

        public void ChangeConfig(WeaponItemConfig config)
        {
            if (_config != null)
            {
                _pawn.Status.RemoveStatModifiers(_config.EquipmentData.Modifiers);
                LeanPool.Despawn(_model);
                _damageCollider = null;
            }
            _config = config;
            if (_config != null)
            {
                _pawn.Status.AddStatModifiers(_config.EquipmentData.Modifiers);
                _model = LeanPool.Spawn(_config.Model, transform);
                _damageCollider = GetComponentInChildren<DamageCollider>();
                _damageCollider.Initialize(_pawn, _config.DamageTypes, _config.EquipmentData.OwnerEffects, _config.EquipmentData.TargetEffects);
            }
        }
    }
}