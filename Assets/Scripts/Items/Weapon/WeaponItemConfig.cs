using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Winter Universe/Item/New Weapon")]
    public class WeaponItemConfig : ItemConfig
    {
        [SerializeField] private WeaponTypeConfig _weaponType;
        [SerializeField] private float _attackMinRange = 1f;
        [SerializeField] private float _attackMaxRange = 2f;
        [SerializeField] private float _attackAngle = 10f;
        [SerializeField] private List<DamageType> _damageTypes = new();
        [SerializeField] private EquipmentData _equipmentData;

        public WeaponTypeConfig WeaponType => _weaponType;
        public float AttackMinRange => _attackMinRange;
        public float AttackMaxRange => _attackMaxRange;
        public float AttackAngle => _attackAngle;
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