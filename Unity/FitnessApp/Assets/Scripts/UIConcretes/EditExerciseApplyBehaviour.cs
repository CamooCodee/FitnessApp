﻿using FitnessApp.Domain;

namespace FitnessApp.UIConcretes
{
    public class EditExerciseApplyBehaviour : IExerciseScreenApplyBehaviour
    {
        public void Apply(MyFitnessDomain domain, SimpleExerciseData dataToApply, int exerciseId = -1)
        {
            domain.PerformSingleAction().EditExercise(exerciseId, dataToApply.name, dataToApply.performance);
        }
    }
}