using UnityEngine;
using System;

namespace Ui
{
    [CreateAssetMenu(fileName = "new EventObject", menuName = "Snow/Event/Mobile Ui Event", order = 0)]
    public class MobileEventObject : ScriptableObject
    {
        public readonly string Id = Guid.NewGuid().ToString();
    }
}