using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class SpawnerFaction : SpawnerBase
    {
        [SerializeField] private FactionConfig _faction;
        [SerializeField] private List<VisualConfig> _visuals = new();
        [SerializeField] private List<InventoryConfig> _inventories = new();
        [SerializeField] private List<StateHolderConfig> _stateHolders = new();
        [SerializeField] private List<GoalHolderConfig> _goalHolders = new();

        private PawnData _pawnData;
        private NPCData _npcData;

        protected override void OnSpawn()
        {
            _pawnData = new();
            _npcData = new();
            int amount = Random.Range(_minAmount, _maxAmount + 1);
            for (int i = 0; i < amount; i++)
            {
                _pawnData.DisplayName = $"{_faction.MemberName}";
                _pawnData.Visual = _visuals[Random.Range(0, _visuals.Count)].DisplayName;
                _pawnData.Faction = _faction.DisplayName;
                _pawnData.Inventory = _inventories[Random.Range(0, _inventories.Count)].DisplayName;
                _pawnData.StateHolder = _stateHolders[Random.Range(0, _stateHolders.Count)].DisplayName;
                _npcData.GoalHolder = _goalHolders[Random.Range(0, _goalHolders.Count)].DisplayName;
                NPCController npc = GameManager.StaticInstance.PrefabsManager.GetNPC(_spawnPoints[Random.Range(0, _spawnPoints.Count)]);
                GameManager.StaticInstance.NPCManager.AddController(npc);
                npc.Initialize(_pawnData, _npcData);
            }
        }
    }
}