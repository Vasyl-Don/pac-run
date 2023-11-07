using Enums;
using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Buttons
{
    [RequireComponent(typeof(Button))]
    public class ChangeWindowButton : MonoBehaviour
    {
        [SerializeField] private WindowType _windowTypeToShow;

        private WindowsManager _windowsManager;
        private AudioManager _audioManager;

        private void Start()
        {
            _windowsManager = WindowsManager.Instance;
            _audioManager = AudioManager.Instance;
        }

        public void DoChangeWindow()
        {
            _audioManager.PlaySound(SoundType.ButtonClick);
            _windowsManager.HideLastWindow();
            _windowsManager.ShowWindow(_windowTypeToShow);
        }
    }
}