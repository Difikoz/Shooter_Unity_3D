using UnityEngine;

namespace WinterUniverse
{
    public class RestoreEnergyOverTimeEffect : Effect
    {
        public RestoreEnergyOverTimeEffect(EffectConfig config, PawnController owner, PawnController source, float value, float duration) : base(config, owner, source, value, duration)
        {
        }

        public override void OnApply()
        {
            _owner.Status.RestoreEnergyCurrent(_value);
        }

        protected override void ApplyOnTick(float deltaTime)
        {
            _owner.Status.RestoreEnergyCurrent(_value * deltaTime);
        }
    }
}