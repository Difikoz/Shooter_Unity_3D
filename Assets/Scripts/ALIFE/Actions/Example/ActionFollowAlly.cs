using UnityEngine;

namespace WinterUniverse
{
    public class ActionFollowAlly : ActionBase
    {
        public override void OnStart()
        {
            base.OnStart();
            _npc.Pawn.Combat.SetTarget(_npc.Pawn.Detection.GetClosestAlly());
            //_npc.Pawn.Combat.FollowTarget();
        }
    }
}