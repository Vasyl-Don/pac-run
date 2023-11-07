using System.Collections.Generic;
using Models;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "Custom Containers/Sounds Container", fileName = "Sounds")]
    public class SoundsContainer : ScriptableObject
    {
        public List<SoundModel> Sounds;
    }
}