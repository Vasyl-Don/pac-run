using System.Collections.Generic;
using Enums;
using Helpers;
using Models;
using UnityEngine;

namespace Managers
{
    public class WindowsManager : Singleton<WindowsManager>
    {
        private List<WindowModel> _windowModels = new List<WindowModel>();
        private WindowsObjectPool _windowsObjectPool;

        private void Start()
        {
            _windowsObjectPool = WindowsObjectPool.Instance;
            if (!AnyWindowShowing())
            {
                ShowWindow(WindowType.MainMenu);
            }
        }

        public void ShowWindow(WindowType windowType)
        {
            var window = _windowsObjectPool.GetObjectFromPool(windowType);

            if (window != null)
            {
                if (AnyWindowShowing())
                {
                    // HIDE THE FUCKING WINDOW
                    var lastWindow = GetLastWindow();
                    if (lastWindow != null)
                        lastWindow.gameObject.SetActive(false);
                    else Debug.LogWarning("Last window is null");
                }
                
                _windowModels.Add(window);
            }
            else Debug.LogWarning($"No window of type {windowType} found in object pool.");
        }
        
        public void HideLastWindow()
        {
            if (AnyWindowShowing())
            {
                var lastWindow = GetLastWindow();
                _windowModels.Remove(lastWindow);
                _windowsObjectPool.PoolObject(lastWindow);
            }
        }

        private WindowModel GetLastWindow()
        {
            var window = _windowModels[_windowModels.Count - 1];
            Debug.LogWarning($"_windowsModels Count is {_windowModels.Count}");
            Debug.LogWarning($"window I am trying to get is null == {window == null}");
            return window;
        }
        
        private bool AnyWindowShowing()
        {
            return _windowModels.Count > 0;
        }
    }
}
