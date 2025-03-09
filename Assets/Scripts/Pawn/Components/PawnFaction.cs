using System;
using UnityEngine;

namespace WinterUniverse
{
    public class PawnFaction : MonoBehaviour
    {
        public Action OnFactionChanged;

        private PawnController _pawn;
        private FactionConfig _config;

        public FactionConfig Config => _config;

        public void Initialize(PawnData data)
        {
            _pawn = GetComponent<PawnController>();
            Change(GameManager.StaticInstance.ConfigsManager.GetFaction(data.Faction));
        }

        public void Change(FactionConfig config)
        {
            _config = config;
            OnFactionChanged?.Invoke();
        }
    }
}