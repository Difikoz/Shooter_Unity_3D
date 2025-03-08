using UnityEngine;

namespace WinterUniverse
{
    public abstract class BasicInfoConfig : ScriptableObject
    {
        [SerializeField] protected string _displayName = "Name";
        [SerializeField, TextArea] protected string _description = "Description";
        [SerializeField] protected Color _color = Color.white;
        [SerializeField] protected Sprite _icon;

        public string DisplayName => _displayName;
        public string Description => _description;
        public Color Color => _color;
        public Sprite Icon => _icon;
    }
}