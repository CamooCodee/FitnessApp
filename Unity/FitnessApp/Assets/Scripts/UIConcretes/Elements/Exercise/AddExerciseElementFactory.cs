using FitnessAppAPI;
using UnityEngine;
using UnityEngine.Events;

namespace FitnessApp.UIConcretes.Elements.Exercise
{
    public class AddExerciseElementFactory : ExerciseElementFactory
    {
        [SerializeField] private UnityEvent<int> onAddExercise;

        public override ExerciseElement InstantiateElement(ExerciseData data, Transform elementContainer)
        {
            var element = base.InstantiateElement(data, elementContainer);
            if (element == null || !(element is AddExerciseElement))
            {
                Debug.LogWarning("Not instantiating add exercise elements with an add exercise element factory.");
                return element;
            }
            
            var addExerciseElement = (AddExerciseElement) element;
            addExerciseElement.ListenForAdd(onAddExercise);

            return addExerciseElement;
        }
    }
}