using UnityEngine;

namespace WinterUniverse
{
    public class LayerManager : MonoBehaviour
    {
        [SerializeField] private LayerMask _obstacleMask;
        [SerializeField] private LayerMask _detectableMask;

        public LayerMask ObstacleMask => _obstacleMask;
        public LayerMask DetectableMask => _detectableMask;
    }
}