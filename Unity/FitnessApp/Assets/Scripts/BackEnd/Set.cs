using System;

namespace FitnessApp.BackEnd
{
    public class Set : IWorkoutElement
    { 
        public Exercise Exercise { get; private set; }
        public float PerformanceValueOffset { get; private set; }
        public int PauseLength { get; private set; }
        public int Repetitions { get; private set; }
        private event Action OnElementEnd;

        private int _repsLeft;
        
        public Set(Exercise exercise, float performanceValueOffset, int pauseLength, int repetitions)
        {
            Exercise = exercise;
            PerformanceValueOffset = performanceValueOffset;
            PauseLength = pauseLength;
            Repetitions = repetitions;
        }
        public Set(Exercise exercise, int pauseLength, int repetitions)
        {
            Exercise = exercise;
            PerformanceValueOffset = 0f;
            PauseLength = pauseLength;
            Repetitions = repetitions;
        }

        public void StartElement()
        {
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
            return $"A {Exercise.Name}-set with {Repetitions} repetitions.";
        }

        public void StartListeningForElementEnding(Action func)
        {
            OnElementEnd += func;
        }
        
        private void Update()
        {
            
        }
    }
}