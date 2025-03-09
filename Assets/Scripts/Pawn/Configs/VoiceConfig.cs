using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [CreateAssetMenu(fileName = "Voice", menuName = "Winter Universe/Pawn/New Voice")]
    public class VoiceConfig : BasicInfoConfig
    {
        [SerializeField] private List<AudioClip> _attackClips = new();
        [SerializeField] private List<AudioClip> _getHitClips = new();
        [SerializeField] private List<AudioClip> _deathClips = new();

        public List<AudioClip> AttackClips => _attackClips;
        public List<AudioClip> GetHitClips => _getHitClips;
        public List<AudioClip> DeathClips => _deathClips;
    }
}