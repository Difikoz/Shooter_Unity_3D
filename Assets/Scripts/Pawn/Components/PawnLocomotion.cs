using UnityEngine;

namespace WinterUniverse
{
    public class PawnLocomotion : MonoBehaviour
    {
        private PawnController _pawn;
        private CharacterController _cc;
        private Vector3 _moveVelocity;
        private Vector3 _fallVelocity;

        public void Initialize()
        {
            _pawn = GetComponent<PawnController>();
        }

        public void ResetComponent()
        {

        }

        public void OnUpdate()
        {
            // gravity
            _cc.Move(_fallVelocity * Time.deltaTime);
            if (_pawn.Input.MoveDirection != Vector3.zero)
            {
                _moveVelocity = Vector3.MoveTowards(_moveVelocity, _pawn.Input.MoveDirection, Time.deltaTime);
            }
            else
            {
                _moveVelocity = Vector3.MoveTowards(_moveVelocity, Vector3.zero, Time.deltaTime);
            }
            _cc.Move(_moveVelocity * Time.deltaTime);
        }
    }
}