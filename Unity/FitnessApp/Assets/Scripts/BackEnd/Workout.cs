using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace FitnessApp.BackEnd
{
    public class Workout
    {
        public List<IWorkoutElement> Elements { get; private set; }
        private WorkoutFollowAlong _followAlongMode;
        
        public Workout()
        {
            Elements = new List<IWorkoutElement>();
        }
        
        public void StartWorkout()
        {
            var elements = SplitElementsForFollowAlongMode();
            _followAlongMode = new WorkoutFollowAlong(elements);
            _followAlongMode.StartWorkout();
        }
        
        List<IFollowAlongElement> SplitElementsForFollowAlongMode()
        {
            var splitElements = new List<IFollowAlongElement>();
            
            for (var i = 0; i < Elements.Count; i++)
            {
                splitElements.AddRange(Elements[i].SplitForFollowAlongMode());
            }

            return splitElements;
        }
        
        public void AddElements(IWorkoutElement element)
        {
            if(Elements.Contains(element))
                Debug.Log($"Added the same element twice: '{element.GetType().Name}'");
            
            Elements.Add(element);
        }
        public void RemoveElements(IWorkoutElement element)
        {
            if(!Elements.Contains(element)) return;
            Elements.Remove(element);
        }

        private class WorkoutFollowAlong
        {
            public List<IFollowAlongElement> FollowAlongElements { get; private set; }
            private int _currentFollowAlongIndex;

            public WorkoutFollowAlong([NotNull] List<IFollowAlongElement> elements)
            {
                FollowAlongElements = elements;
                _currentFollowAlongIndex = -1;
            }
            
            public void StartWorkout()
            {
                NextFollowAlongElement();
            }

            void NextFollowAlongElement()
            {
                _currentFollowAlongIndex++;
                FollowAlongElements[_currentFollowAlongIndex]
                    .StartListeningForElementEnding(OnCurrentElementFinished);
                string elementInfo = FollowAlongElements[_currentFollowAlongIndex].GetElementInformation();
                Debug.Log(elementInfo);
                FollowAlongElements[_currentFollowAlongIndex].StartElement();
            }
        
            void OnCurrentElementFinished()
            {
                NextFollowAlongElement();
            }
        }
    }
}