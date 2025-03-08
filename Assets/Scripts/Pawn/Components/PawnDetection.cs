using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class PawnDetection : MonoBehaviour
    {
        private PawnController _pawn;
        private List<PawnController> _detectedEnemies = new();
        private List<PawnController> _detectedNeutrals = new();
        private List<PawnController> _detectedAllies = new();
        private List<InteractableBase> _detectedInteractables = new();
        private float _detectionTime;

        [SerializeField] private float _detectionCooldown = 2f;

        public List<PawnController> DetectedEnemies => _detectedEnemies;
        public List<PawnController> DetectedNeutrals => _detectedNeutrals;
        public List<PawnController> DetectedAllies => _detectedAllies;
        public List<InteractableBase> DetectedInteractables => _detectedInteractables;

        public void Initialize()
        {
            _pawn = GetComponentInParent<PawnController>();
        }

        public void OnUpdate()
        {
            if (_detectionTime >= _detectionCooldown)
            {
                _detectedEnemies.Clear();
                _detectedNeutrals.Clear();
                _detectedAllies.Clear();
                _detectedInteractables.Clear();
                float distance;
                RelationshipState relationship;
                Collider[] colliders = Physics.OverlapSphere(_pawn.Animator.EyesPoint.position, _pawn.Status.ViewDistance.CurrentValue, GameManager.StaticInstance.LayerManager.DetectableMask);
                foreach (Collider collider in colliders)
                {
                    distance = Vector3.Distance(transform.position, collider.transform.position);
                    InteractableBase interactable = collider.GetComponentInParent<InteractableBase>();
                    if (interactable != null /*&& interactable.AllowInteractionForNPC*/ && interactable.CanInteract(_pawn))
                    {
                        _detectedInteractables.Add(interactable);
                    }
                    else if (collider.TryGetComponent(out interactable) /*&& interactable.AllowInteractionForNPC */&& interactable.CanInteract(_pawn))
                    {
                        _detectedInteractables.Add(interactable);
                    }
                    else if (collider.TryGetComponent(out PawnController pawn) && pawn != _pawn && pawn.StateHolder.CompareStateValue("Is Dead", false))
                    {
                        if (distance <= _pawn.Status.HearRadius.CurrentValue || TargetIsVisible(pawn))
                        {
                            relationship = _pawn.Faction.Config.GetState(pawn.Faction.Config);
                            switch (relationship)
                            {
                                case RelationshipState.Enemy:
                                    _detectedEnemies.Add(pawn);
                                    break;
                                case RelationshipState.Neutral:
                                    _detectedNeutrals.Add(pawn);
                                    break;
                                case RelationshipState.Ally:
                                    _detectedAllies.Add(pawn);
                                    break;
                            }
                        }
                    }
                }
                _pawn.StateHolder.SetState("Detected Enemy", _detectedEnemies.Count > 0);
                _pawn.StateHolder.SetState("Detected Neutral", _detectedNeutrals.Count > 0);
                _pawn.StateHolder.SetState("Detected Ally", _detectedAllies.Count > 0);
                _detectionTime = 0f;
            }
            else
            {
                _detectionTime += Time.deltaTime;
            }
        }

        public bool TargetInRange(Transform t)
        {
            float distance = Vector3.Distance(transform.position, t.position);
            if (distance <= _pawn.Status.HearRadius.CurrentValue)
            {
                return true;
            }
            else if (distance <= _pawn.Status.ViewDistance.CurrentValue)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool TargetIsVisible(Transform t)
        {
            Vector3 direction = (t.position - _pawn.Animator.EyesPoint.position).normalized;
            if (Vector3.Angle(_pawn.Animator.EyesPoint.forward, direction) > _pawn.Status.ViewAngle.CurrentValue / 2f)
            {
                return false;
            }
            if (Physics.Linecast(_pawn.Animator.EyesPoint.position, t.position, GameManager.StaticInstance.LayerManager.ObstacleMask))
            {
                return false;
            }
            return true;
        }

        public bool TargetIsVisible(PawnController pawn)
        {
            Vector3 direction = (pawn.Animator.BodyPoint.position - _pawn.Animator.EyesPoint.position).normalized;
            if (Vector3.Angle(_pawn.Animator.EyesPoint.forward, direction) > _pawn.Status.ViewAngle.CurrentValue / 2f)
            {
                return false;
            }
            if (Physics.Linecast(_pawn.Animator.EyesPoint.position, pawn.Animator.BodyPoint.position, GameManager.StaticInstance.LayerManager.ObstacleMask))
            {
                return false;
            }
            return true;
        }

        public PawnController GetClosestEnemy()
        {
            PawnController closestPawn = null;
            float maxDistance = float.MaxValue;
            float distance;
            foreach (PawnController pawn in _detectedEnemies)
            {
                distance = Vector3.Distance(transform.position, pawn.transform.position);
                if (distance < maxDistance)
                {
                    maxDistance = distance;
                    closestPawn = pawn;
                }
            }
            return closestPawn;
        }

        public PawnController GetClosestNeutral()
        {
            PawnController closestPawn = null;
            float maxDistance = float.MaxValue;
            float distance;
            foreach (PawnController pawn in _detectedNeutrals)
            {
                distance = Vector3.Distance(transform.position, pawn.transform.position);
                if (distance < maxDistance)
                {
                    maxDistance = distance;
                    closestPawn = pawn;
                }
            }
            return closestPawn;
        }

        public PawnController GetClosestAlly()
        {
            PawnController closestPawn = null;
            float maxDistance = float.MaxValue;
            float distance;
            foreach (PawnController pawn in _detectedAllies)
            {
                distance = Vector3.Distance(transform.position, pawn.transform.position);
                if (distance < maxDistance)
                {
                    maxDistance = distance;
                    closestPawn = pawn;
                }
            }
            return closestPawn;
        }

        //private void OnDrawGizmos()
        //{
        //    if (_pawn != null & _pawn.Status != null && _pawn.Status.Stats.Count > 0)
        //    {
        //        Gizmos.color = Color.red;
        //        Gizmos.DrawWireSphere(transform.position, _pawn.Status.HearRadius.CurrentValue);
        //        Gizmos.color = Color.yellow;
        //        Gizmos.DrawWireSphere(transform.position, _pawn.Status.ViewDistance.CurrentValue);
        //    }
        //}
    }
}