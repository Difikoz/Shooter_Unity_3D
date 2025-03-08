using UnityEngine;

namespace WinterUniverse
{
    public class HUDUI : MonoBehaviour
    {
        [SerializeField] private TargetInfoUI _playerInfo;
        [SerializeField] private TargetInfoUI _targetInfo;

        public TargetInfoUI EffectsBar => _playerInfo;
        public TargetInfoUI TargetInfo => _targetInfo;

        public void Initialize()
        {
            _playerInfo.Initialize(GameManager.StaticInstance.PlayerManager.Pawn);
            _targetInfo.gameObject.SetActive(false);
            GameManager.StaticInstance.PlayerManager.Pawn.Combat.OnTargetChanged += OnTargetChanged;
        }

        public void ResetComponent()
        {
            GameManager.StaticInstance.PlayerManager.Pawn.Combat.OnTargetChanged -= OnTargetChanged;
            _playerInfo.ResetComponent();
        }

        private void OnTargetChanged()
        {
            if (_targetInfo.isActiveAndEnabled)
            {
                _targetInfo.ResetComponent();
            }
            if (GameManager.StaticInstance.PlayerManager.Pawn.Combat.Target != null)
            {
                _targetInfo.gameObject.SetActive(true);
                _targetInfo.Initialize(GameManager.StaticInstance.PlayerManager.Pawn.Combat.Target);
            }
            else
            {
                _targetInfo.gameObject.SetActive(false);
            }
        }
    }
}