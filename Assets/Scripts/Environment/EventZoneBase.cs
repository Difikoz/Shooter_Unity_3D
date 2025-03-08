using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public abstract class EventZoneBase : MonoBehaviour
    {
        protected List<PawnController> _enteredTargets = new();

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PawnController pawn) && !_enteredTargets.Contains(pawn))
            {
                OnEntered(pawn);
                _enteredTargets.Add(pawn);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out PawnController pawn) && _enteredTargets.Contains(pawn))
            {
                _enteredTargets.Remove(pawn);
                OnExited(pawn);
            }
        }

        protected abstract void OnEntered(PawnController target);
        protected abstract void OnExited(PawnController target);
    }
}