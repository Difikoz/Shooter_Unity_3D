using Lean.Pool;
using UnityEngine;

namespace WinterUniverse
{
    public class PawnController : MonoBehaviour
    {
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
            LeanPool.Spawn(GameManager.StaticInstance.ConfigsManager.GetVisual(data.Visual).Model, transform);
            GetComponents();
            InitializeComponents(data);
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

        private void InitializeComponents(PawnData data)
        {
            _stateHolder = new();
            foreach (StateConfig config in GameManager.StaticInstance.ConfigsManager.PawnStates)
            {
                _stateHolder.SetState(config.ID, false);
            }
            foreach (StateCreator creator in GameManager.StaticInstance.ConfigsManager.GetStateHolder(data.StateHolder).StatesToChange)
            {
                _stateHolder.SetState(creator.Config.ID, creator.Value);
            }
            _animator.Initialize();
            _combat.Initialize();
            _detection.Initialize();
            _effects.Initialize();
            _equipment.Initialize();
            _faction.Initialize(data);
            _input.Initialize();
            _inventory.Initialize(data);
            _locomotion.Initialize();
            _sound.Initialize(data);
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