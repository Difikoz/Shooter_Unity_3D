using UnityEngine;

namespace WinterUniverse
{
    public class StatusBarUI : MonoBehaviour
    {
        private TabGroup _tabGroup;
        private StatsBarUI _statsBar;
        private InventoryBarUI _inventoryBar;
        private JournalBarUI _journalBar;
        private FactionsBarUI _factionsBar;
        private MapBarUI _mapBar;

        public TabGroup TabGroup => _tabGroup;
        public StatsBarUI StatsBar => _statsBar;
        public InventoryBarUI InventoryBar => _inventoryBar;
        public JournalBarUI JournalBar => _journalBar;
        public FactionsBarUI FactionsBar => _factionsBar;
        public MapBarUI MapBar => _mapBar;

        public void Initialize()
        {
            _tabGroup = GetComponent<TabGroup>();
            _statsBar = GetComponentInChildren<StatsBarUI>();
            _inventoryBar = GetComponentInChildren<InventoryBarUI>();
            _journalBar = GetComponentInChildren<JournalBarUI>();
            _factionsBar = GetComponentInChildren<FactionsBarUI>();
            _mapBar = GetComponentInChildren<MapBarUI>();
            _tabGroup.Initialize();
            _statsBar.Initialize();
            _inventoryBar.Initialize();
            _journalBar.Initialize();
            _factionsBar.Initialize();
            _mapBar.Initialize();
            gameObject.SetActive(false);
        }

        public void ResetComponent()
        {
            _statsBar.ResetComponent();
            _inventoryBar.ResetComponent();
            _journalBar.ResetComponent();
            _factionsBar.ResetComponent();
            _mapBar.ResetComponent();
        }
    }
}