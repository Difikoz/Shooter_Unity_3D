using UnityEngine;

namespace WinterUniverse
{
    public class PawnSound : MonoBehaviour
    {
        private PawnController _pawn;
        private AudioSource _audioSource;

        public void Initialize()
        {
            _pawn = GetComponentInParent<PawnController>();
            _audioSource = GetComponentInChildren<AudioSource>();
        }
    }
}