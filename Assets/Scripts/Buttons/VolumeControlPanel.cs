using Enums;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Buttons
{
    public class VolumeControlPanel : MonoBehaviour
    {
        private AudioManager _audioManager;

        [SerializeField] private TMP_Text _volumeText;

        [SerializeField] private Image _turnOffOnButtonImage;
        [SerializeField] private Sprite _enabledSprite;
        [SerializeField] private Sprite _disabledSprite;

        private void Awake()
        {
            _audioManager = AudioManager.Instance;
        }

        private void Start()
        {
            // set correct values of sound and music

            // update sprite
        }

        public void DoTurnUpMusic()
        {
            _audioManager.PlaySound(SoundType.ButtonClick);
            _audioManager.TurnUpMusic();
            UpdateVolumeText(_audioManager.MusicVolume);
        }

        public void DoTurnDownMusic()
        {
            _audioManager.PlaySound(SoundType.ButtonClick);
            _audioManager.TurnDownMusic();
            UpdateVolumeText(_audioManager.MusicVolume);
        }

        public void DoTurnOffOnMusic()
        {
            _audioManager.PlaySound(SoundType.ButtonClick);
            _audioManager.TurnOffOnMusic();
            UpdateSprite(_audioManager.MusicTurnedOn);
            UpdateVolumeText(_audioManager.MusicVolume);
        }

        public void DoTurnUpSound()
        {
            _audioManager.PlaySound(SoundType.ButtonClick);
            _audioManager.TurnUpSound();
            UpdateVolumeText(_audioManager.SoundVolume);
        }

        public void DoTurnDownSound()
        {
            _audioManager.PlaySound(SoundType.ButtonClick);
            _audioManager.TurnDownSound();
            UpdateVolumeText(_audioManager.SoundVolume);
        }

        public void DoTurnOffOnSound()
        {
            _audioManager.PlaySound(SoundType.ButtonClick);
            _audioManager.TurnOffOnSound();
            UpdateSprite(_audioManager.SoundTurnedOn);
            UpdateVolumeText(_audioManager.SoundVolume);
        }

        private void UpdateVolumeText(float newValue)
        {
            _volumeText.text = newValue.ToString();
        }

        private void UpdateSprite(bool audioEnabled)
        {
            _turnOffOnButtonImage.sprite = audioEnabled ? _enabledSprite : _disabledSprite;
        }
    }
}