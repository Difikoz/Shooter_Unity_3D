using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class SpawnerItem : SpawnerBase
    {
        [SerializeField] private List<ItemStack> _stacksToSpawn = new();

        protected override void OnSpawn()
        {
            int amount = Random.Range(_minAmount, _maxAmount + 1);
            for (int i = 0; i < amount; i++)
            {
                InteractableItem item = GameManager.StaticInstance.PrefabsManager.GetItem(_spawnPoints[Random.Range(0, _spawnPoints.Count)]);
                item.Initialize(_stacksToSpawn[Random.Range(0, _stacksToSpawn.Count)]);
            }
        }
    }
}