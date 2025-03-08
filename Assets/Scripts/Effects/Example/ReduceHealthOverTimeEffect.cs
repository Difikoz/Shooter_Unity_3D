using UnityEngine;

namespace WinterUniverse
{
    public class ReduceHealthOverTimeEffect : Effect
    {
        private ElementConfig _element;

        public ReduceHealthOverTimeEffect(EffectConfig config, PawnController owner, PawnController source, float value, float duration, ElementConfig element) : base(config, owner, source, value, duration)
        {
            _element = element;
        }

        public override void OnApply()
        {
            if (_source != null)
            {
                _value += _value * _source.Status.GetStat(_element.DamageStat.DisplayName).CurrentValue;
                _value *= _source.Status.DamageDealt.CurrentValue;
            }
            _owner.Status.ReduceHealthCurrent(_value, _element, _source);
        }

        protected override void ApplyOnTick(float deltaTime)
        {
            _owner.Status.ReduceHealthCurrent(_value * deltaTime, _element, _source);
        }
    }
}