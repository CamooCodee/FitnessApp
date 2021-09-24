using System;
using System.Linq;
using UnityEngine;

namespace FitnessApp.UICore.MovableElementList
{
    public class TouchStopDragBehaviour : MonoBehaviour, IStopDragBehaviour
    {
        private event Action onStop;

        private int _previousTouchCount = 0;
        
        private void Update()
        {
            if (_previousTouchCount == 1 && Input.touchCount != 1) onStop?.Invoke();
            _previousTouchCount = Input.touchCount;
        }

        public void ListenForStop(Action func)
        {
            if(onStop != null && func != null && onStop.GetInvocationList().Contains(func)) return;
            onStop += func;
        }

        public void StopListeningForStop(Action func)
        {
            if(onStop != null && func != null && !onStop.GetInvocationList().Contains(func)) return;
            onStop -= func;
        }

        public void SetEnabled(bool val)
        {
            enabled = val;
        }
    }
}