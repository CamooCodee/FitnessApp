using FitnessAppAPI;
using UnityEngine;

namespace UIConcretes.Elements.Workout
{
    public interface IWorkoutElementFactory
    {
        public WorkoutElement InstantiateElement(WorkoutData data, Transform elementContainer);
    }
}