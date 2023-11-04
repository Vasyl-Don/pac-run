using Enums;
using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Buttons
{
    [RequireComponent(typeof(Button))]
    public class ChangeWindowButton : MonoBehaviour
    {
        [SerializeField]
        private WindowType _windowTypeToShow;

        private WindowsManager _windowsManager;
        
        private void Start()
        {
            _windowsManager = WindowsManager.Instance;
        }
        
        public void ChangeWindow()
        {
            _windowsManager.HideLastWindow();
            _windowsManager.ShowWindow(_windowTypeToShow);
        }
    }
}