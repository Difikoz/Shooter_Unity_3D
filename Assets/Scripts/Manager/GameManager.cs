using UnityEngine;

namespace WinterUniverse
{
    public class GameManager : Singleton<GameManager>
    {
        private InputMode _inputMode;
        private AudioManager _audioManager;
        private CameraManager _cameraManager;
        private ConfigsManager _configsManager;
        private LayerManager _layerManager;
        private NPCManager _npcManager;
        private PlayerManager _playerManager;
        private PrefabsManager _prefabsManager;
        private SpawnersManager _spawnersManager;
        private UIManager _uiManager;
        private WorldManager _worldManager;

        public InputMode InputMode => _inputMode;
        public AudioManager AudioManager => _audioManager;
        public CameraManager CameraManager => _cameraManager;
        public ConfigsManager ConfigsManager => _configsManager;
        public LayerManager LayerManager => _layerManager;
        public NPCManager NPCManager => _npcManager;
        public PlayerManager PlayerManager => _playerManager;
        public PrefabsManager PrefabsManager => _prefabsManager;
        public SpawnersManager SpawnersManager => _spawnersManager;
        public UIManager UIManager => _uiManager;
        public WorldManager WorldManager => _worldManager;

        protected override void Awake()
        {
            base.Awake();
            GetComponents();
            InitializeComponents();
        }

        //private void OnDestroy()
        //{
        //    ResetComponents();
        //}

        private void GetComponents()
        {
            _audioManager = GetComponentInChildren<AudioManager>();
            _cameraManager = GetComponentInChildren<CameraManager>();
            _configsManager = GetComponentInChildren<ConfigsManager>();
            _layerManager = GetComponentInChildren<LayerManager>();
            _npcManager = GetComponentInChildren<NPCManager>();
            _playerManager = GetComponentInChildren<PlayerManager>();
            _prefabsManager = GetComponentInChildren<PrefabsManager>();
            _spawnersManager = GetComponentInChildren<SpawnersManager>();
            _uiManager = GetComponentInChildren<UIManager>();
            _worldManager = GetComponentInChildren<WorldManager>();
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

        //private void ResetComponents()
        //{
        //    _uiManager.ResetComponent();
        //    _cameraManager.ResetComponent();
        //    _playerManager.ResetComponent();
        //    _npcManager.ResetComponent();
        //}

        private void Update()
        {
            _spawnersManager.OnUpdate();
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