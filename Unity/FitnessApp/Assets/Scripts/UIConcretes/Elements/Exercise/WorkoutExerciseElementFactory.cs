using FitnessApp.Domain;
using FitnessAppAPI;
using UnityEngine;
using UnityEngine.Events;

namespace FitnessApp.UIConcretes.Elements.Exercise
{
    public class WorkoutExerciseElementFactory : ExerciseElementFactory
    {
        [Space(10f)]
        [Header("Workout Exercise Specific")]
        [SerializeField] private MyFitnessDomain domain;
        [Space(10f)]
        [SerializeField] private UnityEvent<int> onEditOffset;

        protected override void Setup()
        {
            base.Setup();
            domain.Require(this);
        }

        public override ExerciseElement InstantiateElement(ExerciseData data, Transform elementContainer)
        {
            var element = base.InstantiateElement(data, elementContainer);
            if (element == null || !(element is WorkoutExerciseElement))
            {
                Debug.LogWarning("Not instantiating movable exercise elements with a movable exercise element factory.");
                return element;
            }
            
            var movableExerciseElement = (WorkoutExerciseElement) element;
            movableExerciseElement.ListenForEditOffset(onEditOffset);
            var offset = new PerformanceArgs();
            for (var i = 0; i < data.performance.Count; i++)
            {
                offset.AddArgs(data.performance[i].GetCopyWithMainValueDefault());
            }
            movableExerciseElement.SetOffset(offset);
            return movableExerciseElement;
        }
    }

}