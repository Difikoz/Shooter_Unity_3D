using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class ArmorSlot : MonoBehaviour
    {
        [SerializeField] private ArmorRenderer _defaultRenderer;
        [SerializeField] private List<ArmorRenderer> _armorRenderers = new();

        private PawnController _pawn;
        private ArmorItemConfig _config;

        public ArmorItemConfig Config => _config;

        public void Initialize()
        {
            _pawn = GetComponentInParent<PawnController>();
            ChangeConfig(null);
        }

        public void ChangeConfig(ArmorItemConfig config)
        {
            if (_config != null)
            {
                _pawn.Status.RemoveStatModifiers(_config.EquipmentData.Modifiers);
            }
            _config = config;
            if (_config != null)
            {
                _pawn.Status.AddStatModifiers(_config.EquipmentData.Modifiers);
            }
            _defaultRenderer.Toggle(_config == null);
            foreach (ArmorRenderer ar in _armorRenderers)
            {
                ar.Toggle(ar.Config == _config);
            }
        }
    }
}