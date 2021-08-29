using CustomAttributes;
using FitnessApp.Domain;
using FitnessApp.UIConcretes.Screens.ExerciseDetails;
using FitnessApp.UICore.Screens;
using UnityEngine;

namespace FitnessApp.UIConcretes.Elements.Exercise
{
    public class ExerciseActions : MonoBehaviour
    {
        [SerializeField] private MyFitnessDomain domain;
        [SerializeField, AcceptOnly(typeof(IExerciseEditor))]
        private MonoBehaviour editExerciseScreenLogic;
        private IExerciseEditor _exerciseEditor;
        [SerializeField] private ScreenSlider screenSlider;
        [SerializeField] private GameObject editExerciseScreen;

        private void Awake()
        {
            _exerciseEditor = editExerciseScreenLogic as IExerciseEditor;
            _exerciseEditor.Require(this);
        }

        public void Delete(int exerciseId)
        {
            domain.PerformSingleAction().DeleteExercise(exerciseId);
        }

        public void Copy(int exerciseId)
        {
            var toCopy = domain.PerformSingleAction().GetExerciseData(exerciseId);
            domain.PerformSingleAction().CreateNewExercise(toCopy.name, toCopy.performance);
        }

        public void Edit(int exerciseId)
        {
            _exerciseEditor.StartEdit(exerciseId);
            screenSlider.SlideAScreenIn(editExerciseScreen);
        }
    }
}