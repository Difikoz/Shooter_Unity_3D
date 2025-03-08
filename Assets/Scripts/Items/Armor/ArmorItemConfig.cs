using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Armor", menuName = "Winter Universe/Item/New Armor")]
    public class ArmorItemConfig : ItemConfig
    {
        [SerializeField] private ArmorTypeConfig _armorType;
        [SerializeField] private EquipmentData _equipmentData;

        public ArmorTypeConfig ArmorType => _armorType;
        public EquipmentData EquipmentData => _equipmentData;

        private void OnValidate()
        {
            _itemType = ItemType.Armor;
        }

        public override void Use(PawnController pawn, bool fromInventory = true)
        {
            pawn.Equipment.EquipArmor(this, fromInventory);
        }
    }
}