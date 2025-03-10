using UnityEngine;

namespace WinterUniverse
{
    public class ActionFollowTarget : ActionBase
    {
        [SerializeField] private RelationshipState _followType;
        [SerializeField] private float _minFollowDistance = 5f;
        [SerializeField] private float _maxFollowDistance = 10f;
        [SerializeField] private float _resetTime = 10f;

        private Vector3 _lastTargetPosition;
        private float _currentResetTime;

        public override void OnStart()
        {
            base.OnStart();
            switch (_followType)
            {
                case RelationshipState.Enemy:
                    _npc.Pawn.Combat.SetTarget(_npc.Pawn.Detection.GetClosestEnemy());
                    break;
                case RelationshipState.Neutral:
                    _npc.Pawn.Combat.SetTarget(_npc.Pawn.Detection.GetClosestNeutral());
                    break;
                case RelationshipState.Ally:
                    _npc.Pawn.Combat.SetTarget(_npc.Pawn.Detection.GetClosestAlly());
                    break;
            }
            _lastTargetPosition = _npc.Pawn.Combat.Target.transform.position;
            _currentResetTime = 0f;
            _npc.SetDestination(_lastTargetPosition);
        }

        public override bool CanAbort()
        {
            if (_currentResetTime >= _resetTime)
            {
                return true;
            }
            return base.CanAbort();
        }

        public override void OnUpdate(float deltaTime)
        {
            base.OnUpdate(deltaTime);
            if (_npc.Pawn.Combat.Target == null)
            {
                switch (_followType)
                {
                    case RelationshipState.Enemy:
                        if (_npc.Pawn.Detection.DetectedEnemies.Count > 0)
                        {
                            _npc.Pawn.Combat.SetTarget(_npc.Pawn.Detection.GetClosestEnemy());
                        }
                        break;
                    case RelationshipState.Neutral:
                        if (_npc.Pawn.Detection.DetectedNeutrals.Count > 0)
                        {
                            _npc.Pawn.Combat.SetTarget(_npc.Pawn.Detection.GetClosestNeutral());
                        }
                        break;
                    case RelationshipState.Ally:
                        if (_npc.Pawn.Detection.DetectedAllies.Count > 0)
                        {
                            _npc.Pawn.Combat.SetTarget(_npc.Pawn.Detection.GetClosestAlly());
                        }
                        break;
                }
                return;
            }
            if (_npc.Pawn.Detection.TargetIsVisible(_npc.Pawn.Combat.Target))
            {
                _lastTargetPosition = _npc.Pawn.Combat.Target.transform.position;
                _currentResetTime = 0f;
                if (_followType == RelationshipState.Enemy)
                {
                    Follow(_npc.Pawn.Equipment.WeaponSlot.Config.MinRange, _npc.Pawn.Equipment.WeaponSlot.Config.MaxRange);
                }
                else
                {
                    Follow(_minFollowDistance, _maxFollowDistance);
                }
                _npc.Pawn.StateHolder.CompareStateValue("Is Attacking", _followType == RelationshipState.Enemy);
            }
            else
            {
                _currentResetTime += deltaTime;
                //if (_npc.ReachedDestination)
                //{
                //    _npc.SetDestination(_npc.Pawn.Combat.Target.transform.position);
                //}
                _npc.Pawn.StateHolder.CompareStateValue("Is Attacking", false);
                _npc.SetDestination(_lastTargetPosition);
            }
        }

        private void Follow(float minDistance, float maxDistance)
        {
            if (_npc.Pawn.Combat.DistanceToTarget <= minDistance)
            {
                if (!_npc.ReachedDestination)
                {
                    _npc.StopMovement();
                }
            }
            else if (_npc.Pawn.Combat.DistanceToTarget <= maxDistance)
            {
                _npc.SetDestination(_lastTargetPosition);
            }
        }
    }
}