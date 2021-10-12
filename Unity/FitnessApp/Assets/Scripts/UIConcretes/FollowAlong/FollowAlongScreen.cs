using System;
using UnityEngine;
using Screen = FitnessApp.UICore.Screens.Screen;

namespace FitnessApp.UIConcretes.FollowAlong
{
    public class FollowAlongScreen : Screen
    {
        private IFollowAlongScreenBehaviour[] _behaviours; 

        [SerializeField] private CoupleAction onCancel = new CoupleAction();

        private void Awake()
        {
            SetupBehaviours<IFollowAlongScreenBehaviour, FollowAlongScreen>(out _behaviours);
            CreateSnapshotForAllEvents();
            ListenForScreenClose(ClearAllEvents);
            ListenForScreenClose(InvokeOnCancel);
            ListenForScreenOpen(InvokeOpen);  
        }

        void InvokeOpen()
        {
            InvokeOpenEvent<IFollowAlongScreenBehaviour, FollowAlongScreen>(ref _behaviours);
        }
        
        void ClearAllEvents()
        {
            onCancel.ClearUntilSnapshot();
        }
        
        void CreateSnapshotForAllEvents()
        {
            onCancel.CreateSnapshot();
        }

        public void ListenForCancel(Action func)
        {
            onCancel.AddListener(func);
        }
        
        public void InvokeOnCancel()
        {
            onCancel?.Invoke();
        }
    }
}