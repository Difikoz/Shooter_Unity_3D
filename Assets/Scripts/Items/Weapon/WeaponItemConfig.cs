using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Winter Universe/Item/New Weapon")]
    public class WeaponItemConfig : ItemConfig
    {
        [SerializeField] private GameObject _weaponTypePrefab;
        [SerializeField] private AnimatorOverrideController _controller;
        [SerializeField] private bool _useLeftHandIK = true;
        [SerializeField] private bool _useAiming = true;
        [SerializeField] private float _fireRate = 300f;
        [SerializeField] private float _minRange = 50f;
        [SerializeField] private float _maxRange = 100f;
        [SerializeField] private float _force = 250f;
        [SerializeField] private float _spread = 5f;
        [SerializeField] private float _energyConsumption = 5f;
        [SerializeField] private int _projectilePerShot = 1;
        [SerializeField] private List<DamageType> _damageTypes = new();
        [SerializeField] private EquipmentData _equipmentData;

        public GameObject WeaponTypePrefab => _weaponTypePrefab;
        public AnimatorOverrideController Controller => _controller;
        public bool UseLeftHandIK => _useLeftHandIK;
        public bool UseAiming => _useAiming;
        public float FireRate => _fireRate;
        public float MinRange => _minRange;
        public float MaxRange => _maxRange;
        public float Force => _force;
        public float Spread => _spread;
        public float EnergyConsumption => _energyConsumption;
        public int ProjectilePerShot => _projectilePerShot;
        public List<DamageType> DamageTypes => _damageTypes;
        public EquipmentData EquipmentData => _equipmentData;

        private void OnValidate()
        {
            _itemType = ItemType.Weapon;
        }

        public override void Use(PawnController pawn, bool fromInventory = true)
        {
            pawn.Equipment.EquipWeapon(this, fromInventory);
        }
    }
}