using UnityEngine;

namespace WinterUniverse
{
    public class PawnSound : MonoBehaviour
    {
        private PawnController _pawn;

        public void Initialize()
        {
            _pawn = GetComponent<PawnController>();
        }
    }
}