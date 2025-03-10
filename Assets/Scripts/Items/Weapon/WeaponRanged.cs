using UnityEngine;

namespace WinterUniverse
{
    public class WeaponRanged : Weapon
    {
        private ShootPoint _shootPoint;
        private float _lastFireTime;

        public override void Initialize(WeaponItemConfig config)
        {
            base.Initialize(config);
            _shootPoint = GetComponentInChildren<ShootPoint>();
        }

        public override bool CanAttack()
        {
            if (Time.time < _config.FireRate / 600f + _lastFireTime)
            {
                return false;
            }
            return base.CanAttack();
        }

        public override void OnAttack()
        {
            _lastFireTime = Time.time;
            Debug.Log($"POOF! {_lastFireTime}");
        }
    }
}