using FitnessApp.Domain;
using FitnessApp.UICore;
using UnityEngine;

namespace FitnessApp.UIConcretes
{
    public class ExerciseActions : MonoBehaviour
    {
        [SerializeField] private MainExerciseList exerciseList;
        [SerializeField] private MyFitnessDomain domain;
        [SerializeField] private ExerciseDetailsScreenLogic editExerciseScreenLogic;
        [SerializeField] private ExerciseDetailsScreenVisuals editExerciseScreenVisuals;
        [SerializeField] private ScreenSlideResponse screenSlider;
        [SerializeField] private GameObject editExerciseScreen;
        
        
        public void Delete(int exerciseId)
        {
            domain.PerformSingleAction().DeleteExercise(exerciseId);
            exerciseList.UpdateExerciseElements();
        }

        public void Copy(int exerciseId)
        {
            var toCopy = domain.PerformSingleAction().GetExerciseData(exerciseId);
            domain.PerformSingleAction().CreateNewExercise(toCopy.name, toCopy.performance);
            exerciseList.UpdateExerciseElements();
        }

        public void Edit(int exerciseId)
        {
            editExerciseScreenVisuals.ResetUI();
            editExerciseScreenLogic.StartEdit(exerciseId);
            screenSlider.SlideAScreenIn(editExerciseScreen);
        }
    }
}