using UnityEngine;

namespace WinterUniverse
{
    public class WorldManager : MonoBehaviour
    {
        [SerializeField] private StateHolderConfig _stateHolderConfig;

        private StateHolder _stateHolder;

        public StateHolder StateHolder => _stateHolder;

        public void Initialize()
        {
            _stateHolder = new();
            foreach (StateConfig config in GameManager.StaticInstance.ConfigsManager.WorldStates)
            {
                _stateHolder.SetState(config.ID, false);
            }
            foreach (StateCreator creator in _stateHolderConfig.StatesToChange)
            {
                _stateHolder.SetState(creator.Config.ID, creator.Value);
            }
        }
    }
}