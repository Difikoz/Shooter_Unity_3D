using UnityEngine;

namespace WinterUniverse
{
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
            }
            else
            {
                _fallVelocity.y += GameManager.StaticInstance.ConfigsManager.Gravity * Time.deltaTime;
            }
            _cc.Move(_fallVelocity * Time.deltaTime);
            if (_pawn.Input.MoveDirection != Vector3.zero)
            {
                _moveVelocity = Vector3.MoveTowards(_moveVelocity, _pawn.Input.MoveDirection * _pawn.Status.MovementSpeed.CurrentValue, _pawn.Status.MovementSpeed.CurrentValue * 2f * Time.deltaTime);
            }
            else
            {
                _moveVelocity = Vector3.MoveTowards(_moveVelocity, Vector3.zero, _pawn.Status.MovementSpeed.CurrentValue * 4f * Time.deltaTime);
            }
            _cc.Move(_moveVelocity * Time.deltaTime);
        }
    }
}