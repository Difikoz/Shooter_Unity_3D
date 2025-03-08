using System;
using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class PawnEffects : MonoBehaviour
    {
        public Action OnEffectsChanged;

        private PawnController _pawn;
        private List<Effect> _allEffects = new();

        [SerializeField] private float _tickCooldown = 0.5f;

        private float _tickTime;

        public List<Effect> AllEffects => _allEffects;

        public void Initialize()
        {
            _pawn = GetComponent<PawnController>();
            _allEffects.Clear();
        }

        public void OnUpdate()
        {
            if (_tickTime >= _tickCooldown)
            {
                HandleEffects();
                _tickTime = 0f;
            }
            else
            {
                _tickTime += Time.deltaTime;
            }
        }

        public void ApplyEffects(List<EffectCreator> effects, PawnController source)
        {
            foreach (EffectCreator effect in effects)
            {
                if (effect.Triggered)
                {
                    AddEffect(effect.Config.CreateEffect(_pawn, source, effect.Value, effect.Duration));
                }
            }
        }

        public void AddEffect(Effect effect)
        {
            _allEffects.Add(effect);
        }

        public void RemoveEffect(Effect effect)
        {
            if (_allEffects.Contains(effect))
            {
                _allEffects.Remove(effect);
            }
        }

        private void HandleEffects()
        {
            for (int i = _allEffects.Count - 1; i >= 0; i--)
            {
                _allEffects[i].OnTick(_tickCooldown);
            }
            OnEffectsChanged?.Invoke();
        }
    }
}