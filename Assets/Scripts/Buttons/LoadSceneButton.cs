using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Buttons
{
    [RequireComponent(typeof(Button))]
    public class LoadSceneButton : MonoBehaviour
    {
        [SerializeField]
        private int _sceneIdToLoad;

        public void LoadSceneById()
        {
            SceneManager.LoadScene(_sceneIdToLoad);
        }
    }
}