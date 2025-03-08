using System;
using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class PawnStatus : MonoBehaviour
    {
        public Action<float, float> OnHealthChanged;
        public Action<float, float> OnEnergyChanged;
        public Action<float, float> OnManaChanged;
        public Action OnDied;
        public Action OnRevived;
        public Action OnStatsChanged;

        private PawnController _pawn;

        private List<Stat> _stats = new();

        private float _healthCurrent;
        private float _energyCurrent;
        private float _manaCurrent;
        private Stat _healthMax;
        private Stat _healthRegeneration;
        private Stat _energyMax;
        private Stat _energyRegeneration;
        private Stat _manaMax;
        private Stat _manaRegeneration;
        private Stat _acceleration;
        private Stat _moveSpeed;
        private Stat _rotateSpeed;
        private Stat _attackSpeed;
        private Stat _damageDealt;
        private Stat _slicingDamage;
        private Stat _piercingDamage;
        private Stat _bluntDamage;
        private Stat _fireDamage;
        private Stat _waterDamage;
        private Stat _airDamage;
        private Stat _holyDamage;
        private Stat _darknessDamage;
        private Stat _acidDamage;
        private Stat _damageTaken;
        private Stat _slicingResistance;
        private Stat _piercingResistance;
        private Stat _bluntResistance;
        private Stat _fireResistance;
        private Stat _waterResistance;
        private Stat _airResistance;
        private Stat _holyResistance;
        private Stat _darknessResistance;
        private Stat _acidResistance;
        private Stat _hearRadius;
        private Stat _viewDistance;
        private Stat _viewAngle;

        [SerializeField] private float _regenerationCooldown = 0.2f;

        private float _regenerationTime;

        public List<Stat> Stats => _stats;
        public float HealthCurrent => _healthCurrent;
        public float EnergyCurrent => _energyCurrent;
        public float ManaCurrent => _manaCurrent;
        public Stat HealthMax => _healthMax;
        public Stat HealthRegeneration => _healthRegeneration;
        public Stat EnergyMax => _energyMax;
        public Stat EnergyRegeneration => _energyRegeneration;
        public Stat ManaMax => _manaMax;
        public Stat ManaRegeneration => _manaRegeneration;
        public Stat Acceleration => _acceleration;
        public Stat MoveSpeed => _moveSpeed;
        public Stat RotateSpeed => _rotateSpeed;
        public Stat AttackSpeed => _attackSpeed;
        public Stat DamageDealt => _damageDealt;
        public Stat SlicingDamage => _slicingDamage;
        public Stat PiercingDamage => _piercingDamage;
        public Stat BluntDamage => _bluntDamage;
        public Stat FireDamage => _fireDamage;
        public Stat WaterDamage => _waterDamage;
        public Stat AirDamage => _airDamage;
        public Stat HolyDamage => _holyDamage;
        public Stat DarknessDamage => _darknessDamage;
        public Stat AcidDamage => _acidDamage;
        public Stat DamageTaken => _damageTaken;
        public Stat SlicingResistance => _slicingResistance;
        public Stat PiercingResistance => _piercingResistance;
        public Stat BluntResistance => _bluntResistance;
        public Stat FireResistance => _fireResistance;
        public Stat WaterResistance => _waterResistance;
        public Stat AirResistance => _airResistance;
        public Stat HolyResistance => _holyResistance;
        public Stat DarknessResistance => _darknessResistance;
        public Stat AcidResistance => _acidResistance;
        public Stat HearRadius => _hearRadius;
        public Stat ViewDistance => _viewDistance;
        public Stat ViewAngle => _viewAngle;
        public float HealthPercent => _healthCurrent / _healthMax.CurrentValue;
        public float EnergyPercent => _energyCurrent / _energyMax.CurrentValue;
        public float ManaPercent => _manaCurrent / _manaMax.CurrentValue;

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
                    case "Mana Max":
                        _manaMax = s;
                        break;
                    case "Mana Regeneration":
                        _manaRegeneration = s;
                        break;
                    case "Acceleration":
                        _acceleration = s;
                        break;
                    case "Move Speed":
                        _moveSpeed = s;
                        break;
                    case "Rotate Speed":
                        _rotateSpeed = s;
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
                    case "Fire Damage":
                        _fireDamage = s;
                        break;
                    case "Water Damage":
                        _waterDamage = s;
                        break;
                    case "Air Damage":
                        _airDamage = s;
                        break;
                    case "Holy Damage":
                        _holyDamage = s;
                        break;
                    case "Darkness Damage":
                        _darknessDamage = s;
                        break;
                    case "Acid Damage":
                        _acidDamage = s;
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
                    case "Fire Resistance":
                        _fireResistance = s;
                        break;
                    case "Water Resistance":
                        _waterResistance = s;
                        break;
                    case "Air Resistance":
                        _airResistance = s;
                        break;
                    case "Holy Resistance":
                        _holyResistance = s;
                        break;
                    case "Darkness Resistance":
                        _darknessResistance = s;
                        break;
                    case "Acid Resistance":
                        _acidResistance = s;
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
            _manaCurrent = Mathf.Clamp(_manaCurrent, 0f, _manaMax.CurrentValue);
            OnHealthChanged?.Invoke(_healthCurrent, _healthMax.CurrentValue);
            OnEnergyChanged?.Invoke(_energyCurrent, _energyMax.CurrentValue);
            OnManaChanged?.Invoke(_manaCurrent, _manaMax.CurrentValue);
            OnStatsChanged?.Invoke();
        }

        public void OnUpdate()
        {
            if (_regenerationTime >= _regenerationCooldown)
            {
                RestoreHealthCurrent(_healthRegeneration.CurrentValue * _regenerationCooldown);
                RestoreEnergyCurrent(_energyRegeneration.CurrentValue * _regenerationCooldown);
                RestoreManaCurrent(_manaRegeneration.CurrentValue * _regenerationCooldown);
                _regenerationTime = 0f;
            }
            else
            {
                _regenerationTime += Time.deltaTime;
            }
        }

        public void ReduceHealthCurrent(float value, ElementConfig type, PawnController source = null)
        {
            if (_pawn.StateHolder.CompareStateValue("Is Dead", true) || value <= 0f)
            {
                return;
            }
            foreach (ArmorSlot slot in _pawn.Equipment.ArmorSlots)
            {
                if (slot.Config != null && slot.Config.EquipmentData.OwnerEffects.Count > 0)
                {
                    _pawn.Effects.ApplyEffects(slot.Config.EquipmentData.OwnerEffects, _pawn);
                }
            }
            if (source != null)
            {
                foreach (ArmorSlot slot in _pawn.Equipment.ArmorSlots)
                {
                    if (slot.Config != null && slot.Config.EquipmentData.TargetEffects.Count > 0)
                    {
                        source.Effects.ApplyEffects(slot.Config.EquipmentData.TargetEffects, _pawn);
                    }
                }
            }
            float resistance = GetStat(type.ResistanceStat.DisplayName).CurrentValue;
            if (resistance < 100f)
            {
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

        public void ReduceManaCurrent(float value)
        {
            if (_pawn.StateHolder.CompareStateValue("Is Dead", true) || value <= 0f)
            {
                return;
            }
            _manaCurrent = Mathf.Clamp(_manaCurrent - value, 0f, _manaMax.CurrentValue);
            OnManaChanged?.Invoke(_manaCurrent, _manaMax.CurrentValue);
        }

        public void RestoreManaCurrent(float value)
        {
            if (_pawn.StateHolder.CompareStateValue("Is Dead", true) || value <= 0f)
            {
                return;
            }
            _manaCurrent = Mathf.Clamp(_manaCurrent + value, 0f, _manaMax.CurrentValue);
            OnManaChanged?.Invoke(_manaCurrent, _manaMax.CurrentValue);
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
            _manaCurrent = 0f;
            OnHealthChanged?.Invoke(_healthCurrent, _healthMax.CurrentValue);
            OnEnergyChanged?.Invoke(_energyCurrent, _energyMax.CurrentValue);
            OnManaChanged?.Invoke(_manaCurrent, _manaMax.CurrentValue);
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
            RestoreManaCurrent(_manaMax.CurrentValue);
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