using System;
using UnityEngine;

namespace FitnessApp.BackEnd
{
    public class Set : IWorkoutElement
    { 
        public WorkoutExercise Exercise { get; private set; }
        public int PauseLength { get; private set; }
        public int Repetitions { get; private set; }
        
        public Set(WorkoutExercise exercise, int pauseLength, int repetitions)
        {
            Exercise = exercise;
            PauseLength = pauseLength;
            Repetitions = repetitions;
        }

        public IFollowAlongElement[] SplitElement()
        {
            var elements = new IFollowAlongElement[2 * Repetitions - 1];

            for (var i = 0; i < elements.Length; i++)
            {
                if(i % 2 == 0) elements[i] = Exercise;
                else elements[i] = new Pause(PauseLength);
            }

            return elements;
        }
    }
}