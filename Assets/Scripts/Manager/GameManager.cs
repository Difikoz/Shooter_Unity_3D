using UnityEngine;

namespace WinterUniverse
{
    public class GameManager : Singleton<GameManager>
    {
        private InputMode _inputMode;
        private PlayerManager _playerManager;
        private NPCManager _npcManager;
        private CameraManager _cameraManager;
        private AudioManager _audioManager;
        private ConfigsManager _configsManager;
        private LayerManager _layerManager;
        private PrefabsManager _prefabsManager;
        private SpawnersManager _spawnersManager;
        private WorldManager _worldManager;
        private UIManager _uiManager;

        public InputMode InputMode => _inputMode;
        public PlayerManager PlayerManager => _playerManager;
        public NPCManager NPCManager => _npcManager;
        public CameraManager CameraManager => _cameraManager;
        public AudioManager AudioManager => _audioManager;
        public ConfigsManager ConfigsManager => _configsManager;
        public LayerManager LayerManager => _layerManager;
        public PrefabsManager PrefabsManager => _prefabsManager;
        public SpawnersManager SpawnersManager => _spawnersManager;
        public WorldManager WorldManager => _worldManager;
        public UIManager UIManager => _uiManager;

        protected override void Awake()
        {
            base.Awake();
            GetComponents();
            InitializeComponents();
        }

        //private void OnDestroy()
        //{
        //    _uiManager.ResetComponent();
        //    _cameraManager.ResetComponent();
        //    _playerManager.ResetComponent();
        //    _aiManager.ResetComponent();
        //}

        private void GetComponents()
        {
            _playerManager = GetComponentInChildren<PlayerManager>();
            _npcManager = GetComponentInChildren<NPCManager>();
            _cameraManager = GetComponentInChildren<CameraManager>();
            _audioManager = GetComponentInChildren<AudioManager>();
            _configsManager = GetComponentInChildren<ConfigsManager>();
            _layerManager = GetComponentInChildren<LayerManager>();
            _prefabsManager = GetComponentInChildren<PrefabsManager>();
            _spawnersManager = GetComponentInChildren<SpawnersManager>();
            _worldManager = GetComponentInChildren<WorldManager>();
            _uiManager = GetComponentInChildren<UIManager>();
        }

        private void InitializeComponents()
        {
            _audioManager.Initialize();
            _configsManager.Initialize();
            _worldManager.Initialize();
            _playerManager.Initialize();
            _npcManager.Initialize();
            _spawnersManager.Initialize();
            _cameraManager.Initialize();
            _uiManager.Initialize();
            SetInputMode(InputMode.Game);
        }

        private void Update()
        {
            _playerManager.OnUpdate();
            _npcManager.OnUpdate();
            _cameraManager.OnUpdate();
        }

        public void SetInputMode(InputMode mode)
        {
            _inputMode = mode;
            switch (_inputMode)
            {
                case InputMode.Game:
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                    break;
                case InputMode.UI:
                    Cursor.lockState = CursorLockMode.Confined;
                    Cursor.visible = true;
                    break;
            }
        }
    }
}