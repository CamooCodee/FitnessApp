using FitnessAppAPI;
using UnityEngine;

namespace FitnessApp.UIConcretes.Elements.Exercise
{
    public interface IExerciseElementFactory
    {
        public void InstantiateElement(ExerciseData data, Transform elementContainer);
    }
}