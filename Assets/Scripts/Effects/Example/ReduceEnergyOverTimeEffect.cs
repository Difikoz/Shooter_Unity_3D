using UnityEngine;

namespace WinterUniverse
{
    public class ReduceEnergyOverTimeEffect : Effect
    {
        public ReduceEnergyOverTimeEffect(EffectConfig config, PawnController owner, PawnController source, float value, float duration) : base(config, owner, source, value, duration)
        {
        }

        public override void OnApply()
        {
            _owner.Status.ReduceEnergyCurrent(_value);
        }

        protected override void ApplyOnTick(float deltaTime)
        {
            _owner.Status.ReduceEnergyCurrent(_value * deltaTime);
        }
    }
}