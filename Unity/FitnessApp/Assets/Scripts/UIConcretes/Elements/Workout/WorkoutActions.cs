using CustomAttributes;
using FitnessApp;
using FitnessApp.Domain;
using FitnessApp.UIConcretes.FollowAlong;
using FitnessApp.UIConcretes.Screens;
using FitnessApp.UICore.Screens;
using UnityEngine;

namespace UIConcretes.Elements.Workout
{
    public class WorkoutActions : MonoBehaviour
    {
        [SerializeField] private MyFitnessDomain domain;
        [SerializeField, AcceptOnly(typeof(IElementEditor))] private MonoBehaviour workoutEditorReference;
        private IElementEditor _workoutEditor;
        [SerializeField] private ScreenSlider screenSlider;
        [SerializeField] private GameObject editWorkoutScreen;
        [SerializeField] private GameObject followAlongScreen;
        [SerializeField] private FollowAlongManager followAlongManager; // TODO: Make this work against an interface

        private void Awake()
        {
            _workoutEditor = workoutEditorReference as IElementEditor;
            _workoutEditor.Require(this);
            screenSlider.Require(this);
            editWorkoutScreen.Require(this);
        }

        public void Delete(int workoutId)
        {
            domain.PerformSingleAction().DeleteWorkout(workoutId);
        }

        public void Copy(int workoutId)
        {
            var toCopy = domain.PerformSingleAction(false).GetWorkoutData(workoutId);
            var workout = domain.PerformSingleAction(false).CreateNewWorkout(toCopy.name);
            domain.PerformSingleAction().EditWorkout(workout, toCopy.name, toCopy.elements);
        }

        public void Edit(int workoutId)
        {
            _workoutEditor.StartEdit(workoutId);
            screenSlider.SlideAScreenIn(editWorkoutScreen);
        }

        public void Begin(int workoutId)
        {
            if(followAlongManager == null) return;

            screenSlider.SlideAScreenIn(followAlongScreen);
            followAlongManager.StartMode(workoutId);
        }
    }
}