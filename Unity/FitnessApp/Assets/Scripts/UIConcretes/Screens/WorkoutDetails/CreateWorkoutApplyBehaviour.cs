using FitnessApp.Domain;

namespace FitnessApp.UIConcretes.Screens.WorkoutDetails
{
    public class CreateWorkoutApplyBehaviour : IWorkoutScreenApplyBehaviour
    {
        public void Apply(MyFitnessDomain domain, SimpleWorkoutData toApply, int workoutId = -1)
        {
            var api = domain.PerformSingleAction();
            var workout = api.CreateNewWorkout(toApply.name);
            api.EditWorkout(workout, toApply.name, toApply.elements);
        }
    }
}