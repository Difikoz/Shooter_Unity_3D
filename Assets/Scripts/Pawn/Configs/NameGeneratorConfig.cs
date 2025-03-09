using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Name Generator", menuName = "Winter Universe/Pawn/New Name Generator")]
    public class NameGeneratorConfig : ScriptableObject
    {
        [SerializeField] private List<string> _displayNames = new();

        public string GetDisplayName()
        {
            return _displayNames[Random.Range(0, _displayNames.Count)];
        }
    }
}