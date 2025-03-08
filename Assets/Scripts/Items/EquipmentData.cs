using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [System.Serializable]
    public class EquipmentData
    {
        [SerializeField] private List<StatModifierCreator> _modifiers = new();
        [SerializeField] private List<EffectCreator> _ownerEffects = new();
        [SerializeField] private List<EffectCreator> _targetEffects = new();

        public List<StatModifierCreator> Modifiers => _modifiers;
        public List<EffectCreator> OwnerEffects => _ownerEffects;
        public List<EffectCreator> TargetEffects => _targetEffects;
    }
}