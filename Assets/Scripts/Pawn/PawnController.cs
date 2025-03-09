using Lean.Pool;
using UnityEngine;

namespace WinterUniverse
{
    public class PawnController : MonoBehaviour
    {
        private PawnData _data;
        private PawnAnimator _animator;
        private PawnCombat _combat;
        private PawnDetection _detection;
        private PawnEffects _effects;
        private PawnEquipment _equipment;
        private PawnFaction _faction;
        private PawnInput _input;
        private PawnInventory _inventory;
        private PawnLocomotion _locomotion;
        private PawnSound _sound;
        private PawnStatus _status;
        private StateHolder _stateHolder;

        public PawnData Data => _data;
        public PawnAnimator Animator => _animator;
        public PawnCombat Combat => _combat;
        public PawnDetection Detection => _detection;
        public PawnEffects Effects => _effects;
        public PawnEquipment Equipment => _equipment;
        public PawnFaction Faction => _faction;
        public PawnInput Input => _input;
        public PawnInventory Inventory => _inventory;
        public PawnLocomotion Locomotion => _locomotion;
        public PawnSound Sound => _sound;
        public PawnStatus Status => _status;
        public StateHolder StateHolder => _stateHolder;

        public void Create(PawnData data)
        {
            _data = new()
            {
                DisplayName = data.DisplayName,
                Visual = data.Visual,
                Faction = data.Faction,
                Inventory = data.Inventory,
                StateHolder = data.StateHolder
            };
            LeanPool.Spawn(GameManager.StaticInstance.ConfigsManager.GetVisual(_data.Visual).Model, transform);
            GetComponents();
            InitializeComponents();
        }

        private void GetComponents()
        {
            _animator = GetComponentInChildren<PawnAnimator>();
            _combat = GetComponent<PawnCombat>();
            _detection = GetComponent<PawnDetection>();
            _effects = GetComponent<PawnEffects>();
            _equipment = GetComponentInChildren<PawnEquipment>();
            _faction = GetComponent<PawnFaction>();
            _input = GetComponent<PawnInput>();
            _inventory = GetComponent<PawnInventory>();
            _locomotion = GetComponent<PawnLocomotion>();
            _sound = GetComponentInChildren<PawnSound>();
            _status = GetComponent<PawnStatus>();
        }

        private void InitializeComponents()
        {
            _stateHolder = new();
            foreach (StateConfig config in GameManager.StaticInstance.ConfigsManager.PawnStates)
            {
                _stateHolder.SetState(config.ID, false);
            }
            foreach (StateCreator creator in GameManager.StaticInstance.ConfigsManager.GetStateHolder(_data.StateHolder).StatesToChange)
            {
                _stateHolder.SetState(creator.Config.ID, creator.Value);
            }
            _animator.Initialize();
            _combat.Initialize();
            _detection.Initialize();
            _effects.Initialize();
            _equipment.Initialize();
            _faction.Initialize();
            _input.Initialize();
            _inventory.Initialize();
            _locomotion.Initialize();
            _sound.Initialize();
            _status.Initialize();
        }

        public void ResetComponents()
        {
            _animator.ResetComponent();
            _equipment.ResetComponent();
            _inventory.ResetComponent();
            _locomotion.ResetComponent();
            _status.ResetComponent();
            LeanPool.Despawn(_animator.gameObject);
        }

        public void OnUpdate()
        {
            _animator.OnUpdate();
            _combat.OnUpdate();
            _detection.OnUpdate();
            _effects.OnUpdate();
            _input.OnUpdate();
            _locomotion.OnUpdate();
            _status.OnUpdate();
        }
    }
}