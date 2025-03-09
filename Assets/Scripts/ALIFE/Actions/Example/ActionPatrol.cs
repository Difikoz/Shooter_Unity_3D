using UnityEngine;

namespace WinterUniverse
{
    public class ActionPatrol : ActionBase
    {
        [SerializeField] private float _radius = 50f;

        private Vector3 _rootPosition;

        public override void Initialize()
        {
            base.Initialize();
            _rootPosition = transform.position;
        }

        public override void OnStart()
        {
            base.OnStart();
            //_npc.Pawn.Locomotion.SetFollowDistance();
            //_npc.Pawn.Locomotion.SetDestinationInRange(_rootPosition, _radius);
        }
    }
}