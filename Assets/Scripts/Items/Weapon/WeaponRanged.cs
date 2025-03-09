using UnityEngine;

namespace WinterUniverse
{
    public class WeaponRanged : Weapon
    {
        private ShootPoint _shootPoint;

        public override void Initialize(WeaponItemConfig config)
        {
            base.Initialize(config);
            _shootPoint = GetComponentInChildren<ShootPoint>();
        }

        public override void OnFire()
        {
            //fire
        }
    }
}