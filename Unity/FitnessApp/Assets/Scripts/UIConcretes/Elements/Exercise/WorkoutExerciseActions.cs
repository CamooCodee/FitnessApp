using CustomAttributes;
using FitnessApp.UIConcretes.Screens.ExerciseOffsets;
using FitnessApp.UIConcretes.Screens.WorkoutDetails;
using UnityEngine;

namespace FitnessApp.UIConcretes.Elements.Exercise
{
    public class WorkoutExerciseActions : ExerciseActions
    {
        [SerializeField, AcceptOnly(typeof(IWorkoutElementList))] private MonoBehaviour elementListReference;
        private IWorkoutElementList _elementList;

        [SerializeField] private GameObject offsetExerciseScreen;
        [SerializeField, AcceptOnly(typeof(IExerciseOffsetter))]
        private MonoBehaviour exerciseOffsetterReference;
        private IExerciseOffsetter _exerciseOffsetter;
        
        void Awake()
        {
            this.Setup();
        }

        private new void Setup()
        {
            base.Setup();
            _elementList = elementListReference as IWorkoutElementList;
            _elementList.Require();
            _exerciseOffsetter = exerciseOffsetterReference as IExerciseOffsetter;
            _exerciseOffsetter.Require(this);
        }
        
        public override void Copy(int exerciseId)
        {
            _elementList.CopyExercise(exerciseId);
        }
        
        public override void Delete(int exerciseId)
        {
            _elementList.RemoveExercise(exerciseId);
        }

        public void EditOffset(int exerciseIndex)
        {
            _exerciseOffsetter.StartOffset(exerciseIndex, _elementList.GetExerciseElementAt(exerciseIndex));
            ScreenSlider.SlideAScreenIn(offsetExerciseScreen);
        }
    }
}