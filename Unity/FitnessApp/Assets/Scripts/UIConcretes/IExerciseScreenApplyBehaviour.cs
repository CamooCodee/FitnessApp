using FitnessApp.Domain;

namespace FitnessApp.UIConcretes
{
    public interface IExerciseScreenApplyBehaviour
    {
        public void Apply(MyFitnessDomain domain, SimpleExerciseData dataToApply, int exerciseId = -1);
    }
}