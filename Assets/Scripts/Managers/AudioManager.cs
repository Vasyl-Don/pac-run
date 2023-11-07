using System.Linq;
using Enums;
using Helpers;
using ScriptableObjects;
using UnityEngine;

namespace Managers
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioManager : Singleton<AudioManager>
    {
        [SerializeField] private AudioSource _musicAudioSource;
        [SerializeField] private AudioSource _soundAudioSource;

        [SerializeField] private SoundsContainer _soundsContainer;

        public float MusicVolume => Mathf.Round(_musicAudioSource.volume * 10f);
        public bool MusicTurnedOn { get; private set; }

        public float SoundVolume => Mathf.Round(_soundAudioSource.volume * 10f);
        public bool SoundTurnedOn { get; private set; }

        private void Start()
        {
            _musicAudioSource = GetComponent<AudioSource>();
            // Set audio source volume from saved settings file

            // Get info if music and sound is turned on
            SoundTurnedOn = true;
            MusicTurnedOn = true;
            // 
            if (MusicTurnedOn)
            {
                _musicAudioSource.Play();
            }
        }

        public void PlaySound(SoundType soundType)
        {
            if (SoundTurnedOn)
            {
                var sound = _soundsContainer.Sounds.FirstOrDefault(snd => snd.SoundType == soundType);
                if (sound != null)
                {
                    _soundAudioSource.clip = sound.AudioClip;
                    _soundAudioSource.Play();
                }
                else Debug.LogWarning($"No sound of type {soundType} found.");
            }
        }

        public void TurnUpMusic()
        {
            _musicAudioSource.volume += 0.1f;
        }

        public void TurnDownMusic()
        {
            _musicAudioSource.volume -= 0.1f;
        }

        public void TurnOffOnMusic()
        {
            MusicTurnedOn = !MusicTurnedOn;
            if (!MusicTurnedOn)
            {
                _musicAudioSource.Stop();
                _musicAudioSource.enabled = false;
            }
            else
            {
                _musicAudioSource.enabled = true;
                _musicAudioSource.Play();
            }
        }

        public void TurnUpSound()
        {
            _soundAudioSource.volume += 0.1f;
        }

        public void TurnDownSound()
        {
            _soundAudioSource.volume -= 0.1f;
        }

        public void TurnOffOnSound()
        {
            SoundTurnedOn = !SoundTurnedOn;
            _soundAudioSource.enabled = SoundTurnedOn;
        }
    }
}