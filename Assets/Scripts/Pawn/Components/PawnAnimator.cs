using UnityEngine;
using UnityEngine.Animations.Rigging;
using static UnityEngine.GraphicsBuffer;

namespace WinterUniverse
{
    public class PawnAnimator : MonoBehaviour
    {
        private PawnController _pawn;
        private Animator _animator;
        private Transform _leftHandTarget;
        private bool _useAiming;

        [SerializeField] private AnimatorOverrideController _defaultController;
        [SerializeField] private bool _useRootMotion = true;
        [SerializeField] private Rig _aimRig;
        [SerializeField] private Rig _leftHandRig;
        [SerializeField] private Transform _leftHandTargetIK;
        [SerializeField] private Transform _aimBone;
        [SerializeField] private Transform _aimTarget;
        [SerializeField] private Transform _headPoint;
        [SerializeField] private Transform _eyesPoint;
        [SerializeField] private Transform _bodyPoint;
        [SerializeField] private float _height = 2f;
        [SerializeField] private float _radius = 0.5f;
        [SerializeField, Range(1f, 4f)] private float _acceleration = 2f;
        [SerializeField, Range(1f, 4f)] private float _deceleration = 2f;
        [SerializeField, Range(0.5f, 2f)] private float _rotateSpeed = 1f;
        [SerializeField] private float _jumpForce = 2f;
        [SerializeField] private float _turnAngle = 45f;
        //[SerializeField] private float _basicMovementSpeed = 4f;

        public Transform HeadPoint => _headPoint;
        public Transform EyesPoint => _eyesPoint;
        public Transform BodyPoint => _bodyPoint;
        public float Height => _height;
        public float Radius => _radius;
        public float Acceleration => _acceleration;
        public float Deceleration => _deceleration;
        public float RotateSpeed => _rotateSpeed;
        public float JumpForce => _jumpForce;
        public float TurnAngle => _turnAngle;
        //public float BasicMovementSpeed => _basicMovementSpeed;

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
            _aimTarget.position = _pawn.Input.LookPoint;
            _animator.SetFloat("Forward Velocity", _pawn.Input.ForwardVelocity);
            _animator.SetFloat("Right Velocity", _pawn.Input.RightVelocity);
            _animator.SetFloat("Turn Velocity", _pawn.Input.TurnVelocity);
            _animator.SetFloat("Fall Velocity", _pawn.Input.FallVelocity);
            _animator.SetFloat("Movement Speed", _pawn.Status.MovementSpeed.CurrentValue / 100f);
            _animator.SetFloat("Aim Angle", _aimBone.localEulerAngles.x);
            _animator.SetBool("Is Moving", _pawn.Input.MoveVelocity.magnitude > 0.1f);
            _animator.SetBool("Is Grounded", _pawn.StateHolder.CompareStateValue("Is Grounded", true));
            _animator.SetBool("Is Aiming", _useAiming && _pawn.StateHolder.CompareStateValue("Is Aiming", true));
            if (_leftHandTarget != null)
            {
                _leftHandTargetIK.SetLocalPositionAndRotation(_leftHandTarget.localPosition, _leftHandTarget.localRotation);
            }
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

        public void ChangeController(WeaponItemConfig config)
        {
            if (config != null)
            {
                _animator.runtimeAnimatorController = config.Controller;
                ToggleAimingIK(config.UseAiming);
                if (config.UseLeftHandIK)
                {
                    ToggleLeftHandIK(GetComponentInChildren<LeftHandTarget>().transform);
                }
                else
                {
                    ToggleLeftHandIK(null);
                }
            }
            else
            {
                _animator.runtimeAnimatorController = _defaultController;
                ToggleAimingIK(false);
                ToggleLeftHandIK(null);
            }
        }

        private void ToggleAimingIK(bool enabled)
        {
            if (enabled)
            {
                _useAiming = true;
                _aimRig.weight = 1f;
            }
            else
            {
                _useAiming = false;
                _aimRig.weight = 0f;
            }
        }

        private void ToggleLeftHandIK(Transform target)
        {
            if (target != null)
            {
                _leftHandTarget = target;
                _leftHandRig.weight = 1f;
            }
            else
            {
                _leftHandTarget = null;
                _leftHandRig.weight = 0f;
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

        public void FootR()
        {
            // play sound
        }

        public void FootL()
        {
            // play sound
        }

        public void Land()
        {
            // play sound
        }

        public void WeaponSwitch()
        {
            // play sound
        }

        private void OnAnimatorMove()
        {
            if (_useRootMotion)
            {
                _pawn.Locomotion.AddForce(_animator.deltaPosition);
                _pawn.Locomotion.AddForce(_animator.deltaRotation);
            }
        }
    }
}