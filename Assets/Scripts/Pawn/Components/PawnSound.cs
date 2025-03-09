using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class PawnSound : MonoBehaviour
    {
        private PawnController _pawn;
        private VoiceConfig _config;
        private AudioSource _audioSource;

        public void Initialize(PawnData data)
        {
            _pawn = GetComponentInParent<PawnController>();
            _audioSource = GetComponentInChildren<AudioSource>();
            _config = GameManager.StaticInstance.ConfigsManager.GetVoice(data.Voice);
        }

        public void PlayAttackClip()
        {
            PlaySound(_config.AttackClips);
        }

        public void PlayGetHitClip()
        {
            PlaySound(_config.GetHitClips);
        }

        public void PlayDeathClip()
        {
            PlaySound(_config.DeathClips);
        }

        public void PlaySound(List<AudioClip> clips, float volume = 1f, bool randomizePitch = true, float minPitch = 0.8f, float maxPitch = 1.2f)
        {
            if (clips != null && clips.Count > 0)
            {
                PlaySound(clips[Random.Range(0, clips.Count)], volume, randomizePitch, minPitch, maxPitch);
            }
        }

        public void PlaySound(AudioClip clip, float volume = 1f, bool randomizePitch = true, float minPitch = 0.8f, float maxPitch = 1.2f)
        {
            if (clip == null)
            {
                return;
            }
            _audioSource.volume = volume;
            if (randomizePitch)
            {
                _audioSource.pitch = Random.Range(minPitch, maxPitch);
            }
            else
            {
                _audioSource.pitch = 1f;
            }
            _audioSource.PlayOneShot(clip);
        }
    }
}