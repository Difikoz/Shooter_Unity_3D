using UnityEngine;

namespace WinterUniverse
{
    public class PawnAnimator : MonoBehaviour
    {
        private PawnController _pawn;
        private Animator _animator;

        [SerializeField] private Transform _eyesPoint;
        [SerializeField] private Transform _bodyPoint;
        [SerializeField, Range(0, 1)] private int _agentTypeID = 0;
        [SerializeField] private float _height = 2f;
        [SerializeField] private float _radius = 0.5f;
        [SerializeField] private float _acceleration = 10f;
        [SerializeField] private float _moveSpeed = 4f;
        [SerializeField] private float _rotateSpeed = 180f;
        [SerializeField] private float _attackSpeed = 1f;

        public Transform EyesPoint => _eyesPoint;
        public Transform BodyPoint => _bodyPoint;
        public int AgentTypeID => _agentTypeID;
        public float Height => _height;
        public float Radius => _radius;
        public float Acceleration => _acceleration;
        public float MoveSpeed => _moveSpeed;
        public float RotateSpeed => _rotateSpeed;
        public float AttackSpeed => _attackSpeed;

        public void Initialize()
        {
            _pawn = GetComponentInParent<PawnController>();
            _animator = GetComponent<Animator>();
            _pawn.Status.OnStatsChanged += OnStatsChanged;
        }

        public void ResetComponent()
        {
            _pawn.Status.OnStatsChanged -= OnStatsChanged;
        }

        public void OnUpdate()
        {
            //_animator.SetFloat("Velocity", _pawn.Locomotion.Velocity);
        }

        public void PlayAction(string name, float fadeTime = 0.1f, bool isPerfomingAction = true, bool stopMovement = true, bool resetTarget = false)
        {
            _pawn.StateHolder.CompareStateValue("Is Perfoming Action", isPerfomingAction);
            if(stopMovement)
            {
                //_pawn.Locomotion.StopMovement(resetTarget);
            }
            _animator.CrossFade(name, fadeTime);
        }

        private void OnStatsChanged()
        {
            _animator.SetFloat("MoveSpeed", _pawn.Status.MoveSpeed.CurrentValue / 100f);
            _animator.SetFloat("AttackSpeed", _attackSpeed * _pawn.Status.AttackSpeed.CurrentValue / 100f);
        }

        public void ResetState()
        {
            if (_pawn != null && _pawn.StateHolder.HasState("Is Perfoming Action"))
            {
                _pawn.StateHolder.SetState("Is Perfoming Action", false);
            }
        }

        public void OpenDamageCollider()
        {
            if (_pawn.Equipment.WeaponSlot.DamageCollider != null)
            {
                _pawn.Equipment.WeaponSlot.DamageCollider.EnableCollider();
            }
        }

        public void CloseDamageCollider()
        {
            if (_pawn.Equipment.WeaponSlot.DamageCollider != null)
            {
                _pawn.Equipment.WeaponSlot.DamageCollider.DisableCollider();
            }
        }

        public void ClearDamagedTargets()
        {
            if (_pawn.Equipment.WeaponSlot.DamageCollider != null)
            {
                _pawn.Equipment.WeaponSlot.DamageCollider.ClearTargets();
            }
        }
    }
}