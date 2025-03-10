using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class SpawnersManager : MonoBehaviour
    {
        private List<SpawnerBase> _spawners = new();

        public void Initialize()
        {
            SpawnerBase[] spawners = FindObjectsByType<SpawnerBase>(FindObjectsSortMode.None);
            foreach (SpawnerBase spawner in spawners)
            {
                _spawners.Add(spawner);
                spawner.Initialize();
            }
        }

        public void OnUpdate()
        {
            foreach (SpawnerBase spawner in _spawners)
            {
                spawner.OnUpdate();
            }
        }
    }
}