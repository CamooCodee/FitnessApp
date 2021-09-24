using System;
using UnityEngine;
using Screen = FitnessApp.UICore.Screens.Screen;

namespace FitnessApp.UIConcretes.Screens.ExerciseDetails
{
    public class ExerciseDetailsScreen : Screen
    {
        private IExerciseScreenBehaviour[] _behaviours; 
        
        [SerializeField] private CoupleAction onAddTimer = new CoupleAction();
        [SerializeField] private CoupleAction onAddReps = new CoupleAction();
        [SerializeField] private CoupleAction onAddWeight = new CoupleAction();
        [SerializeField] private CoupleAction onAdd = new CoupleAction();
        [SerializeField] private CoupleAction onApply = new CoupleAction();
        [SerializeField] private CoupleAction onCancel = new CoupleAction();

        private void Awake()
        {
            SetupBehaviours<IExerciseScreenBehaviour, ExerciseDetailsScreen>(out _behaviours);
            ListenForScreenClose(ClearAllEvents);
            ListenForScreenOpen(InvokeOpen);
            CreateSnapshotForAllEvents();
        }
        
        void InvokeOpen()
        {
            InvokeOpenEvent<IExerciseScreenBehaviour, ExerciseDetailsScreen>(ref _behaviours);
        }

        void ClearAllEvents()
        {
            onAddTimer.ClearUntilSnapshot();
            onAddReps.ClearUntilSnapshot();
            onAddWeight.ClearUntilSnapshot();
            onAdd.ClearUntilSnapshot();
            onApply.ClearUntilSnapshot();
            onCancel.ClearUntilSnapshot();
        }
        
        void CreateSnapshotForAllEvents()
        {
            onAddTimer.CreateSnapshot();
            onAddReps.CreateSnapshot();
            onAddWeight.CreateSnapshot();
            onAdd.CreateSnapshot();
            onApply.CreateSnapshot();
            onCancel.CreateSnapshot();
        }

        public void ListenForTimerAdd(Action func)
        {
            onAddTimer.AddListener(func);
        }
        public void ListenForWeightAdd(Action func)
        {
            onAddWeight.AddListener(func);
        }
        public void ListenForRepsAdd(Action func)
        {
            onAddReps.AddListener(func);
        }
        public void ListenForAdd(Action func)
        {
            onAdd.AddListener(func);
        }

        public void ListenForCancel(Action func)
        {
            onCancel.AddListener(func);
        }
        public void ListenForApply(Action func)
        {
            onApply.AddListener(func);
        }
        
        public void InvokeTimerAdd()
        {
            onAddTimer.Invoke();
            onAdd.Invoke();
        }
        public void InvokeWeightAdd()
        {
            onAddWeight.Invoke();
            onAdd.Invoke();
        }
        public void InvokeRepsAdd()
        {
            onAddReps.Invoke();
            onAdd.Invoke();
        }
        public void InvokeCancel()
        {
            onCancel.Invoke();
        }
        public void InvokeApply()
        {
            onApply.Invoke();
        }
    }
}