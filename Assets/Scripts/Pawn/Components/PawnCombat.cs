using System;
using UnityEngine;

namespace WinterUniverse
{
    public class PawnCombat : MonoBehaviour
    {
        public Action OnTargetChanged;

        private PawnController _pawn;
        private PawnController _target;
        private RelationshipState _relationshipToTarget;
        private Vector3 _directionToTarget;
        private float _distanceToTarget;
        private float _angleToTarget;

        public PawnController Target => _target;
        public RelationshipState RelationshipToTarget => _relationshipToTarget;
        public Vector3 DirectionToTarget => _directionToTarget;
        public float DistanceToTarget => _distanceToTarget;
        public float AngleToTarget => _angleToTarget;

        public void Initialize()
        {
            _pawn = GetComponent<PawnController>();
        }

        public void OnUpdate()
        {
            if (_target != null)
            {
                if (_target.StateHolder.CompareStateValue("Is Dead", true))
                {
                    ResetTarget();
                }
                else if (_pawn.StateHolder.CompareStateValue("Is Perfoming Action", false))
                {
                    _directionToTarget = (_target.transform.position - transform.position).normalized;
                    _distanceToTarget = Vector3.Distance(transform.position, _target.transform.position);
                    _angleToTarget = Vector3.SignedAngle(transform.forward, (_target.transform.position - transform.position).normalized, Vector3.up);
                }
            }
            if (_pawn.Equipment.WeaponSlot.Weapon != null && _pawn.StateHolder.CompareStateValue("Is Attacking", true) && _pawn.Equipment.WeaponSlot.Weapon.CanAttack())
            {
                _pawn.Equipment.WeaponSlot.Weapon.OnAttack();
            }
        }

        public void SetTarget(PawnController target)
        {
            if (target != null && target.StateHolder.CompareStateValue("Is Dead", false))
            {
                _target = target;
                _directionToTarget = (_target.transform.position - transform.position).normalized;
                _distanceToTarget = Vector3.Distance(transform.position, _target.transform.position);
                _angleToTarget = Vector3.SignedAngle(transform.forward, (_target.transform.position - transform.position).normalized, Vector3.up);
                _relationshipToTarget = _pawn.Faction.Config.GetState(target.Faction.Config);
                OnTargetChanged?.Invoke();
            }
            else
            {
                ResetTarget();
            }
        }

        public void ResetTarget()
        {
            _target = null;
            _distanceToTarget = 0f;
            _angleToTarget = 0f;
            _relationshipToTarget = RelationshipState.Neutral;
            OnTargetChanged?.Invoke();
        }
    }
}