using UnityEngine.Serialization;

namespace Data
{
    [System.Serializable]
    public class GameData
    {
        public int AvailableLevelsCount;

        public AudioSettings AudioSettings;
        
        public GameData()
        {
            AvailableLevelsCount = 1;

            AudioSettings.MusicVolume = 1f;
            AudioSettings.MusicTurnedOn = true;
            AudioSettings.SoundsVolume = 1f;
            AudioSettings.SoundTurnedOn = true;
        }
    }
}