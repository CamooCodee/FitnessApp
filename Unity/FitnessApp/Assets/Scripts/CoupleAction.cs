using System;
using UnityEngine;
using UnityEngine.Events;

namespace FitnessApp
{
    [Serializable]
    public class CoupleAction
    {
        private Action _action;
        [SerializeField] private UnityEvent unityEvent = new UnityEvent();
        private CoupleAction _snapshot;
        
        public void AddListener(Action func)
        {
            _action += func;
        }
        public void AddListener(UnityAction func)
        {
            unityEvent.AddListener(func);
        }
        public void RemoveListener(Action func)
        {
            _action -= func;
        }
        public void RemoveListener(UnityAction func)
        {
            unityEvent.RemoveListener(func);
        }

        public void Invoke()
        {
            _snapshot?.Invoke();
            _action?.Invoke();
            unityEvent?.Invoke();
        }
        
        public void ClearUntilSnapshot()
        {
            _action = null;
            unityEvent = new UnityEvent();
        }

        public void CreateSnapshot()
        {
            _snapshot = new CoupleAction();
            _snapshot._action = _action;
            _snapshot.unityEvent = unityEvent;
            ClearUntilSnapshot();
        }
    }
}