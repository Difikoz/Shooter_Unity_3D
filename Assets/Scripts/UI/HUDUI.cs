using UnityEngine;

namespace WinterUniverse
{
    public class HUDUI : MonoBehaviour
    {
        private PlayerInfoUI _playerInfo;

        public PlayerInfoUI EffectsBar => _playerInfo;

        public void Initialize()
        {
            _playerInfo = GetComponentInChildren<PlayerInfoUI>();
            _playerInfo.Initialize();
        }

        public void ResetComponent()
        {
            _playerInfo.ResetComponent();
        }
    }
}