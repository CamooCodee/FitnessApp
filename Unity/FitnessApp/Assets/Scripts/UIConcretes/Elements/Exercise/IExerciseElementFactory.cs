using FitnessAppAPI;
using UnityEngine;

namespace FitnessApp.UIConcretes.Elements.Exercise
{
    public interface IExerciseElementFactory
    {
        public ExerciseElement InstantiateElement(ExerciseData data, Transform elementContainer);
    }
}