using System.Collections;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace WinterUniverse
{
    public class PawnAnimator : MonoBehaviour
    {
        private PawnController _pawn;
        private Animator _animator;
        private Coroutine _rigCoroutine;
        private Transform _leftHandTarget;

        [SerializeField] private Rig _leftHandRig;
        [SerializeField] private Transform _leftHandTargetIK;
        [SerializeField] private Transform _aimTarget;
        [SerializeField] private Transform _headPoint;
        [SerializeField] private Transform _eyesPoint;
        [SerializeField] private Transform _bodyPoint;
        [SerializeField] private float _height = 2f;
        [SerializeField] private float _radius = 0.5f;

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
            _aimTarget.position = _pawn.Input.LookPoint;
            _animator.SetFloat("ForwardVelocity", _pawn.Input.ForwardVelocity);
            _animator.SetFloat("RightVelocity", _pawn.Input.RightVelocity);
            _animator.SetFloat("TurnVelocity", _pawn.Input.TurnVelocity);
            _animator.SetFloat("FallVelocity", _pawn.Input.FallVelocity);
            _animator.SetBool("Is Moving", _pawn.Input.MoveDirection.magnitude > 0.1f);
            _animator.SetBool("Is Grounded", _pawn.StateHolder.CompareStateValue("Is Grounded", true));
            _animator.SetBool("Is Aiming", _pawn.Input.AimInput);
            if (_leftHandTarget != null)
            {
                _leftHandTargetIK.position = Vector3.MoveTowards(_leftHandTargetIK.position, _leftHandTarget.position, Time.deltaTime);
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

        public void EnableLeftHandIK(Transform target)
        {
            if (_rigCoroutine != null)
            {
                StopCoroutine(_rigCoroutine);
            }
            _leftHandTarget = target;
            _rigCoroutine = StartCoroutine(ChangeRigWeight(1f));
        }

        public void DisableLeftHandIK()
        {
            if (_rigCoroutine != null)
            {
                StopCoroutine(_rigCoroutine);
            }
            _leftHandTarget = null;
            _rigCoroutine = StartCoroutine(ChangeRigWeight(0f));
        }

        private IEnumerator ChangeRigWeight(float value)
        {
            while (_leftHandRig.weight != value)
            {
                _leftHandRig.weight = Mathf.MoveTowards(_leftHandRig.weight, value, Time.deltaTime);
                yield return null;
            }
            _rigCoroutine = null;
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