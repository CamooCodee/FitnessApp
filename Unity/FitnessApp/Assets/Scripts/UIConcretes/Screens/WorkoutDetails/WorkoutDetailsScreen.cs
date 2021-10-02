using System;
using UnityEngine;
using Screen = FitnessApp.UICore.Screens.Screen;

namespace FitnessApp.UIConcretes.Screens.WorkoutDetails
{
    public class WorkoutDetailsScreen : Screen
    {
        private IWorkoutScreenBehaviour[] _behaviours;

        [SerializeField] private CoupleAction onAddExercise = new CoupleAction();
        [SerializeField] private CoupleAction onAddPause = new CoupleAction();
        [SerializeField] private CoupleAction onCancel = new CoupleAction();
        [SerializeField] private CoupleAction onApply = new CoupleAction();

        private void Awake()
        {
            SetupBehaviours<IWorkoutScreenBehaviour, WorkoutDetailsScreen>(out _behaviours);
            CreateSnapshotForAllEvents();
            ListenForScreenOpen(InvokeOpen);
            ListenForScreenClose(ClearAllEvents);
        }
    
        void ClearAllEvents()
        {
            onAddExercise.ClearUntilSnapshot();
            onAddPause.ClearUntilSnapshot();
            onApply.ClearUntilSnapshot();
            onCancel.ClearUntilSnapshot();
        }
        
        void CreateSnapshotForAllEvents()
        {
            onAddExercise.CreateSnapshot();
            onAddPause.CreateSnapshot();
            onApply.CreateSnapshot();
            onCancel.CreateSnapshot();
        }
        
        public void ListenForCancel(Action func)
        {
            onCancel.AddListener(func);
        }
        public void ListenForApply(Action func)
        {
            onApply.AddListener(func);
        }

        void InvokeOpen()
        {
            InvokeOpenEvent<IWorkoutScreenBehaviour, WorkoutDetailsScreen>(ref _behaviours);
        }
        
        public void InvokeOnAddExercise()
        {
            onAddExercise?.Invoke();
        }
        
        public void InvokeOnAddPause()
        {
            onAddPause?.Invoke();
        }
        
        public void InvokeOnApply()
        {
            onApply?.Invoke();
        }
        
        public void InvokeOnCancel()
        {
            onCancel?.Invoke();
        }
    }
}