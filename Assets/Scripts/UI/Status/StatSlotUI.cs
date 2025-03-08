using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace WinterUniverse
{
    public class StatSlotUI : MonoBehaviour, IPointerEnterHandler, ISelectHandler
    {
        [SerializeField] private Button _thisButton;
        [SerializeField] private TMP_Text _infoText;

        private Stat _stat;

        public void Initialize(Stat s)
        {
            _stat = s;
            _infoText.text = $"{_stat.Config.DisplayName}: {_stat.CurrentValue}{(_stat.Config.IsPercent ? "%" : "")}";
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _thisButton.Select();
        }

        public void OnSelect(BaseEventData eventData)
        {
            GameManager.StaticInstance.UIManager.StatusBar.StatsBar.ShowFullInformation(_stat);
        }
    }
}