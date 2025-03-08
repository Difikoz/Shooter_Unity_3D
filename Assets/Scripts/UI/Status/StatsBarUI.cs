using Lean.Pool;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace WinterUniverse
{
    public class StatsBarUI : MonoBehaviour
    {
        [SerializeField] private Transform _contentRoot;
        [SerializeField] private GameObject _slotPrefab;
        [SerializeField] private Image _InfoBarIconImage;
        [SerializeField] private TMP_Text _infoBarNameText;
        [SerializeField] private TMP_Text _infoBarDescriptionText;

        private List<StatSlotUI> _slots = new();

        public void Initialize()
        {
            for (int i = 0; i < GameManager.StaticInstance.ConfigsManager.Stats.Count; i++)
            {
                _slots.Add(LeanPool.Spawn(_slotPrefab, _contentRoot).GetComponent<StatSlotUI>());
            }
            GameManager.StaticInstance.PlayerManager.Pawn.Status.OnStatsChanged += OnStatsChanged;
            OnStatsChanged();
        }

        public void ResetComponent()
        {
            GameManager.StaticInstance.PlayerManager.Pawn.Status.OnStatsChanged -= OnStatsChanged;
        }

        private void OnStatsChanged()
        {
            int index = 0;
            foreach (Stat s in GameManager.StaticInstance.PlayerManager.Pawn.Status.Stats)
            {
                ShowFullInformation(s);
                _slots[index].Initialize(s);
                index++;
            }
        }

        public void ShowFullInformation(Stat s)
        {
            _InfoBarIconImage.sprite = s.Config.Icon;
            _infoBarNameText.text = s.Config.DisplayName;
            _infoBarDescriptionText.text = s.Config.Description;
        }
    }
}