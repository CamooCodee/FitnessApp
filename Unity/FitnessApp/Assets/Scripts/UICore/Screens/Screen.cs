using UnityEngine;
using UnityEngine.Events;

namespace FitnessApp.UICore.Screens
{
    public abstract class Screen : MonoBehaviour
    {
        [SerializeField] private UnityEvent onScreenOpen;
        [SerializeField] private UnityEvent onScreenClose;
        private bool _isOpen = false;
        
        public void Open()
        {
            if (_isOpen)
            {
                Debug.Log("Screen already was open!");
                return;
            }
            onScreenOpen?.Invoke();
            _isOpen = true;
        }
        public void Close()
        {
            if (!_isOpen)
            {
                Debug.Log("Screen already was closed!");
                return;
            }
            onScreenClose?.Invoke();
            _isOpen = false;
        }

        public void ListenForScreenOpen(UnityAction action)
        {
            onScreenOpen.AddListener(action);
        }
        public void ListenForScreenClose(UnityAction action)
        {
            onScreenClose.AddListener(action);
        }
    }
}