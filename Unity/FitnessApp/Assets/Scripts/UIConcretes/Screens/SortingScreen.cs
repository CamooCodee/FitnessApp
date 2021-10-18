using UnityEngine;
using Screen = FitnessApp.UICore.Screens.Screen;

namespace FitnessApp.UIConcretes.Screens
{
    public class SortingScreen : Screen
    {
        [SerializeField] private GameObject[] exerciseSortButtons;
        [SerializeField] private GameObject[] workoutSortButtons;
        
        public void SetSortingForWorkout()
        {
            SetActiveExerciseButtons(false);
            SetActiveWorkoutButtons(true);
        }

        public void SetSortingForExercises()
        {
            SetActiveWorkoutButtons(false);
            SetActiveExerciseButtons(true);
        }

        void SetActiveWorkoutButtons(bool state)
        {
            foreach (var button in workoutSortButtons) 
                button.SetActive(state);
        }

        void SetActiveExerciseButtons(bool state)
        {
            foreach (var button in exerciseSortButtons) 
                button.SetActive(state);
        }
    }
}