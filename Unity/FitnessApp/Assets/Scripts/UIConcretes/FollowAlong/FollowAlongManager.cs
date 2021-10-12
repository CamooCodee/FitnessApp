using System;
using FitnessApp.Domain;
using FitnessApp.UIConcretes.FollowAlong.Logic;
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

        public void StartMode(int workoutId)
        {
            _currentWorkout = workoutId;
            var workout = domain.PerformSingleAction(false).GetWorkoutData(workoutId);
            _mode.StartMode(new SimpleWorkoutData(workout));
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