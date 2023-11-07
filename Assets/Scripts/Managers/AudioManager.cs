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

        public float MusicVolume => _musicAudioSource.volume * 10f;
        private bool _musicTurnedOn;

        public float SoundVolume => _soundAudioSource.volume * 10f;
        private bool _soundTurnedOn;

        private void Start()
        {
            _musicAudioSource = GetComponent<AudioSource>();
            // Set audio source volume from saved settings file

            // Get info if music and sound is turned on
            _soundTurnedOn = true;
            _musicTurnedOn = true;
            // 
            if (_musicTurnedOn)
            {
                _musicAudioSource.Play();
            }
        }

        public void PlaySound(SoundType soundType)
        {
            if (_soundTurnedOn)
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
            _musicAudioSource.volume++;
        }

        public void TurnDownMusic()
        {
            _musicAudioSource.volume--;
        }

        public void TurnOffOnMusic()
        {
            _musicTurnedOn = !_musicTurnedOn;
            if (!_musicTurnedOn)
            {
                _musicAudioSource.Stop();
            }
            else _musicAudioSource.Play();
        }

        public void TurnUpSound()
        {
            _soundAudioSource.volume++;
        }

        public void TurnDownSound()
        {
            _soundAudioSource.volume--;
        }

        public void TurnOffOnSound()
        {
            _soundTurnedOn = !_soundTurnedOn;
            if (!_soundTurnedOn)
            {
                _soundAudioSource.Stop();
            }
            else _soundAudioSource.Play();
        }
    }
}