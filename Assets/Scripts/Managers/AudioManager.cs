using System.Linq;
using Data;
using Enums;
using Helpers;
using ScriptableObjects;
using UnityEngine;

namespace Managers
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioManager : Singleton<AudioManager>, IDataPersistence
    {
        [SerializeField] private AudioSource _musicAudioSource;
        [SerializeField] private AudioSource _soundAudioSource;

        [SerializeField] private SoundsContainer _soundsContainer;
        [SerializeField] private MusicContainer _musicContainer;

        public float MusicVolume => Mathf.Round(_musicAudioSource.volume * 10f);
        public bool MusicTurnedOn { get; private set; }

        public float SoundVolume => Mathf.Round(_soundAudioSource.volume * 10f);
        public bool SoundTurnedOn { get; private set; }

        private void Start()
        {
            SetBackgroundMusic(MusicType.MainMenuBackground);
        }

        public void LoadData(GameData data)
        {
            _musicAudioSource.volume = data.AudioSettings.MusicVolume;
            MusicTurnedOn = data.AudioSettings.MusicTurnedOn;

            _soundAudioSource.volume = data.AudioSettings.SoundsVolume;
            SoundTurnedOn = data.AudioSettings.SoundTurnedOn;
        }

        public void SaveData(GameData data)
        {
            data.AudioSettings.MusicVolume = _musicAudioSource.volume;
            data.AudioSettings.MusicTurnedOn = MusicTurnedOn;

            data.AudioSettings.SoundsVolume = _soundAudioSource.volume;
            data.AudioSettings.SoundTurnedOn = SoundTurnedOn;
        }

        public void PlaySound(SoundType soundType)
        {
            if (!SoundTurnedOn)
                return;

            var sound = _soundsContainer.Sounds.FirstOrDefault(snd => snd.SoundType == soundType);
            if (sound != null)
            {
                _soundAudioSource.clip = sound.AudioClip;
                _soundAudioSource.Play();
            }
            else Debug.LogWarning($"No sound of type {soundType} found.");
        }

        public void SetBackgroundMusic(MusicType musicType)
        {
            var music = _musicContainer.Music.FirstOrDefault(msc => msc.MusicType == musicType);
            if (music != null)
            {
                _musicAudioSource.clip = music.AudioClip;
                if (MusicTurnedOn)
                    _musicAudioSource.Play();
            }
            else Debug.LogWarning($"No music of type {musicType} found.");
        }

        public void TurnUpVolume(AudioClipType audioClipType)
        {
            switch (audioClipType)
            {
                case AudioClipType.Music:
                    _musicAudioSource.volume += 0.1f;
                    break;
                case AudioClipType.Sound:
                    _soundAudioSource.volume += 0.1f;
                    break;
                case AudioClipType.None:
                    Debug.LogWarning("Passed clipType parameter is None.");
                    break;
                default:
                    Debug.LogWarning("Not set ClipType. Something went wrong.");
                    break;
            }
        }

        public void TurnDownVolume(AudioClipType audioClipType)
        {
            switch (audioClipType)
            {
                case AudioClipType.Music:
                    _musicAudioSource.volume -= 0.1f;
                    break;
                case AudioClipType.Sound:
                    _soundAudioSource.volume -= 0.1f;
                    break;
                case AudioClipType.None:
                    Debug.LogWarning($"Passed clipType parameter is None.");
                    break;
                default:
                    Debug.LogWarning("Not set ClipType. Something went wrong.");
                    break;
            }
        }

        public void TurnOffOnVolume(AudioClipType audioClipType)
        {
            switch (audioClipType)
            {
                case AudioClipType.Music:
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

                    break;
                }
                case AudioClipType.Sound:
                    SoundTurnedOn = !SoundTurnedOn;
                    _soundAudioSource.enabled = SoundTurnedOn;
                    break;
                case AudioClipType.None:
                    Debug.LogWarning($"Passed clipType parameter is None.");
                    break;
                default:
                    Debug.LogWarning("Not set ClipType. Something went wrong.");
                    break;
            }
        }
    }
}