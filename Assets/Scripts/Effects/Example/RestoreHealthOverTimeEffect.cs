using UnityEngine;

namespace WinterUniverse
{
    public class RestoreHealthOverTimeEffect : Effect
    {
        public RestoreHealthOverTimeEffect(EffectConfig config, PawnController owner, PawnController source, float value, float duration) : base(config, owner, source, value, duration)
        {
        }

        public override void OnApply()
        {
            _owner.Status.RestoreHealthCurrent(_value);
        }

        protected override void ApplyOnTick(float deltaTime)
        {
            _owner.Status.RestoreHealthCurrent(_value * deltaTime);
        }
    }
}