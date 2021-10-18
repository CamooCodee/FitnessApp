using FitnessAppAPI;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace FitnessApp.UIConcretes.FollowAlong.Logic
{
    public class FollowAlongLogic : MonoBehaviour, IFollowAlongMode
    {
        private IFollowAlongListener[] _listeners;

        private int _currentElement;
        private IWorkoutDataElement[] _elements;

        [SerializeField] private TextMeshProUGUI workoutNameDisplay;
        [SerializeField] private UnityEvent onFinish;
        
        
        private void Awake()
        {
            _listeners = GetComponents<IFollowAlongListener>();
            workoutNameDisplay.Require(this);
        }

        public void StartMode(SimpleWorkoutData workout)
        {
            gameObject.SetActive(true); // Might be inactive due to this being inside of a screen
            _elements = workout.elements.ToArray();
            _currentElement = 0;
            workoutNameDisplay.text = workout.name;
            InvokeListeners();
        }

        public void CancelMode()
        {
            
        }

        public void PreviousElement()
        {
            if(_currentElement < 1) return;
            _currentElement--;
            ConfirmElement();
            InvokeListeners();
        }
        
        public void NextElement()
        {
            Next();
            ConfirmElement();
            InvokeListeners();
        }

        void Next()
        {
            if (_currentElement >= _elements.Length - 1)
            {
                FinishWorkout();
                return;
            }
            _currentElement++;
        }

        private void FinishWorkout()
        {
            _currentElement = _elements.Length;
            onFinish.Invoke();
        }

        private void ConfirmElement()
        {
            while (_currentElement <= _elements.Length - 1 && _elements[_currentElement] == null)
            {
                Debug.LogError("An element in the workout was null! Going to next element.");
                Next();
            }
        }
        
        private void InvokeListeners()
        {
            if (_currentElement >= _elements.Length) return;
            
            foreach (var listener in _listeners)
            {
                listener.OnNewElement(_elements, _currentElement);
            }
        }
        public void InvokeResetOnListeners()
        {
            foreach (var listener in _listeners)
            {
                listener.ResetListener();
            }
        }
    }
}