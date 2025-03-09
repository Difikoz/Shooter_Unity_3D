using UnityEngine;

namespace WinterUniverse
{
    public class ActionFlee : ActionBase
    {
        //[SerializeField] private float _minDistance = 50f;
        //[SerializeField] private float _maxDistance = 100f;

        public override void OnStart()
        {
            base.OnStart();
            //_npc.Pawn.Locomotion.SetDestination(_npc.Pawn.transform.position + _npc.Pawn.Combat.DirectionToTarget * Random.Range(_minDistance, _maxDistance));
        }
    }
}