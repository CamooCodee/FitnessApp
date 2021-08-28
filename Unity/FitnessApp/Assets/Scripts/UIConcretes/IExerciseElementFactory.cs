using FitnessAppAPI;
using UnityEngine;

namespace FitnessApp.UIConcretes
{
    public interface IExerciseElementFactory
    {
        public void InstantiateElement(ExerciseData data, Transform elementContainer);
    }
}