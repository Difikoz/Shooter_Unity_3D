using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Reduce Health Over Time", menuName = "Winter Universe/Effect/New Reduce Health Over Time")]
    public class ReduceHealthOverTimeEffectConfig : EffectConfig
    {
        [SerializeField] private ElementConfig _element;

        private void OnValidate()
        {
            _effectType = EffectType.ReduceHealthOverTime;
        }

        public override Effect CreateEffect(PawnController target, PawnController source, float value, float duration)
        {
            return new ReduceHealthOverTimeEffect(this, target, source, value, duration, _element);
        }
    }
}