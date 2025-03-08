using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [System.Serializable]
    public class Stat
    {
        private StatConfig _config;
        private float _currentValue;
        private List<float> _flatModifiers = new();
        private List<float> _multiplierModifiers = new();

        public StatConfig Config => _config;
        public float CurrentValue => _currentValue;
        public List<float> FlatModifiers => _flatModifiers;
        public List<float> MultiplierModifiers => _multiplierModifiers;

        public Stat(StatConfig config)
        {
            _config = config;
            _currentValue = _config.BaseValue;
        }

        public void AddModifier(StatModifier modifier)
        {
            if (modifier.Type == StatModifierType.Flat)
            {
                _flatModifiers.Add(modifier.Value);
            }
            else if (modifier.Type == StatModifierType.Multiplier)
            {
                _multiplierModifiers.Add(modifier.Value);
            }
            CalculateCurrentValue();
        }

        public void RemoveModifier(StatModifier modifier)
        {
            if (modifier.Type == StatModifierType.Flat && _flatModifiers.Contains(modifier.Value))
            {
                _flatModifiers.Remove(modifier.Value);
            }
            else if (modifier.Type == StatModifierType.Multiplier && _multiplierModifiers.Contains(modifier.Value))
            {
                _multiplierModifiers.Remove(modifier.Value);
            }
            CalculateCurrentValue();
        }

        public void CalculateCurrentValue()
        {
            float value = _config.BaseValue;
            foreach (float f in _flatModifiers)
            {
                value += f;
            }
            float multiplierValue = 0f;
            foreach (float f in _multiplierModifiers)
            {
                multiplierValue += f;
            }
            if (multiplierValue != 0f)
            {
                multiplierValue *= value;
                multiplierValue /= 100f;
                value += multiplierValue;
            }
            if (_config.ClampMinValue && value < _config.MinValue)
            {
                value = _config.MinValue;
            }
            else if (_config.ClampMaxValue && value > _config.MaxValue)
            {
                value = _config.MaxValue;
            }
            _currentValue = value;
        }
    }
}