using UnityEngine;

namespace WinterUniverse
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private PlayerConfig _playerConfig;

        private PlayerInputActions _inputActions;
        private PawnController _pawn;
        private Vector2 _moveInput;

        public PawnController Pawn => _pawn;

        public void Initialize()
        {
            _inputActions = new();
            _inputActions.Enable();
            _inputActions.Player.Jump.performed += ctx => OnJump();
            _inputActions.Player.Interact.performed += ctx => OnInteract();
            Initialize(_playerConfig.GetPawnData(), _playerConfig.GetPlayerData());// for test
        }

        public void Initialize(PawnData pawnData, PlayerData playerData)
        {
            InitializePawn(pawnData);
            LoadData(playerData);
        }

        public void InitializePawn(PawnData data)
        {
            _pawn = GameManager.StaticInstance.PrefabsManager.GetPawn(transform);
            _pawn.Create(data);
        }

        public void ResetComponent()
        {
            _inputActions.Player.Jump.performed -= ctx => OnJump();
            _inputActions.Player.Interact.performed -= ctx => OnInteract();
            _inputActions.Disable();
            _pawn.ResetComponents();
        }

        public void OnUpdate()
        {
            if (GameManager.StaticInstance.InputMode == InputMode.Game)
            {
                _moveInput = _inputActions.Player.Move.ReadValue<Vector2>();
                _pawn.Input.FireInput = _inputActions.Player.Fire.IsPressed();
                _pawn.Input.AimInput = _inputActions.Player.Aim.IsPressed();
            }
            else
            {
                _moveInput = Vector2.zero;
                _pawn.Input.FireInput = false;
                _pawn.Input.AimInput = false;
            }
            _pawn.Input.MoveDirection = GameManager.StaticInstance.CameraManager.transform.forward * _moveInput.y + GameManager.StaticInstance.CameraManager.transform.right * _moveInput.x;
            _pawn.Input.LookDirection = GameManager.StaticInstance.CameraManager.transform.forward;
            _pawn.Input.LookPoint = GameManager.StaticInstance.CameraManager.GetHitPoint();
            _pawn.OnUpdate();
        }

        private void OnJump()
        {
            if (GameManager.StaticInstance.InputMode == InputMode.UI || _pawn == null)
            {
                return;
            }
            _pawn.Locomotion.Jump();
        }

        private void OnInteract()
        {
            if (GameManager.StaticInstance.InputMode == InputMode.UI || _pawn == null)
            {
                return;
            }
        }

        public void SaveData(ref PlayerData data)
        {
            data.Weapon = _pawn.Equipment.WeaponSlot.Config != null ? _pawn.Equipment.WeaponSlot.Config.DisplayName : "Empty";
            data.Armor = _pawn.Equipment.ArmorSlot.Config != null ? _pawn.Equipment.ArmorSlot.Config.DisplayName : "Empty";
            data.Stacks = new();
            foreach (ItemStack stack in _pawn.Inventory.Stacks)
            {
                if (data.Stacks.ContainsKey(stack.Item.DisplayName))
                {
                    data.Stacks[stack.Item.DisplayName] += stack.Amount;
                }
                else
                {
                    data.Stacks.Add(stack.Item.DisplayName, stack.Amount);
                }
            }
            data.Transform.SetPositionAndRotation(_pawn.transform.position, _pawn.transform.eulerAngles);

        }

        public void LoadData(PlayerData data)
        {
            _pawn.transform.SetPositionAndRotation(data.Transform.GetPosition(), data.Transform.GetRotation());
            _pawn.Inventory.Initialize(data.Stacks);
            _pawn.Equipment.EquipWeapon(GameManager.StaticInstance.ConfigsManager.GetWeapon(data.Weapon), false, false);
            _pawn.Equipment.EquipArmor(GameManager.StaticInstance.ConfigsManager.GetArmor(data.Armor), false, false);
        }
    }
}