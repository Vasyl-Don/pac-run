using System.Collections;
using Helpers;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class SceneLoadManager : Singleton<SceneLoadManager>
    {
        private WindowsManager _windowsManager;
        
        private void Start()
        {
            _windowsManager = WindowsManager.Instance;
        }

        public void LoadSceneById(int sceneIdToLoad)
        {
            StartCoroutine(LoadSceneCoroutine(sceneIdToLoad));
        }
        
        private IEnumerator LoadSceneCoroutine(int sceneIdToLoad)
        {
            _windowsManager.BeforeLoadScene();
            var asyncLoad = SceneManager.LoadSceneAsync(sceneIdToLoad);
            while (!asyncLoad.isDone)
            {
                yield return null;
            }
            _windowsManager.AfterLoadScene();
        }
    }
}