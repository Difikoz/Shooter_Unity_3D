using UnityEngine;

namespace WinterUniverse
{
    [System.Serializable]
    public class PlayerData
    {
        public string Weapon;
        public string Armor;
        public SerializableDictionary<string, int> Stacks;
        public TransformValues Transform;
    }
}