using UnityEngine;

namespace WinterUniverse
{
    public class PawnInput : MonoBehaviour
    {
        private PawnController _pawn;

        public Vector3 MoveDirection;
        public Vector3 LookDirection;
        public Vector3 LookPoint;
        public bool FireInput;
        public bool AimInput;
        public float ForwardVelocity;
        public float RightVelocity;
        public float TurnVelocity;
        public float FallVelocity;

        public void Initialize()
        {
            _pawn = GetComponent<PawnController>();
        }

        public void OnUpdate()
        {

        }
    }
}