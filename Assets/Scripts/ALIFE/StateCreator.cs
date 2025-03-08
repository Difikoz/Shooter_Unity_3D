using UnityEngine;

namespace WinterUniverse
{
    [System.Serializable]
    public class StateCreator
    {
        [SerializeField] private StateConfig _config;
        [SerializeField] private bool _value;

        public StateConfig Config => _config;
        public bool Value => _value;

        public StateCreator(StateConfig config, bool value)
        {
            _config = config;
            _value = value;
        }
    }
}