using UnityEngine;

namespace WinterUniverse
{
    public class ActionFighting : ActionBase
    {
        [SerializeField] private float _resetTime = 10f;

        private Vector3 _lastTargetPosition;
        private float _currentResetTime;

        public override void OnStart()
        {
            base.OnStart();
            _npc.Pawn.Combat.SetTarget(_npc.Pawn.Detection.GetClosestEnemy());
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
                if (_npc.Pawn.Detection.DetectedEnemies.Count > 0)
                {
                    _npc.Pawn.Combat.SetTarget(_npc.Pawn.Detection.GetClosestEnemy());
                }
                return;
            }
            _npc.Pawn.Input.LookDirection = _npc.Pawn.Combat.DirectionToTarget;
            _npc.Pawn.Input.LookPoint = _npc.Pawn.Combat.Target.Animator.BodyPoint.position;
            if (_npc.Pawn.Detection.TargetIsVisible(_npc.Pawn.Combat.Target))
            {
                _lastTargetPosition = _npc.Pawn.Combat.Target.transform.position;
                _currentResetTime = 0f;
                if (_npc.Pawn.Combat.DistanceToTarget <= _npc.Pawn.Equipment.WeaponSlot.Config.MinRange)
                {
                    if (!_npc.ReachedDestination)
                    {
                        _npc.StopMovement();
                    }
                    _npc.Pawn.StateHolder.CompareStateValue("Is Attacking", true);
                }
                else if (_npc.Pawn.Combat.DistanceToTarget <= _npc.Pawn.Equipment.WeaponSlot.Config.MaxRange)
                {
                    _npc.SetDestination(_lastTargetPosition);
                    _npc.Pawn.StateHolder.CompareStateValue("Is Attacking", true);
                }
            }
            else
            {
                _currentResetTime += deltaTime;
                //if (_npc.ReachedDestination)
                //{
                //    _npc.SetDestination(_npc.Pawn.Combat.Target.transform.position);
                //}
                _npc.SetDestination(_lastTargetPosition);
            }
        }
    }
}