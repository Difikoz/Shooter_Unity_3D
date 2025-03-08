using UnityEngine;

namespace WinterUniverse
{
    [System.Serializable]
    public class GoalCreator
    {
        [SerializeField] private GoalConfig _config;
        [SerializeField] private int _priority;

        public GoalConfig Config => _config;
        public int Priority => _priority;
    }
}