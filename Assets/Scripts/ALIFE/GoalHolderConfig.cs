using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Goal Holder", menuName = "Winter Universe/Pawn/New Goal Holder")]
    public class GoalHolderConfig : ScriptableObject
    {
        [SerializeField] private string _displayName = "Name";
        [SerializeField] private List<GoalCreator> _goalsToAdd = new();

        public string DisplayName => _displayName;
        public List<GoalCreator> GoalsToAdd => _goalsToAdd;
    }
}