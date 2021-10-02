using System;
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
            _elementList.CopyElement(exerciseId);
        }
        
        public override void Delete(int exerciseId)
        {
            _elementList.RemoveElement(exerciseId);
        }

        public void EditOffset(int exerciseIndex)
        {
            var exerciseElement = _elementList.GetElementAt(exerciseIndex) as WorkoutExerciseElement;
            if(exerciseElement == null) throw new Exception("Trying to edit the offset of an element which isn't a WorkoutExerciseElement!");
            
            _exerciseOffsetter.StartOffset(exerciseIndex, exerciseElement);
            ScreenSlider.SlideAScreenIn(offsetExerciseScreen);
        }
    }
}