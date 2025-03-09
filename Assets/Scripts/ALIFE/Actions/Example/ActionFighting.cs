using UnityEngine;

namespace WinterUniverse
{
    public class ActionFighting : ActionBase
    {
        public override void OnStart()
        {
            base.OnStart();
            _npc.Pawn.Combat.SetTarget(_npc.Pawn.Detection.GetClosestEnemy());
            //_npc.Pawn.Combat.AttackTarget();
        }
    }
}