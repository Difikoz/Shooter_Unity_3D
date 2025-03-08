using System.Collections.Generic;

namespace WinterUniverse
{
    public class GoalHolder
    {
        private GoalConfig _config;
        private StateHolder _requiredStates;

        public GoalConfig Config => _config;
        public StateHolder RequiredStates => _requiredStates;

        public GoalHolder(GoalConfig config)
        {
            _config = config;
            _requiredStates = new();
            foreach (StateCreator creator in _config.RequiredStates)
            {
                _requiredStates.SetState(creator.Config.ID, creator.Value);
            }
        }
    }
}