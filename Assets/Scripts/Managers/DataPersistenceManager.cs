using System.Collections.Generic;
using System.Linq;
using Data;
using Helpers;
using UnityEngine;

namespace Managers
{
    public class DataPersistenceManager : Singleton<DataPersistenceManager>
    {
        [Header("File Storage Config")]
        [SerializeField] private string _fileName;
        
        private GameData _gameData;
        private FileDataHandler _fileDataHandler;
        
        private List<IDataPersistence> _dataPersistentObjects;
        
        private void Start()
        {
            _fileDataHandler = new FileDataHandler(Application.persistentDataPath, _fileName);
            _dataPersistentObjects = FindAllDataPersistentObjects();
            
            LoadGame();
        }
        
        public void NewGame()
        {
            _gameData = new GameData();
        }

        public void LoadGame()
        {
            _gameData = _fileDataHandler.Load();
            
            if (_gameData == null)
            {
                Debug.Log("No saved data found. Loading default data.");
                _gameData = new GameData();
            }
            
            foreach (IDataPersistence dataPersistenceObj in _dataPersistentObjects)
            {
                dataPersistenceObj.LoadData(_gameData);
            }
        }
        
        public void SaveGame()
        {
            foreach (var dataPersistenceObj in _dataPersistentObjects)
            {
                dataPersistenceObj.SaveData(_gameData);
            }
            
            _fileDataHandler.Save(_gameData);
        }

        private void OnApplicationQuit()
        {
            SaveGame();
        }

        private List<IDataPersistence> FindAllDataPersistentObjects()
        {
            var objects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();

            return new List<IDataPersistence>(objects);
        }
    }
}