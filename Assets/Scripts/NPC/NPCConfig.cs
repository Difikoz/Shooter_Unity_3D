using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "NPC", menuName = "Winter Universe/Data/New NPC")]
    public class NPCConfig : ScriptableObject
    {
        [SerializeField] private List<NameGeneratorConfig> _nameGenerators = new();
        [SerializeField] private List<VisualConfig> _visuals = new();
        [SerializeField] private List<VoiceConfig> _voices = new();
        [SerializeField] private List<InventoryConfig> _inventories = new();
        [SerializeField] private List<StateHolderConfig> _stateHolders = new();
        [SerializeField] private List<GoalHolderConfig> _goalHolders = new();

        public PawnData GetPawnData()
        {
            PawnData data = new()
            {
                DisplayName = _nameGenerators[Random.Range(0, _nameGenerators.Count)].GetDisplayName(),
                Visual = _visuals[Random.Range(0, _visuals.Count)].DisplayName,
                Voice = _voices[Random.Range(0, _voices.Count)].DisplayName,
                Inventory = _inventories[Random.Range(0, _inventories.Count)].DisplayName,
                StateHolder = _stateHolders[Random.Range(0, _stateHolders.Count)].DisplayName,
            };
            return data;
        }

        public NPCData GetNPCData()
        {
            NPCData data = new()
            {
                GoalHolder = _goalHolders[Random.Range(0, _goalHolders.Count)].DisplayName,
            };
            return data;
        }
    }
}