using System;
using UnityEngine;

namespace FitnessApp.BackEnd
{
    public class Pause : IWorkoutElement
    {
        public int Length { get; private set; }
        private event Action OnElementEnd;

        private float _timeLeft;
        
        public Pause(int length)
        {
            Length = length;
        }

        public void StartElement()
        {
            Debug.Log("Started!");

            _timeLeft = Length;
            
            UnityEventFunctionGlobalizer.GlobalUpdate += Update;
        }

        void EndElement()
        {
            OnElementEnd?.Invoke();
            OnElementEnd = null;
            
            UnityEventFunctionGlobalizer.GlobalUpdate -= Update;
        }
        
        public string GetElementInformation()
        {
            return $"A {Length} seconds pause!";
        }

        public void StartListeningForElementEnding(Action func)
        {
            OnElementEnd += func;
        }

        void Update()
        {
            HandleTimer();
        }

        void HandleTimer()
        {
            _timeLeft -= Time.deltaTime;

            if (_timeLeft <= 0f)
            {
                EndElement();
            }
        }
    }
}