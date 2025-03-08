using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Restore Energy Over Time", menuName = "Winter Universe/Effect/New Restore Energy Over Time")]
    public class RestoreEnergyOverTimeEffectConfig : EffectConfig
    {
        private void OnValidate()
        {
            _effectType = EffectType.RestoreEnergyOverTime;
        }

        public override Effect CreateEffect(PawnController target, PawnController source, float value, float duration)
        {
            return new RestoreEnergyOverTimeEffect(this, target, source, value, duration);
        }
    }
}