using UnityEngine;

namespace WinterUniverse
{
    public abstract class InteractableBase : MonoBehaviour
    {
        [SerializeField] private bool _allowInteractionForNPC;
        [SerializeField] private Transform _pointToInteract;
        [SerializeField] private float _distanceToInteract = 1f;

        public bool AllowInteractionForNPC => _allowInteractionForNPC;
        public Transform PointToInteract => _pointToInteract != null ? _pointToInteract : transform;
        public float DistanceToInteract => _distanceToInteract;

        public abstract bool CanInteract(PawnController pawn);
        public abstract void Interact(PawnController pawn);
    }
}