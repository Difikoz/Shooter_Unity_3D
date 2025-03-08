using Lean.Pool;
using UnityEngine;

namespace WinterUniverse
{
    public class EffectsBarUI : MonoBehaviour
    {
        [SerializeField] private Transform _contentRoot;
        [SerializeField] private GameObject _slotPrefab;

        private PawnController _pawn;

        public void Initialize(PawnController pawn)
        {
            _pawn = pawn;
            _pawn.Effects.OnEffectsChanged += OnEffectsChanged;
            OnEffectsChanged();
        }

        public void ResetComponent()
        {
            _pawn.Effects.OnEffectsChanged -= OnEffectsChanged;
        }

        private void OnEffectsChanged()
        {
            while (_contentRoot.childCount > 0)
            {
                LeanPool.Despawn(_contentRoot.GetChild(0).gameObject);
            }
            foreach (Effect effect in _pawn.Effects.AllEffects)
            {
                LeanPool.Spawn(_slotPrefab, _contentRoot).GetComponent<EffectSlotUI>().Initialize(effect);
            }
        }
    }
}