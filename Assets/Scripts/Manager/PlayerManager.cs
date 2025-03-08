using UnityEngine;
using UnityEngine.InputSystem;

namespace WinterUniverse
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private PawnData _testPawnData;
        [SerializeField] private PlayerData _testPlayerData;

        private PawnController _pawn;
        private Vector2 _cursorLocalPosition;
        private Ray _cameraRay;
        private RaycastHit _cameraHit;

        public PawnController Pawn => _pawn;

        public void OnMoveToPosition()
        {
            if (GameManager.StaticInstance.InputMode == InputMode.UI)
            {
                return;
            }
            if (Physics.Raycast(_cameraRay, out _cameraHit, 1000f))
            {
                //_pawn.Locomotion.StopMovement();
                //_pawn.Locomotion.SetDestination(_cameraHit.point);
            }
        }

        public void Initialize()
        {
            Initialize(_testPawnData, _testPlayerData);
        }

        public void Initialize(PawnData pawnData, PlayerData playerData)
        {
            InitializePawn(pawnData);
            LoadData(playerData);
        }

        public void InitializePawn(PawnData data)
        {
            _pawn = GameManager.StaticInstance.PrefabsManager.GetPawn(transform);
            //_pawn.Initialize(data);
        }

        public void ResetComponent()
        {
            //_pawn.ResetComponent();
        }

        public void OnUpdate()
        {
            _cameraRay = Camera.main.ScreenPointToRay(_cursorLocalPosition);
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