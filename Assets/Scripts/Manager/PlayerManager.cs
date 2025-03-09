using UnityEngine;
using UnityEngine.InputSystem;

namespace WinterUniverse
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private PlayerConfig _playerConfig;

        private PawnController _pawn;
        private Vector2 _moveInput;

        public PawnController Pawn => _pawn;

        public void OnMove(InputValue value)
        {
            _moveInput = value.Get<Vector2>();
        }

        public void OnInteract()
        {

        }

        public void OnFire(InputValue value)
        {
            _pawn.Input.FireInput = value.isPressed;
        }

        public void OnAim(InputValue value)
        {
            _pawn.Input.AimInput = value.isPressed;
        }

        public void Initialize()
        {
            Initialize(_playerConfig.GetPawnData(), _playerConfig.GetPlayerData());
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
            _pawn.ResetComponents();
        }

        public void OnUpdate()
        {
            _pawn.Input.MoveDirection = GameManager.StaticInstance.CameraManager.transform.forward * _moveInput.y + GameManager.StaticInstance.CameraManager.transform.right * _moveInput.x;
            _pawn.Input.LookDirection = GameManager.StaticInstance.CameraManager.transform.forward;
            _pawn.Input.LookPoint = GameManager.StaticInstance.CameraManager.GetHitPoint();
            _pawn.OnUpdate();
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