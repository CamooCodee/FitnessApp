using FitnessApp.Domain;

namespace FitnessApp.UIConcretes.Screens.WorkoutDetails
{
    public interface IWorkoutScreenApplyBehaviour
    {
        public void Apply(MyFitnessDomain domain, SimpleWorkoutData toApply, int workoutId = -1);
    }
}