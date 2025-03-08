using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Consumable", menuName = "Winter Universe/Item/New Consumable")]
    public class ConsumableItemConfig : ItemConfig
    {
        [SerializeField] private ConsumableTypeConfig _consumableType;
        [SerializeField] private List<EffectCreator> _effects = new();

        public ConsumableTypeConfig ConsumableType => _consumableType;
        public List<EffectCreator> Effects => _effects;

        private void OnValidate()
        {
            _itemType = ItemType.Consumable;
        }

        public override void Use(PawnController pawn, bool fromInventory = true)
        {
            if (pawn.StateHolder.CompareStateValue("Is Dead", true) || pawn.StateHolder.CompareStateValue("Is Perfoming Action", true))
            {
                return;
            }
            if (!fromInventory || (fromInventory && pawn.Inventory.RemoveItem(this)))
            {
                //if (_playAnimationOnUse)
                //{
                //    pawn.Animator.PlayAction(_animationOnUse);
                //}
                foreach (EffectCreator effect in _effects)
                {
                    pawn.Effects.AddEffect(effect.Config.CreateEffect(pawn, pawn, effect.Value, effect.Duration));
                }
            }
        }
    }
}