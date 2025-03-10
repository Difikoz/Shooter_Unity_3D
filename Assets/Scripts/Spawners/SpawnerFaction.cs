using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class SpawnerFaction : SpawnerBase
    {
        [SerializeField] private int _limitAmount = 10;
        [SerializeField] private FactionConfig _faction;

        private List<NPCController> _spawnedControllers = new();

        protected override void OnSpawn()
        {
            if (_spawnedControllers.Count > 0)
            {
                for (int i = _spawnedControllers.Count - 1; i >= 0; i--)
                {
                    if (_spawnedControllers[i].Pawn.StateHolder.CompareStateValue("Is Dead", true))
                    {
                        GameManager.StaticInstance.NPCManager.RemoveController(_spawnedControllers[i]);
                        _spawnedControllers[i].ResetComponent();
                        GameManager.StaticInstance.PrefabsManager.DespawnObject(_spawnedControllers[i].gameObject, 1f);
                        _spawnedControllers.RemoveAt(i);
                    }
                }
            }
            int amount = Random.Range(_minAmount, _maxAmount + 1);
            for (int i = 0; i < amount; i++)
            {
                if (_spawnedControllers.Count == _limitAmount)
                {
                    break;
                }
                NPCController npc = GameManager.StaticInstance.PrefabsManager.GetNPC(_spawnPoints[Random.Range(0, _spawnPoints.Count)]);
                GameManager.StaticInstance.NPCManager.AddController(npc);
                _spawnedControllers.Add(npc);
                npc.Initialize(_faction.MemberConfigs[Random.Range(0, _faction.MemberConfigs.Count)].GetPawnData(), _faction.MemberConfigs[Random.Range(0, _faction.MemberConfigs.Count)].GetNPCData());
            }
        }
    }
}