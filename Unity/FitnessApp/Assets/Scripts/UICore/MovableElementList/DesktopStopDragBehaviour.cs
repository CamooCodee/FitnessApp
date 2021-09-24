using System;
using System.Linq;
using UnityEngine;

namespace FitnessApp.UICore.MovableElementList
{
    public class DesktopStopDragBehaviour : MonoBehaviour, IStopDragBehaviour
    {
        private event Action onStop;

        private void Update()
        {
            if (Input.GetMouseButtonUp(0))
            {
                onStop?.Invoke();
            }
        }

        public void ListenForStop(Action func)
        {
            if(onStop != null && (func == null || onStop.GetInvocationList().Contains(func))) return;
            onStop += func;
        }

        public void StopListeningForStop(Action func)
        {
            if(onStop == null || func == null || !onStop.GetInvocationList().Contains(func)) return;
            onStop -= func;
        }

        public void SetEnabled(bool val)
        {
            enabled = val;
        }
    }
}