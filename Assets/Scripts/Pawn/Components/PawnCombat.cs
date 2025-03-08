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
                    if (_pawn.StateHolder.CompareStateValue("Is Fighting", true) && _pawn.Equipment.WeaponSlot.Config != null && _distanceToTarget <= _pawn.Equipment.WeaponSlot.Config.AttackMaxRange)
                    {
                        if (_angleToTarget > _pawn.Equipment.WeaponSlot.Config.AttackAngle / 2f)
                        {
                            transform.Rotate(Vector3.up * _pawn.Status.RotateSpeed.CurrentValue * Time.deltaTime);
                        }
                        else if (_angleToTarget < -_pawn.Equipment.WeaponSlot.Config.AttackAngle / 2f)
                        {
                            transform.Rotate(Vector3.up * -_pawn.Status.RotateSpeed.CurrentValue * Time.deltaTime);
                        }
                        else
                        {
                            _pawn.Animator.PlayAction("Attack");
                        }
                    }
                }
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

        public void FollowTarget(float minDistance = -1f, float maxDistance = -1f)
        {
            if (_target != null)
            {
                //_pawn.Locomotion.StopMovement();
                //_pawn.Locomotion.SetDestination(_target.transform);
                if (minDistance <= 0f)
                {
                   // minDistance = _pawn.Locomotion.Agent.radius * 4f;
                }
                if (maxDistance <= 0f)
                {
                    //maxDistance = _pawn.Locomotion.Agent.radius * 8f;
                }
                //_pawn.Locomotion.SetFollowDistance(minDistance, maxDistance);
                _pawn.StateHolder.SetState("Is Following", true);
                _pawn.StateHolder.SetState("Is Fighting", false);
            }
            else
            {
                ResetTarget();
            }
        }

        public void AttackTarget()
        {
            if (_target != null)
            {
                if (_pawn.Equipment.WeaponSlot.Config != null)
                {
                    //_pawn.Locomotion.StopMovement();
                    //_pawn.Locomotion.SetDestination(_target.transform);
                    //_pawn.Locomotion.SetFollowDistance(_pawn.Equipment.WeaponSlot.Config.AttackMinRange, _pawn.Equipment.WeaponSlot.Config.AttackMaxRange);
                    _pawn.StateHolder.SetState("Is Following", false);
                    _pawn.StateHolder.SetState("Is Fighting", true);
                }
            }
            else
            {
                ResetTarget();
            }
        }

        public void ResetTarget()
        {
            //_pawn.Locomotion.StopMovement();
            _target = null;
            _distanceToTarget = 0f;
            _angleToTarget = 0f;
            _relationshipToTarget = RelationshipState.Neutral;
            _pawn.StateHolder.SetState("Is Fighting", false);
            _pawn.StateHolder.SetState("Is Following", false);
            OnTargetChanged?.Invoke();
        }

        //public bool GetEnemyTarget(out PawnController enemy)
        //{
        //    enemy = _target;
        //    return enemy != null && _relationshipToTarget == RelationshipState.Enemy;
        //}
    }
}