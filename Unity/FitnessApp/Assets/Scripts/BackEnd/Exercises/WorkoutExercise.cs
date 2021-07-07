using System;
using System.Collections.Generic;

namespace FitnessApp.BackEnd
{
    public class WorkoutExercise : IWorkoutElement, IFollowAlongElement
    {
        private Exercise _bluePrint;

        public WorkoutExercise(Exercise bluePrint)
        {
            _bluePrint = bluePrint;
        }

        public void StartElement()
        {
            
        }

        public string GetElementInformation()
        {
            return "";
        }

        public IFollowAlongElement[] SplitElement()
        {
            return null;
        }

        public void StartListeningForElementEnding(Action func)
        {
            
        }
    }
}