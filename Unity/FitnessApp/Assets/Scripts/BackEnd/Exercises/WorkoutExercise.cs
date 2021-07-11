using System;
using System.Collections.Generic;

namespace FitnessApp.BackEnd
{
    public class WorkoutExercise : IWorkoutElement, IFollowAlongElement
    {
        private Exercise _bluePrint;
        private List<IPerformanceComponent> _performanceComponents;
        private event Action OnElementEnd;
        private int _finishedComponentAmount;
        private IForceFinishBehaviour _forceFinishBehaviour;
        
        public WorkoutExercise(Exercise bluePrint, IForceFinishBehaviour forceFinishBehaviour)
        {
            _bluePrint = bluePrint;
            _forceFinishBehaviour = forceFinishBehaviour;
            _performanceComponents = bluePrint.PerformanceComponents;
            _finishedComponentAmount = 0;
        }

        public void StartElement()
        {
            for (var i = 0; i < _performanceComponents.Count; i++)
            {
                _performanceComponents[i].StartListeningForFinishedPerformance(OnComponentFinished);
            }
            
            _forceFinishBehaviour.ForceFinish(_performanceComponents);
        }

        void EndElement()
        {
            OnElementEnd?.Invoke();
            
            for (var i = 0; i < _performanceComponents.Count; i++)
            {
                _performanceComponents[i].StopListeningForFinishedPerformance(OnComponentFinished);
            }
        }
        
        public string GetElementInformation()
        {
            return $"{_bluePrint.Name}";
        }

        public IFollowAlongElement[] SplitForFollowAlongMode()
        {
            return new [] { this };
        }

        public void StartListeningForElementEnding(Action func)
        {
            OnElementEnd += func;
        }

        void OnComponentFinished()
        {
            _finishedComponentAmount++;
            
            if(_finishedComponentAmount == _performanceComponents.Count) EndElement();
        }
    }
}