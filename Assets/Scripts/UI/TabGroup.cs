using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class TabGroup : MonoBehaviour
    {
        [SerializeField] private Sprite _backgroundActivated;
        [SerializeField] private Sprite _backgroundHovered;
        [SerializeField] private Sprite _backgroundDeactivated;
        [SerializeField] private List<TabButton> _buttons = new();
        [SerializeField] private List<GameObject> _pages = new();

        private int _currentPageIndex;
        private TabButton _selectedTab;

        public void Initialize()
        {
            OnTabButtonPressed(_buttons[0]);
        }

        public void OnTabButtonEntered(TabButton button)
        {
            if (_selectedTab != null && _selectedTab == button)
            {
                return;
            }
            button.Background.sprite = _backgroundHovered;
        }

        public void OnTabButtonExited(TabButton button)
        {
            if (_selectedTab != null && _selectedTab == button)
            {
                return;
            }
            button.Background.sprite = _backgroundDeactivated;
        }

        public void OnTabButtonPressed(TabButton button)
        {
            if (_selectedTab != null)
            {
                _selectedTab.OnDeselect();
            }
            _selectedTab = button;
            _selectedTab.OnPressed();
            ResetTabs();
            button.Background.sprite = _backgroundActivated;
            _currentPageIndex = button.transform.GetSiblingIndex();
            for (int i = 0; i < _pages.Count; i++)
            {
                _pages[i].SetActive(i == _currentPageIndex);
            }
        }

        public void ResetTabs()
        {
            foreach (TabButton button in _buttons)
            {
                if (_selectedTab != null && button == _selectedTab)
                {
                    continue;
                }
                button.Background.sprite = _backgroundDeactivated;
            }
        }

        public void PreviousTab()
        {
            if (_currentPageIndex > 0)
            {
                OnTabButtonPressed(_buttons[_currentPageIndex - 1]);
            }
            else
            {
                OnTabButtonPressed(_buttons[_buttons.Count - 1]);
            }
        }

        public void NextTab()
        {
            if (_currentPageIndex < _buttons.Count - 1)
            {
                OnTabButtonPressed(_buttons[_currentPageIndex + 1]);
            }
            else
            {
                OnTabButtonPressed(_buttons[0]);
            }
        }
    }
}