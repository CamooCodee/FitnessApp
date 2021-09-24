using System;
using UnityEngine;
using UnityEngine.Events;

namespace FitnessApp
{
    [Serializable]
    public class CoupleAction
    {
        private event Action action;
        [SerializeField] private UnityEvent unityEvent = new UnityEvent();
        private CoupleAction _snapshot;
        
        public void AddListener(Action func)
        {
            action += func;
        }
        public void AddListener(UnityAction func)
        {
            unityEvent.AddListener(func);
        }
        public void RemoveListener(Action func)
        {
            action -= func;
        }
        public void RemoveListener(UnityAction func)
        {
            unityEvent.RemoveListener(func);
        }

        public void Invoke()
        {
            _snapshot?.Invoke();
            action?.Invoke();
            unityEvent?.Invoke();
        }
        
        public void ClearUntilSnapshot()
        {
            action = null;
            unityEvent = new UnityEvent();
        }

        public void CreateSnapshot()
        {
            _snapshot = new CoupleAction();
            _snapshot.action = action;
            _snapshot.unityEvent = unityEvent;
            ClearUntilSnapshot();
        }
    }
}