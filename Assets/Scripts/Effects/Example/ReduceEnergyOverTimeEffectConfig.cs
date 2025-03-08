using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Reduce Energy Over Time", menuName = "Winter Universe/Effect/New Reduce Energy Over Time")]
    public class ReduceEnergyOverTimeEffectConfig : EffectConfig
    {
        private void OnValidate()
        {
            _effectType = EffectType.ReduceEnergyOverTime;
        }

        public override Effect CreateEffect(PawnController target, PawnController source, float value, float duration)
        {
            return new ReduceEnergyOverTimeEffect(this, target, source, value, duration);
        }
    }
}