using System;
using FitnessApp;
using UnityEngine;
using Screen = FitnessApp.UICore.Screens.Screen;

namespace FitnessApp.UIConcretes.Screens.ExerciseOffsets
{
    public class ExerciseOffsetScreen : Screen
    {
        private IOffsetScreenBehaviour[] _behaviours;

        [SerializeField] private CoupleAction onApply = new CoupleAction();
        [SerializeField] private CoupleAction onCancel = new CoupleAction();
        
        private void Awake()
        {
            SetupBehaviours<IOffsetScreenBehaviour, ExerciseOffsetScreen>(out _behaviours);
            CreateSnapshotForAllEvents();
            ListenForScreenOpen(InvokeOpen);
            ListenForScreenClose(ClearAllEvents);
        }

        private void InvokeOpen()
        {
            InvokeOpenEvent<IOffsetScreenBehaviour, ExerciseOffsetScreen>(ref _behaviours);
        }
        
        void ClearAllEvents()
        {
            onApply.ClearUntilSnapshot();
            onCancel.ClearUntilSnapshot();
        }
        
        void CreateSnapshotForAllEvents()
        {
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
        public void InvokeOnCancel()
        {
            onCancel?.Invoke();
        }
        public void InvokeOnApply()
        {
            onApply?.Invoke();
        }
    }
}