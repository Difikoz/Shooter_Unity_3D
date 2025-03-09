using UnityEngine;

namespace WinterUniverse
{
    public class UIManager : MonoBehaviour
    {
        private PlayerInputActions _inputActions;
        private HUDUI _hud;
        private StatusBarUI _statusBar;

        public HUDUI HUD => _hud;
        public StatusBarUI StatusBar => _statusBar;

        public void Initialize()
        {
            _inputActions = new();
            _inputActions.Enable();
            _inputActions.UI.Status.performed += ctx => ToggleStatusBar();
            _inputActions.UI.Previous.performed += ctx => PreviousTab();
            _inputActions.UI.Next.performed += ctx => NextTab();
            GetComponents();
            InitializeComponents();
        }

        public void ResetComponent()
        {
            _inputActions.UI.Status.performed -= ctx => ToggleStatusBar();
            _inputActions.UI.Previous.performed -= ctx => PreviousTab();
            _inputActions.UI.Next.performed -= ctx => NextTab();
            _inputActions.Disable();
            _hud.ResetComponent();
            _statusBar.ResetComponent();
        }

        private void GetComponents()
        {
            _hud = GetComponentInChildren<HUDUI>();
            _statusBar = GetComponentInChildren<StatusBarUI>();
        }

        private void InitializeComponents()
        {
            _hud.Initialize();
            _statusBar.Initialize();
        }

        private void ToggleStatusBar()
        {
            if (_statusBar.isActiveAndEnabled)
            {
                _statusBar.gameObject.SetActive(false);
                GameManager.StaticInstance.SetInputMode(InputMode.Game);
                GameManager.StaticInstance.PlayerManager.Pawn.StateHolder.SetState("Has New Items", false);
            }
            else
            {
                _statusBar.gameObject.SetActive(true);
                GameManager.StaticInstance.SetInputMode(InputMode.UI);
            }
        }

        //private void ToggleHUD()
        //{
        //    if (_hud.isActiveAndEnabled)
        //    {
        //        _hud.gameObject.SetActive(false);
        //    }
        //    else
        //    {
        //        _hud.gameObject.SetActive(true);
        //    }
        //}

        private void PreviousTab()
        {
            if (_statusBar.isActiveAndEnabled)
            {
                _statusBar.TabGroup.PreviousTab();
            }
        }

        private void NextTab()
        {
            if (_statusBar.isActiveAndEnabled)
            {
                _statusBar.TabGroup.NextTab();
            }
        }
    }
}