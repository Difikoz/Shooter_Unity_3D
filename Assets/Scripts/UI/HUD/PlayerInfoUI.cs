using UnityEngine;

namespace WinterUniverse
{
    public class PlayerInfoUI : MonoBehaviour
    {
        [SerializeField] private VitalityBarUI _healthBar;
        [SerializeField] private VitalityBarUI _energyBar;

        private EffectsBarUI _effectsBar;

        public EffectsBarUI EffectsBar => _effectsBar;

        public void Initialize()
        {
            _effectsBar = GetComponentInChildren<EffectsBarUI>();
            _healthBar.Initialize();
            _energyBar.Initialize();
            _effectsBar.Initialize();
            GameManager.StaticInstance.PlayerManager.Pawn.Status.OnHealthChanged += _healthBar.SetValues;
            GameManager.StaticInstance.PlayerManager.Pawn.Status.OnEnergyChanged += _energyBar.SetValues;
            GameManager.StaticInstance.PlayerManager.Pawn.Status.RecalculateStats();
        }

        public void ResetComponent()
        {
            _effectsBar.ResetComponent();
            GameManager.StaticInstance.PlayerManager.Pawn.Status.OnHealthChanged -= _healthBar.SetValues;
            GameManager.StaticInstance.PlayerManager.Pawn.Status.OnEnergyChanged -= _energyBar.SetValues;
        }
    }
}