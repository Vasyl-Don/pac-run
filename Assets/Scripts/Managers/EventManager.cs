using Helpers;
using UnityEngine;

namespace Managers
{
    public class EventManager : Singleton<EventManager>
    {
        public Event OnShowWindow;
        public Event OnHideLastWindow;
    }
}