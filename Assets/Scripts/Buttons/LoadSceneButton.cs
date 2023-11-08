using Enums;
using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Buttons
{
    [RequireComponent(typeof(Button))]
    public class LoadSceneButton : MonoBehaviour
    {
        [SerializeField] private int _sceneIdToLoad;
        [SerializeField] private MusicType _musicTypeToPlay;

        private AudioManager _audioManager;
        private SceneLoadManager _sceneLoadManager;

        private void Awake()
        {
            _audioManager = AudioManager.Instance;
            _sceneLoadManager = SceneLoadManager.Instance;
        }

        public void DoLoadScene()
        {
            _audioManager.PlaySound(SoundType.ButtonClick);
            _sceneLoadManager.LoadSceneById(_sceneIdToLoad);
            _audioManager.PlayBackgroundMusic(_musicTypeToPlay);
        }
    }
}