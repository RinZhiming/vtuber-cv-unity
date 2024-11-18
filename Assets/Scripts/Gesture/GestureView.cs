using UnityEngine;
using Zenject;

namespace Gesture
{
    public class GestureView : MonoBehaviour
    {
        [Inject] private IGestureRunner gesture;
        
        private void Awake()
        {
            gesture.Init();    
        }
    }
}
