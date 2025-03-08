using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Goal", menuName = "Winter Universe/ALIFE/New Goal")]
    public class GoalConfig : ScriptableObject
    {
        [SerializeField] private string _displayName = "Name";
        [SerializeField] private bool _repeatable = true;
        [SerializeField] private List<StateCreator> _requiredStates = new();

        public string DisplayName => _displayName;
        public bool Repeatable => _repeatable;
        public List<StateCreator> RequiredStates => _requiredStates;
    }
}