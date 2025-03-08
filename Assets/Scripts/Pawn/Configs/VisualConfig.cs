using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Visual", menuName = "Winter Universe/Pawn/New Visual")]
    public class VisualConfig : BasicInfoConfig
    {
        [SerializeField] private GameObject _model;

        public GameObject Model => _model;
    }
}