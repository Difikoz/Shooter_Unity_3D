using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [System.Serializable]
    public class PlayerData
    {
        public string Weapon;
        public List<string> Armors;
        public SerializableDictionary<string, int> Stacks;
        public TransformValues Transform;
    }
}