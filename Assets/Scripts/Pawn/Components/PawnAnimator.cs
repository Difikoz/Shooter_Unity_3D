using UnityEngine;

namespace WinterUniverse
{
    public class PawnAnimator : MonoBehaviour
    {
        private PawnController _pawn;
        private Animator _animator;

        [SerializeField] private Transform _aimBone;
        [SerializeField] private Transform _headPoint;
        [SerializeField] private Transform _eyesPoint;
        [SerializeField] private Transform _bodyPoint;
        [SerializeField] private float _height = 2f;
        [SerializeField] private float _radius = 0.5f;

        public Transform AimBone => _aimBone;
        public Transform HeadPoint => _headPoint;
        public Transform EyesPoint => _eyesPoint;
        public Transform BodyPoint => _bodyPoint;
        public float Height => _height;
        public float Radius => _radius;

        public void Initialize()
        {
            _pawn = GetComponentInParent<PawnController>();
            _animator = GetComponent<Animator>();
        }

        public void ResetComponent()
        {

        }

        public void OnUpdate()
        {
            _animator.SetFloat("ForwardVelocity", 100f);
            _animator.SetFloat("RightVelocity", 100f);
            _animator.SetFloat("TurnVelocity", 100f);
            _animator.SetFloat("FallVelocity", 100f);
            _animator.SetBool("Is Moving", _pawn.Input.MoveDirection.magnitude > 0.1f);
            _animator.SetBool("Is Grounded", _pawn.StateHolder.CompareStateValue("Is Grounded", true));
            _animator.SetBool("Is Aiming", _pawn.Input.AimInput);
        }

        public void SetFloat(string name, float value)
        {
            _animator.SetFloat(name, value);
        }

        public void PlayAction(string name, float fadeTime = 0.1f, bool isPerfomingAction = true)
        {
            _pawn.StateHolder.CompareStateValue("Is Perfoming Action", isPerfomingAction);
            _animator.CrossFade(name, fadeTime);
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
            if (_pawn.Equipment.WeaponSlot.Weapon != null)
            {
                _pawn.Equipment.WeaponSlot.Weapon.gameObject.SendMessage("EnableCollider");
            }
        }

        public void CloseDamageCollider()
        {
            if (_pawn.Equipment.WeaponSlot.Weapon != null)
            {
                _pawn.Equipment.WeaponSlot.Weapon.gameObject.SendMessage("DisableCollider");
            }
        }

        public void ClearDamagedTargets()
        {
            if (_pawn.Equipment.WeaponSlot.Weapon != null)
            {
                _pawn.Equipment.WeaponSlot.Weapon.gameObject.SendMessage("ClearTargets");
            }
        }
    }
}