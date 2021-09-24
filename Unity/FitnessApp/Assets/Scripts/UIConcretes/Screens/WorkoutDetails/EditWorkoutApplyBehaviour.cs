using FitnessApp.Domain;
using UnityEngine;

namespace FitnessApp.UIConcretes.Screens.WorkoutDetails
{
    public class EditWorkoutApplyBehaviour : IWorkoutScreenApplyBehaviour
    {
        public void Apply(MyFitnessDomain domain, SimpleWorkoutData toApply, int workoutId = -1)
        {
            if (workoutId == -1)
            {
                Debug.LogWarning($"Cannot edit workout with an invalid id: '{workoutId}'");
                return;
            }
            
            var api = domain.PerformSingleAction();
            api.EditWorkout(workoutId, toApply.name, toApply.elements);
        }
    }
}