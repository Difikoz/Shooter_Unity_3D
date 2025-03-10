using Lean.Pool;
using UnityEngine;

namespace WinterUniverse
{
    public class WeaponSlot : MonoBehaviour
    {
        private PawnController _pawn;
        private WeaponItemConfig _config;
        private Weapon _weapon;

        public WeaponItemConfig Config => _config;
        public Weapon Weapon => _weapon;

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
            if (_weapon != null)
            {
                LeanPool.Despawn(_weapon.gameObject);
                _weapon = null;
            }
            _config = config;
            if (_config != null)
            {
                _pawn.Status.AddStatModifiers(_config.EquipmentData.Modifiers);
                _weapon = LeanPool.Spawn(_config.WeaponTypePrefab, transform).GetComponent<Weapon>();
                _weapon.Initialize(_config);
            }
            _pawn.Animator.ChangeController(_config);
        }
    }
}