using UnityEngine;

namespace WinterUniverse
{
    [RequireComponent(typeof(CharacterController))]
    public class PawnLocomotion : MonoBehaviour
    {
        private PawnController _pawn;
        private CharacterController _cc;
        private Vector3 _moveVelocity;
        private Vector3 _fallVelocity;
        private RaycastHit _groundHit;

        public void Initialize()
        {
            _pawn = GetComponent<PawnController>();
            _cc = GetComponent<CharacterController>();
            _cc.height = _pawn.Animator.Height;
            _cc.center = Vector3.up * _pawn.Animator.Height / 2f;
            _cc.radius = _pawn.Animator.Radius;
        }

        public void ResetComponent()
        {

        }

        public void OnUpdate()
        {
            _pawn.StateHolder.SetState("Is Grounded", _fallVelocity.y <= 0f && Physics.Raycast(transform.position, Vector3.down, out _groundHit, _cc.center.y - _cc.radius / 2f, GameManager.StaticInstance.LayerManager.ObstacleMask));
            if (_pawn.StateHolder.CompareStateValue("Is Grounded", true))
            {
                _fallVelocity.y = GameManager.StaticInstance.ConfigsManager.Gravity;
                if (_pawn.Input.MoveDirection != Vector3.zero && _pawn.StateHolder.CompareStateValue("Is Perfoming Action", false))
                {
                    _pawn.Input.MoveVelocity = Vector3.MoveTowards(_pawn.Input.MoveVelocity, _pawn.Input.MoveDirection, _pawn.Animator.Acceleration * Time.deltaTime);
                }
                else
                {
                    _pawn.Input.MoveVelocity = Vector3.MoveTowards(_pawn.Input.MoveVelocity, Vector3.zero, _pawn.Animator.Deceleration * Time.deltaTime);
                }
                if (_pawn.Input.LookDirection != Vector3.zero)
                {
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(_pawn.Input.LookDirection), _pawn.Animator.RotateSpeed * Time.deltaTime);
                }
                _moveVelocity = _pawn.Input.MoveVelocity * _pawn.Status.MovementSpeed.CurrentValue;
            }
            else
            {
                _fallVelocity.y += GameManager.StaticInstance.ConfigsManager.Gravity * Time.deltaTime;
            }
            _pawn.Input.ForwardVelocity = Vector3.Dot(_moveVelocity, transform.forward);
            _pawn.Input.RightVelocity = Vector3.Dot(_moveVelocity, transform.right);
            _pawn.Input.FallVelocity = _fallVelocity.y;
            _cc.Move(_moveVelocity * Time.deltaTime);
            _cc.Move(_fallVelocity * Time.deltaTime);
        }

        public void Jump()
        {
            if (_pawn.StateHolder.CompareStateValue("Is Grounded", true) && _pawn.StateHolder.CompareStateValue("Is Perfoming Action", false))
            {
                //_fallVelocity.y = Mathf.Sqrt(_pawn.Animator.JumpForce * -2f * GameManager.StaticInstance.ConfigsManager.Gravity);
            }
        }
    }
}