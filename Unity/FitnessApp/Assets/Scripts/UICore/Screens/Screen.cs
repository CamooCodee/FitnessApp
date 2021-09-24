using FitnessApp.UIConcretes.Screens;
using UnityEngine;
using UnityEngine.Events;

namespace FitnessApp.UICore.Screens
{
    public abstract class Screen : MonoBehaviour
    {
        [SerializeField] private UnityEvent onScreenOpen;
        [SerializeField] private UnityEvent onScreenClose;
        private bool _isOpen;
        
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
        
        protected void SetupBehaviours<TBehaviour, TScreen>(out TBehaviour[] behaviours) where TBehaviour : IScreenBehaviour<TScreen> where TScreen : Screen
        {
            FindBehaviours<TBehaviour, TScreen>(out behaviours);
            InitializeBehaviours<TBehaviour, TScreen>(ref behaviours);
        }
        
        void FindBehaviours<TBehaviour, TScreen>(out TBehaviour[] behaviours) where TBehaviour : IScreenBehaviour<TScreen> where TScreen : Screen
        {
            behaviours = GetComponents<TBehaviour>();
            if (behaviours != null) return;
            
            Debug.Log("No Screen Behaviours found!");
            behaviours = new TBehaviour[0];
        }
        
        void InitializeBehaviours<TBehaviour, TScreen>(ref TBehaviour[] behaviours) where TBehaviour : IScreenBehaviour<TScreen> where TScreen : Screen
        {
            var screen = this as TScreen;
            if(screen == null)
                Debug.LogWarning($"Initializing behaviours with a type of a different screen. '{typeof(TBehaviour).Name}'");
            
            for (var i = 0; i < behaviours.Length; i++)
                behaviours[i].Initialize(screen);
        }

        protected void InvokeOpenEvent<TBehaviour, TScreen>(ref TBehaviour[] behaviours) where TBehaviour : IScreenBehaviour<TScreen> where TScreen : Screen
        {
            var screen = this as TScreen;
            if(screen == null)
                Debug.LogWarning($"Initializing behaviours with a type of a different screen. '{typeof(TBehaviour).Name}'");

            
            for (var i = 0; i < behaviours.Length; i++) 
                behaviours[i].OnScreenOpen(screen);
        }
    }
}