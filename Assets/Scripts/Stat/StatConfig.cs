using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Stat", menuName = "Winter Universe/Pawn/New Stat")]
    public class StatConfig : BasicInfoConfig
    {
        [SerializeField] private float _baseValue;
        [SerializeField] private bool _clampMinValue;
        [SerializeField] private float _minValue;
        [SerializeField] private bool _clampMaxValue;
        [SerializeField] private float _maxValue;
        [SerializeField] private bool _isPercent;

        public float BaseValue => _baseValue;
        public bool ClampMinValue => _clampMinValue;
        public float MinValue => _minValue;
        public bool ClampMaxValue => _clampMaxValue;
        public float MaxValue => _maxValue;
        public bool IsPercent => _isPercent;
    }
}