using System;
using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class PawnStatus : MonoBehaviour
    {
        public Action<float, float> OnHealthChanged;
        public Action<float, float> OnEnergyChanged;
        public Action OnDied;
        public Action OnRevived;
        public Action OnStatsChanged;

        private PawnController _pawn;

        private List<Stat> _stats = new();

        private float _healthCurrent;
        private float _energyCurrent;
        private Stat _healthMax;
        private Stat _healthRegeneration;
        private Stat _energyMax;
        private Stat _energyRegeneration;
        private Stat _movementSpeed;
        private Stat _attackSpeed;
        private Stat _damageDealt;
        private Stat _slicingDamage;
        private Stat _piercingDamage;
        private Stat _bluntDamage;
        private Stat _thermalDamage;
        private Stat _electricalDamage;
        private Stat _chemicalDamage;
        private Stat _damageTaken;
        private Stat _slicingResistance;
        private Stat _piercingResistance;
        private Stat _bluntResistance;
        private Stat _thermalResistance;
        private Stat _electricalResistance;
        private Stat _chemicalResistance;
        private Stat _hearRadius;
        private Stat _viewDistance;
        private Stat _viewAngle;

        [SerializeField] private float _regenerationTickCooldown = 0.2f;
        [SerializeField] private float _healthRegenerationDelayCooldown = 4f;
        [SerializeField] private float _energyRegenerationDelayCooldown = 4f;

        private float _healthRegenerationCurrentTickTime;
        private float _healthRegenerationCurrentDelayTime;
        private float _energyRegenerationCurrentTickTime;
        private float _energyRegenerationCurrentDelayTime;

        public List<Stat> Stats => _stats;
        public float HealthCurrent => _healthCurrent;
        public float EnergyCurrent => _energyCurrent;
        public Stat HealthMax => _healthMax;
        public Stat HealthRegeneration => _healthRegeneration;
        public Stat EnergyMax => _energyMax;
        public Stat EnergyRegeneration => _energyRegeneration;
        public Stat MovementSpeed => _movementSpeed;
        public Stat AttackSpeed => _attackSpeed;
        public Stat DamageDealt => _damageDealt;
        public Stat SlicingDamage => _slicingDamage;
        public Stat PiercingDamage => _piercingDamage;
        public Stat BluntDamage => _bluntDamage;
        public Stat ThermalDamage => _thermalDamage;
        public Stat ElectricalDamage => _electricalDamage;
        public Stat ChemicalDamage => _chemicalDamage;
        public Stat DamageTaken => _damageTaken;
        public Stat SlicingResistance => _slicingResistance;
        public Stat PiercingResistance => _piercingResistance;
        public Stat BluntResistance => _bluntResistance;
        public Stat ThermalResistance => _thermalResistance;
        public Stat ElectricalResistance => _electricalResistance;
        public Stat ChemicalResistance => _chemicalResistance;
        public Stat HearRadius => _hearRadius;
        public Stat ViewDistance => _viewDistance;
        public Stat ViewAngle => _viewAngle;
        public float HealthPercent => _healthCurrent / _healthMax.CurrentValue;
        public float EnergyPercent => _energyCurrent / _energyMax.CurrentValue;

        public void Initialize()
        {
            _pawn = GetComponent<PawnController>();
            CreateStats();
            AssignStats();
            RecalculateStats();
            Revive();
        }

        public void ResetComponent()
        {

        }

        private void CreateStats()
        {
            foreach (StatConfig stat in GameManager.StaticInstance.ConfigsManager.Stats)
            {
                _stats.Add(new(stat));
            }
        }

        private void AssignStats()
        {
            foreach (Stat s in _stats)
            {
                switch (s.Config.DisplayName)
                {
                    case "Health Max":
                        _healthMax = s;
                        break;
                    case "Health Regeneration":
                        _healthRegeneration = s;
                        break;
                    case "Energy Max":
                        _energyMax = s;
                        break;
                    case "Energy Regeneration":
                        _energyRegeneration = s;
                        break;
                    case "Movement Speed":
                        _movementSpeed = s;
                        break;
                    case "Attack Speed":
                        _attackSpeed = s;
                        break;
                    case "Slicing Damage":
                        _slicingDamage = s;
                        break;
                    case "Damage Dealt":
                        _damageDealt = s;
                        break;
                    case "Piercing Damage":
                        _piercingDamage = s;
                        break;
                    case "Blunt Damage":
                        _bluntDamage = s;
                        break;
                    case "Thermal Damage":
                        _thermalDamage = s;
                        break;
                    case "Electrical Damage":
                        _electricalDamage = s;
                        break;
                    case "Chemical Damage":
                        _chemicalDamage = s;
                        break;
                    case "Damage Taken":
                        _damageTaken = s;
                        break;
                    case "Slicing Resistance":
                        _slicingResistance = s;
                        break;
                    case "Piercing Resistance":
                        _piercingResistance = s;
                        break;
                    case "Blunt Resistance":
                        _bluntResistance = s;
                        break;
                    case "Thermal Resistance":
                        _thermalResistance = s;
                        break;
                    case "Electrical Resistance":
                        _electricalResistance = s;
                        break;
                    case "Chemical Resistance":
                        _chemicalResistance = s;
                        break;
                    case "Hear Radius":
                        _hearRadius = s;
                        break;
                    case "View Distance":
                        _viewDistance = s;
                        break;
                    case "View Angle":
                        _viewAngle = s;
                        break;
                }
            }
        }

        public void RecalculateStats()
        {
            _healthCurrent = Mathf.Clamp(_healthCurrent, 0f, _healthMax.CurrentValue);
            _energyCurrent = Mathf.Clamp(_energyCurrent, 0f, _energyMax.CurrentValue);
            OnHealthChanged?.Invoke(_healthCurrent, _healthMax.CurrentValue);
            OnEnergyChanged?.Invoke(_energyCurrent, _energyMax.CurrentValue);
            OnStatsChanged?.Invoke();
        }

        public void OnUpdate()
        {
            if (_healthRegenerationCurrentDelayTime >= _healthRegenerationDelayCooldown)
            {
                if (_healthRegenerationCurrentTickTime >= _regenerationTickCooldown)
                {
                    RestoreHealthCurrent(_healthRegeneration.CurrentValue * _regenerationTickCooldown);
                    _healthRegenerationCurrentTickTime = 0f;
                }
                else
                {
                    _healthRegenerationCurrentTickTime += Time.deltaTime;
                }
            }
            else
            {
                _healthRegenerationCurrentDelayTime += Time.deltaTime;
            }
            if (_energyRegenerationCurrentDelayTime >= _energyRegenerationDelayCooldown)
            {
                if (_energyRegenerationCurrentTickTime >= _regenerationTickCooldown)
                {
                    RestoreEnergyCurrent(_energyRegeneration.CurrentValue * _regenerationTickCooldown);
                    _energyRegenerationCurrentTickTime = 0f;
                }
                else
                {
                    _energyRegenerationCurrentTickTime += Time.deltaTime;
                }
            }
            else
            {
                _energyRegenerationCurrentDelayTime += Time.deltaTime;
            }
        }

        public void ReduceHealthCurrent(float value, ElementConfig type, PawnController source = null)
        {
            if (_pawn.StateHolder.CompareStateValue("Is Dead", true) || value <= 0f)
            {
                return;
            }
            if (_pawn.Equipment.ArmorSlot.Config != null && _pawn.Equipment.ArmorSlot.Config.EquipmentData.OwnerEffects.Count > 0)
            {
                _pawn.Effects.ApplyEffects(_pawn.Equipment.ArmorSlot.Config.EquipmentData.OwnerEffects, _pawn);
            }
            if (source != null)
            {
                if (_pawn.Equipment.ArmorSlot.Config != null && _pawn.Equipment.ArmorSlot.Config.EquipmentData.TargetEffects.Count > 0)
                {
                    source.Effects.ApplyEffects(_pawn.Equipment.ArmorSlot.Config.EquipmentData.TargetEffects, _pawn);
                }
            }
            float resistance = GetStat(type.ResistanceStat.DisplayName).CurrentValue;
            if (resistance < 100f)
            {
                _healthRegenerationCurrentDelayTime = 0f;
                value -= value * resistance / 100f;
                value *= _damageTaken.CurrentValue / 100f;
                _healthCurrent = Mathf.Clamp(_healthCurrent - value, 0f, _healthMax.CurrentValue);
                _pawn.StateHolder.SetState("Is Injured", HealthPercent < 0.25f);
                if (_healthCurrent <= 0f)
                {
                    Die(source);
                }
                else
                {
                    OnHealthChanged?.Invoke(_healthCurrent, _healthMax.CurrentValue);
                }
            }
            else if (resistance > 100f)
            {
                value *= resistance / 100f - 1f;
                RestoreHealthCurrent(value);
            }
        }

        public void RestoreHealthCurrent(float value)
        {
            if (_pawn.StateHolder.CompareStateValue("Is Dead", true) || value <= 0f)
            {
                return;
            }
            _healthCurrent = Mathf.Clamp(_healthCurrent + value, 0f, _healthMax.CurrentValue);
            _pawn.StateHolder.SetState("Is Injured", HealthPercent < 0.25f);
            OnHealthChanged?.Invoke(_healthCurrent, _healthMax.CurrentValue);
        }

        public void ReduceEnergyCurrent(float value)
        {
            if (_pawn.StateHolder.CompareStateValue("Is Dead", true) || value <= 0f)
            {
                return;
            }
            _energyRegenerationCurrentDelayTime = 0f;
            _energyCurrent = Mathf.Clamp(_energyCurrent - value, 0f, _energyMax.CurrentValue);
            OnEnergyChanged?.Invoke(_energyCurrent, _energyMax.CurrentValue);
        }

        public void RestoreEnergyCurrent(float value)
        {
            if (_pawn.StateHolder.CompareStateValue("Is Dead", true) || value <= 0f)
            {
                return;
            }
            _energyCurrent = Mathf.Clamp(_energyCurrent + value, 0f, _energyMax.CurrentValue);
            OnEnergyChanged?.Invoke(_energyCurrent, _energyMax.CurrentValue);
        }

        private void Die(PawnController source = null)
        {
            if (_pawn.StateHolder.CompareStateValue("Is Dead", true))
            {
                return;
            }
            if (source != null)
            {
                // check target
            }
            _healthCurrent = 0f;
            _energyCurrent = 0f;
            OnHealthChanged?.Invoke(_healthCurrent, _healthMax.CurrentValue);
            OnEnergyChanged?.Invoke(_energyCurrent, _energyMax.CurrentValue);
            _pawn.StateHolder.SetState("Is Dead", true);
            _pawn.Animator.PlayAction("Death");
            OnDied?.Invoke();
        }

        public void Revive()
        {
            _pawn.Animator.PlayAction("Revive");
            _pawn.StateHolder.SetState("Is Dead", false);
            RestoreHealthCurrent(_healthMax.CurrentValue);
            RestoreEnergyCurrent(_energyMax.CurrentValue);
            OnRevived?.Invoke();
        }

        public void AddStatModifiers(List<StatModifierCreator> modifiers)
        {
            foreach (StatModifierCreator smc in modifiers)
            {
                AddStatModifier(smc);
            }
            RecalculateStats();
        }

        public void AddStatModifier(StatModifierCreator smc)
        {
            GetStat(smc.Stat.DisplayName).AddModifier(smc.Modifier);
        }

        public void RemoveStatModifiers(List<StatModifierCreator> modifiers)
        {
            foreach (StatModifierCreator smc in modifiers)
            {
                RemoveStatModifier(smc);
            }
            RecalculateStats();
        }

        public void RemoveStatModifier(StatModifierCreator smc)
        {
            GetStat(smc.Stat.DisplayName).RemoveModifier(smc.Modifier);
        }

        public Stat GetStat(string name)
        {
            foreach (Stat stat in _stats)
            {
                if (stat.Config.DisplayName == name)
                {
                    return stat;
                }
            }
            return null;
        }
    }
}