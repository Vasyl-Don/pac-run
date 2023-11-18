using System.Collections.Generic;
using Enums;
using Helpers;
using Models;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class WindowsManager : Singleton<WindowsManager> 
    {
        private readonly List<WindowModel> _windowModels = new();
        private WindowsObjectPool _windowsObjectPool;
        
        private void Start()
        {
            _windowsObjectPool = WindowsObjectPool.Instance;
            AfterLoadScene();
        }

        public void ShowWindow(WindowType windowType)
        {
            var window = _windowsObjectPool.GetObjectFromPool(windowType);

            if (window != null)
            {
                if (AnyWindowShowing())
                {
                    var lastWindow = GetLastWindow();
                    if (lastWindow != null)
                        lastWindow.gameObject.SetActive(false);
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
            var window = _windowModels[^1];
            return window;
        }

        private bool AnyWindowShowing()
        {
            return _windowModels.Count > 0;
        }

        public void BeforeLoadScene()
        {
            HideLastWindow();
            _windowsObjectPool.ClearPool();
        }
        
        public void AfterLoadScene()
        {
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                ShowWindow(WindowType.MainMenu);
            }
        }
    }
}