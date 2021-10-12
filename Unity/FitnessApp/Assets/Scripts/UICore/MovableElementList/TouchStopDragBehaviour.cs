using System;
using System.Linq;
using UnityEngine;

namespace FitnessApp.UICore.MovableElementList
{
    public class TouchStopDragBehaviour : MonoBehaviour, IStopDragBehaviour
    {
        private event Action onStop;

        private int _previousTouchCount;
        private bool _shouldDetect;

        private void Update()
        {
            if(!_shouldDetect) return;
            
            if (_previousTouchCount == 1 && Input.touchCount <= 0) onStop?.Invoke();
            _previousTouchCount = Input.touchCount;
        }

        public void ListenForStop(Action func)
        {
            if(func == null || GetInvocationList(onStop).Contains(func)) return;
            
            onStop += func;
        }

        public void StopListeningForStop(Action func)
        {
            if(func == null || !GetInvocationList(onStop).Contains(func)) return;
            
            onStop -= func;
        }
        
        public void SetEnabled(bool val)
        {
            _shouldDetect = val;
        }

        Delegate[] GetInvocationList(Action e)
        {
            if (e == null) return new Delegate[0];
            
            return e.GetInvocationList();
        }
    }
}