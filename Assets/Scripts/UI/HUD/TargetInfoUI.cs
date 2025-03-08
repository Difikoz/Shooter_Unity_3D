using UnityEngine;
using UnityEngine.UI;

namespace WinterUniverse
{
    public class TargetInfoUI : MonoBehaviour
    {
        [SerializeField] private Image _pawnIconImage;
        [SerializeField] private VitalityBarUI _healthBar;
        [SerializeField] private VitalityBarUI _energyBar;

        private PawnController _pawn;
        private EffectsBarUI _effectsBar;

        public EffectsBarUI EffectsBar => _effectsBar;

        public void Initialize(PawnController pawn)
        {
            _pawn = pawn;
            _effectsBar = GetComponentInChildren<EffectsBarUI>();
            _pawnIconImage.sprite = GameManager.StaticInstance.ConfigsManager.GetVisual(_pawn.Data.Visual).Icon;
            _healthBar.Initialize();
            _energyBar.Initialize();
            _effectsBar.Initialize(_pawn);
            _pawn.Status.OnHealthChanged += _healthBar.SetValues;
            _pawn.Status.OnEnergyChanged += _energyBar.SetValues;
            _pawn.Status.RecalculateStats();
        }

        public void ResetComponent()
        {
            _effectsBar.ResetComponent();
            _pawn.Status.OnHealthChanged -= _healthBar.SetValues;
            _pawn.Status.OnEnergyChanged -= _energyBar.SetValues;
        }
    }
}