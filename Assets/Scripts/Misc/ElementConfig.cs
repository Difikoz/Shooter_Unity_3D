using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Element", menuName = "Winter Universe/Misc/New Element")]
    public class ElementConfig : BasicInfoConfig
    {
        [SerializeField] private StatConfig _damageStat;
        [SerializeField] private StatConfig _resistanceStat;
        [SerializeField] private List<GameObject> _hitVFX = new();
        [SerializeField] private List<AudioClip> _hitClips = new();

        public StatConfig DamageStat => _damageStat;
        public StatConfig ResistanceStat => _resistanceStat;
        public List<GameObject> HitVFX => _hitVFX;
        public List<AudioClip> HitClips => _hitClips;
    }
}