using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace WinterUniverse
{
    public class EffectSlotUI : MonoBehaviour
    {
        [SerializeField] private Button _thisButton;
        [SerializeField] private Image _iconImage;
        [SerializeField] private TMP_Text _durationText;

        private Effect _effect;

        public void Initialize(Effect effect)
        {
            _effect = effect;
            _iconImage.sprite = _effect.Config.Icon;
            _durationText.text = $"{_effect.Duration:0.0}";
        }
    }
}