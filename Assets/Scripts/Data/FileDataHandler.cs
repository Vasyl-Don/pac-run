using System;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace Data
{
    public class FileDataHandler
    {
        private readonly string _dataDirectoryPath;
        private readonly string _dataFilePath;

        public FileDataHandler(string dataDirectoryPath, string dataFilePath)
        {
            _dataDirectoryPath = dataDirectoryPath;
            _dataFilePath = dataFilePath;
        }

        public GameData Load()
        {
            var fullPath = Path.Combine(_dataDirectoryPath, _dataFilePath);
            GameData loadedData = null;
            if (File.Exists(fullPath))
            {
                try
                {
                    string data;
                    using (var stream = new FileStream(fullPath, FileMode.Open))
                    {
                        using (var reader = new StreamReader(stream))
                        {
                            data = reader.ReadToEnd();
                        }
                    }
                    loadedData = JsonConvert.DeserializeObject<GameData>(data);
                }
                catch (Exception e)
                {
                    Debug.LogError($"Error when trying to load data from {fullPath}.\n {e}");
                }
            }

            return loadedData;
        }

        public void Save(GameData data)
        {
            var fullPath = Path.Combine(_dataDirectoryPath, _dataFilePath);

            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

                var serializedData = JsonConvert.SerializeObject(data, Formatting.Indented);
                using var stream = new FileStream(fullPath, FileMode.Create);
                using var writer = new StreamWriter(stream);
                writer.Write(serializedData);
            }
            catch (Exception e)
            {
                Debug.LogError($"Error when trying to save data in {fullPath}.\n {e}");
            }
        }
    }
}