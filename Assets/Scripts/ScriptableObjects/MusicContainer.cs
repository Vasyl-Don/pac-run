using System.Collections.Generic;
using Models;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "Custom Containers/Music Container", fileName = "Music")]
    public class MusicContainer : ScriptableObject
    {
        public List<MusicModel> Music;
    }
}