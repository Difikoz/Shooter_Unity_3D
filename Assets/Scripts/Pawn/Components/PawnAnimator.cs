using UnityEngine;

namespace WinterUniverse
{
    public class PawnAnimator : MonoBehaviour
    {
        private PawnController _pawn;
        private Animator _animator;

        [SerializeField] private Transform _headPoint;
        [SerializeField] private Transform _eyesPoint;
        [SerializeField] private Transform _bodyPoint;
        [SerializeField] private float _height = 2f;
        [SerializeField] private float _radius = 0.5f;
        [SerializeField, Range(1f, 4f)] private float _acceleration = 2f;
        [SerializeField, Range(1f, 4f)] private float _deceleration = 2f;
        [SerializeField, Range(0.5f, 4f)] private float _rotateSpeed = 1f;

        public Transform HeadPoint => _headPoint;
        public Transform EyesPoint => _eyesPoint;
        public Transform BodyPoint => _bodyPoint;
        public float Height => _height;
        public float Radius => _radius;
        public float Acceleration => _acceleration;
        public float Deceleration => _deceleration;
        public float RotateSpeed => _rotateSpeed;

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
            _animator.SetFloat("Forward Velocity", _pawn.Input.ForwardVelocity);
            _animator.SetFloat("Right Velocity", _pawn.Input.RightVelocity);
            _animator.SetFloat("Fall Velocity", _pawn.Input.FallVelocity);
            _animator.SetBool("Is Moving", _pawn.Input.MoveVelocity.magnitude > 0.1f);
            _animator.SetBool("Is Grounded", _pawn.StateHolder.CompareStateValue("Is Grounded", true));
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

        public void FootR()
        {

        }

        public void FootL()
        {

        }

        public void Land()
        {

        }

        public void WeaponSwitch()
        {

        }
    }
}