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
            if (element == null) return null;
            
            var workoutExerciseElement = element as WorkoutExerciseElement;
            if (!(element is WorkoutExerciseElement))
            {
                Debug.LogWarning("Not instantiating workout exercise elements with a workout exercise element factory.");
                return element;
            }
            
            workoutExerciseElement.ListenForEditOffset(onEditOffset);

            return workoutExerciseElement;
        }

        protected override void SetData(ExerciseData data, ExerciseElement element)
        {
            var offsetData = data as OffsetExerciseData;
            var weElement = element as WorkoutExerciseElement;
            if (offsetData == null || weElement == null)
            {
                base.SetData(data, element);
                return;
            }
            
            element.SetData(offsetData.originalData);
            var newOffset = new PerformanceArgs();
            for (var i = 0; i < offsetData.offset.Count; i++)
            {
                newOffset.AddArgs(offsetData.offset[i].GetCopyWithInvalidId());
            }
            weElement.SetOffset(newOffset);
        }
    }

}