using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "State Holder", menuName = "Winter Universe/Pawn/New State Holder")]
    public class StateHolderConfig : ScriptableObject
    {
        [SerializeField] private string _displayName = "Name";
        [SerializeField] private List<StateCreator> _statesToChange = new();

        public string DisplayName => _displayName;
        public List<StateCreator> StatesToChange => _statesToChange;
    }
}