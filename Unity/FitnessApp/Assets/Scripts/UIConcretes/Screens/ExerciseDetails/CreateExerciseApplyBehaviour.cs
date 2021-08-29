using FitnessApp.Domain;

namespace FitnessApp.UIConcretes.Screens.ExerciseDetails
{
    public class CreateExerciseApplyBehaviour : IExerciseScreenApplyBehaviour
    {
        public void Apply(MyFitnessDomain domain, SimpleExerciseData dataToApply, int exerciseId = -1)
        {
            domain.PerformSingleAction().CreateNewExercise(dataToApply.name, dataToApply.performance);
        }
    }
}