using System;
using FitnessApp.Domain;
using FitnessApp.UIConcretes.FollowAlong.Logic;
using FitnessApp.UICore;
using UnityEngine;

namespace FitnessApp.UIConcretes.FollowAlong
{
    public class FollowAlongManager : MonoBehaviour, IFollowAlongScreenBehaviour
    {
        [SerializeField] private MyFitnessDomain domain;

        private IFollowAlongMode _mode;
        private int _currentWorkout;
        
        private void Awake()
        {
            domain.Require(this);
            _mode = GetComponent<IFollowAlongMode>();
            _mode.Require(this);
        }

        public void Initialize(FollowAlongScreen screen)
        {
            
        }

        public void OnScreenOpen(FollowAlongScreen screen)
        {
            screen.ListenForCancel(CancelMode);
        }
        
        /// <returns>Whether or not the workout could be started.</returns>
        public bool StartMode(int workoutId)
        {
            _currentWorkout = workoutId;
            var workout = domain.PerformSingleAction(false).GetWorkoutData(workoutId);

            if (workout.elements.Count == 0)
            {
                if(ConfirmationPopup.instance != null)
                    ConfirmationPopup.instance.PopUpNoOptions(
                        "Invalid Action",
                        "The workout you tried to start had no elements in it. Edit the workout to add more exercises.",
                        null);
                return false;
            }
            
            _mode.StartMode(new SimpleWorkoutData(workout));
            return true;
        }

        public void SetLastSessionDateOfCurrentWorkout()
        {
            domain.PerformSingleAction().StartWorkout(_currentWorkout);
        }

        private void CancelMode()
        {
            _mode.CancelMode();
        }
    }
}