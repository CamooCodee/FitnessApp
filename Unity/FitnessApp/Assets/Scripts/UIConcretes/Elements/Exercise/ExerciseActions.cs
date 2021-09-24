using CustomAttributes;
using FitnessApp.Domain;
using FitnessApp.UIConcretes.Screens;
using FitnessApp.UIConcretes.Screens.ExerciseDetails;
using FitnessApp.UICore.Screens;
using UnityEngine;

namespace FitnessApp.UIConcretes.Elements.Exercise
{
    public class ExerciseActions : MonoBehaviour
    {
        [SerializeField] private MyFitnessDomain domain;
        [SerializeField, AcceptOnly(typeof(IElementEditor))]
        private MonoBehaviour editExerciseScreenLogic;
        private IElementEditor _exerciseEditor;
        [SerializeField] private ScreenSlider screenSlider;
        protected ScreenSlider ScreenSlider => screenSlider;
        [SerializeField] private GameObject editExerciseScreen;

        private void Awake()
        {
            Setup();
        }

        protected void Setup()
        {
            _exerciseEditor = editExerciseScreenLogic as IElementEditor;
            _exerciseEditor.Require(this);
        }
        
        public virtual void Delete(int exerciseId)
        {
            domain.PerformSingleAction().DeleteExercise(exerciseId);
        }

        public virtual void Copy(int exerciseId)
        {
            var toCopy = domain.PerformSingleAction(false).GetExerciseData(exerciseId);
            domain.PerformSingleAction().CreateNewExercise(toCopy.name, toCopy.performance);
        }

        public virtual void Edit(int exerciseId)
        {
            _exerciseEditor.StartEdit(exerciseId);
            screenSlider.SlideAScreenIn(editExerciseScreen);
        }
    }
}