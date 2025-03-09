using Lean.Pool;
using UnityEngine;

namespace WinterUniverse
{
    public class EffectsBarUI : MonoBehaviour
    {
        [SerializeField] private Transform _contentRoot;
        [SerializeField] private GameObject _slotPrefab;

        public void Initialize()
        {
            GameManager.StaticInstance.PlayerManager.Pawn.Effects.OnEffectsChanged += OnEffectsChanged;
            OnEffectsChanged();
        }

        public void ResetComponent()
        {
            GameManager.StaticInstance.PlayerManager.Pawn.Effects.OnEffectsChanged -= OnEffectsChanged;
        }

        private void OnEffectsChanged()
        {
            while (_contentRoot.childCount > 0)
            {
                LeanPool.Despawn(_contentRoot.GetChild(0).gameObject);
            }
            foreach (Effect effect in GameManager.StaticInstance.PlayerManager.Pawn.Effects.AllEffects)
            {
                LeanPool.Spawn(_slotPrefab, _contentRoot).GetComponent<EffectSlotUI>().Initialize(effect);
            }
        }
    }
}