using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class DamageCollider : MonoBehaviour
    {
        private Collider _collider;
        private PawnController _owner;
        private List<EffectCreator> _ownerEffects = new();
        private List<PawnController> _damagedTargets = new();

        [SerializeField] private List<DamageType> _damageTypes = new();
        [SerializeField] private List<EffectCreator> _targetEffects = new();

        public void Initialize()
        {
            _collider = GetComponent<Collider>();
            DisableCollider();
        }

        public void Initialize(PawnController owner, List<DamageType> damageTypes, List<EffectCreator> ownerEffects, List<EffectCreator> targetEffects)
        {
            _owner = owner;
            _damageTypes = new(damageTypes);
            _ownerEffects = new(ownerEffects);
            _targetEffects = new(targetEffects);
            Initialize();
        }

        public void EnableCollider()
        {
            _collider.enabled = true;
        }

        public void DisableCollider()
        {
            _collider.enabled = false;
        }

        public void ClearTargets()
        {
            _damagedTargets.Clear();
        }

        private void HitTarget(PawnController target)
        {
            if (_owner != null)
            {
                if (_ownerEffects.Count > 0)
                {
                    _owner.Effects.ApplyEffects(_ownerEffects, _owner);
                }
                foreach (DamageType dt in _damageTypes)
                {
                    target.Status.ReduceHealthCurrent((dt.Damage + (dt.Damage * _owner.Status.GetStat(dt.Type.DamageStat.DisplayName).CurrentValue / 100f)) * _owner.Status.DamageDealt.CurrentValue / 100f, dt.Type, _owner);
                }
            }
            else
            {
                foreach (DamageType dt in _damageTypes)
                {
                    target.Status.ReduceHealthCurrent(dt.Damage, dt.Type);
                }
            }
            if (_targetEffects.Count > 0)
            {
                target.Effects.ApplyEffects(_targetEffects, _owner);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            PawnController target = other.GetComponentInParent<PawnController>();
            if (target != null && target != _owner && !_damagedTargets.Contains(target))
            {
                _damagedTargets.Add(target);
                HitTarget(target);
            }
        }
    }
}