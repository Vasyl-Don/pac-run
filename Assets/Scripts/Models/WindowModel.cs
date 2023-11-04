using System.Collections.Generic;
using Enums;
using UnityEngine;
using UnityEngine.UI;

namespace Models
{
    [RequireComponent(typeof(Canvas))]
    public class WindowModel : MonoBehaviour
    {
        public WindowType WindowType;
        
        [SerializeField]
        private List<Button> _buttons = new List<Button>();
        
        private void Awake()
        {
            var buttons = GetComponentsInChildren<Button>();
            _buttons.AddRange(buttons);
        }
    }
}