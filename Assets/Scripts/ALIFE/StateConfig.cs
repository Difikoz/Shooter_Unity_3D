using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "State", menuName = "Winter Universe/ALIFE/New State")]
    public class StateConfig : ScriptableObject
    {
        [SerializeField] private string _id;

        public string ID => _id;
    }
}