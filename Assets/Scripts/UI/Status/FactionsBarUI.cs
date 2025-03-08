using Lean.Pool;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace WinterUniverse
{
    public class FactionsBarUI : MonoBehaviour
    {
        [SerializeField] private Transform _contentRoot;
        [SerializeField] private GameObject _slotPrefab;
        [SerializeField] private TMP_Text _playerFactionNameText;
        [SerializeField] private Image _infoBarIconImage;
        [SerializeField] private TMP_Text _infoBarNameText;
        [SerializeField] private TMP_Text _infoBarDescriptionText;

        private List<FactionSlotUI> _slots = new();

        public void Initialize()
        {
            for (int i = 0; i < GameManager.StaticInstance.ConfigsManager.Factions.Count; i++)
            {
                _slots.Add(LeanPool.Spawn(_slotPrefab, _contentRoot).GetComponent<FactionSlotUI>());
            }
            GameManager.StaticInstance.PlayerManager.Pawn.Faction.OnFactionChanged += OnFactionChanged;
            OnFactionChanged();
        }

        public void ResetComponent()
        {
            GameManager.StaticInstance.PlayerManager.Pawn.Faction.OnFactionChanged -= OnFactionChanged;
        }

        private void OnFactionChanged()
        {
            _playerFactionNameText.text = GameManager.StaticInstance.PlayerManager.Pawn.Faction.Config.DisplayName;
            int index = 0;
            foreach (FactionRelationship fr in GameManager.StaticInstance.PlayerManager.Pawn.Faction.Config.Relationships)
            {
                ShowFullInformation(fr);
                _slots[index].Initialize(fr);
                index++;
            }
        }

        public void ShowFullInformation(FactionRelationship relationship)
        {
            _infoBarIconImage.sprite = relationship.Faction.Icon;
            _infoBarNameText.text = relationship.Faction.DisplayName;
            _infoBarDescriptionText.text = relationship.Faction.Description;
        }
    }
}