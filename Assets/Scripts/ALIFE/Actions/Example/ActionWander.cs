using UnityEngine;

namespace WinterUniverse
{
    public class ActionWander : ActionBase
    {
        //[SerializeField] private float _minRadius = 10f;
        //[SerializeField] private float _maxRadius = 20f;

        public override void OnStart()
        {
            base.OnStart();
            //_npc.Pawn.Locomotion.SetFollowDistance();
            //_npc.Pawn.Locomotion.SetDestinationAroundSelf(_minRadius, _maxRadius);
        }
    }
}