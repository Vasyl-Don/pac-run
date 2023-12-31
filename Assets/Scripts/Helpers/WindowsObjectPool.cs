﻿using System.Collections.Generic;
using System.Linq;
using Enums;
using Models;
using UnityEngine;

namespace Helpers
{
    public class WindowsObjectPool : Singleton<WindowsObjectPool>
    {
        private readonly List<WindowModel> _prefabsForPool = new();
        private readonly List<WindowModel> _pooledObjects = new();

        private const string PrefabsFolderPath = "Prefabs/Windows";
        
        private void Start()
        {
            LoadPrefabs();
        }

        private void LoadPrefabs()
        {
            var prefabs = Resources.LoadAll<WindowModel>(PrefabsFolderPath);

            foreach (var prefab in prefabs)
            {
                _prefabsForPool.Add(prefab);
            }
        }

        public WindowModel GetObjectFromPool(WindowType windowType)
        {
            var window = _pooledObjects.FirstOrDefault(wnd => wnd.WindowType == windowType);
            if (window != null)
            {
                _pooledObjects.Remove(window);
                window.gameObject.SetActive(true);
                return window;
            }

            var windowPrefab = _prefabsForPool.FirstOrDefault(wnd => wnd.WindowType == windowType);
            if (windowPrefab != null)
            {
                var newWindow = Instantiate(windowPrefab, Vector3.zero, Quaternion.identity, transform);
                newWindow.WindowType = windowType;
                newWindow.transform.localPosition = Vector3.zero;
                return newWindow;
            }

            Debug.LogWarning($"Object pool does not contain {windowType} window.");
            return null;
        }

        public void PoolObject(WindowModel window)
        {
            window.gameObject.SetActive(false);
            _pooledObjects.Add(window);
        }

        public void ClearPool()
        {
            foreach (var window in _pooledObjects)
            {
                Destroy(window.gameObject);
            }

            _pooledObjects.Clear();
        }
    }
}