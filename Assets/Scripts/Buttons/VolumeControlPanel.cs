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
        [SerializeField] private AudioClipType _audioClipType;

        [SerializeField] private Image _turnOffOnButtonImage;
        [SerializeField] private Sprite _enabledSprite;
        [SerializeField] private Sprite _disabledSprite;

        private void Awake()
        {
            _audioManager = AudioManager.Instance;
        }

        private void Start()
        {
            UpdateUI();
        }

        public void DoTurnUpVolume()
        {
            _audioManager.PlaySound(SoundType.ButtonClick);
            _audioManager.TurnUpVolume(_audioClipType);
            UpdateUI();
        }

        public void DoTurnDownVolume()
        {
            _audioManager.PlaySound(SoundType.ButtonClick);
            _audioManager.TurnDownVolume(_audioClipType);
            UpdateUI();
        }

        public void DoTurnOffOnVolume()
        {
            _audioManager.PlaySound(SoundType.ButtonClick);
            _audioManager.TurnOffOnVolume(_audioClipType);
            UpdateUI();
        }

        private void UpdateUI()
        {
            var audioEnabled = true;
            switch (_audioClipType)
            {
                case AudioClipType.Music:
                    audioEnabled = _audioManager.MusicTurnedOn;
                    UpdateVolumeText(_audioManager.MusicVolume);
                    break;
                case AudioClipType.Sound:
                    audioEnabled = _audioManager.SoundTurnedOn;
                    UpdateVolumeText(_audioManager.SoundVolume);
                    break;
                case AudioClipType.None:
                    Debug.LogWarning("_audioClipType is None.");
                    break;
                default:
                    Debug.LogWarning("Something wrong with the _audioClipType in VolumeControlPanel.");
                    break;
            }
            UpdateSprite(audioEnabled);
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