using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    public class PawnSound : MonoBehaviour
    {
        private PawnController _pawn;
        private AudioSource _audioSource;

        public void Initialize()
        {
            _pawn = GetComponentInParent<PawnController>();
            _audioSource = GetComponentInChildren<AudioSource>();
        }

        public void PlayAttackClip()
        {
            PlaySound(GameManager.StaticInstance.ConfigsManager.GetVoice(_pawn.Data.Voice).AttackClips);
        }

        public void PlayGetHitClip()
        {
            PlaySound(GameManager.StaticInstance.ConfigsManager.GetVoice(_pawn.Data.Voice).GetHitClips);
        }

        public void PlayDeathClip()
        {
            PlaySound(GameManager.StaticInstance.ConfigsManager.GetVoice(_pawn.Data.Voice).DeathClips);
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