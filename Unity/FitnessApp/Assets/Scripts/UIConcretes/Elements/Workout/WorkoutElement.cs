using System;
using System.Collections.Generic;
using FitnessApp;
using FitnessAppAPI;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace UIConcretes.Elements.Workout
{
    public class WorkoutElement : DefaultElement
    {
        [SerializeField] private TextMeshProUGUI nameDisplay;
        [SerializeField] private TextMeshProUGUI lastSessionDisplay;
        [SerializeField] private TextMeshProUGUI exerciseAmountDisplay;
        [SerializeField] private TextMeshProUGUI pauseAmountDisplay;
        
        protected readonly List<UnityEvent<int>> onBegin = new List<UnityEvent<int>>();

        private DateTime _lastSession = DateTime.MinValue;
        
        private void Awake()
        {
            Require();
            InitializeDropdown();
        }

        private void OnEnable()
        {
            UpdateLastSessionDisplay(_lastSession);
        }

        private void Require()
        {
            nameDisplay.Require(this);
            lastSessionDisplay.Require(this);
            exerciseAmountDisplay.Require(this);
            pauseAmountDisplay.Require(this);
        }
        
        public void SetData(WorkoutData data)
        {
            Id = data.id;
            nameDisplay.text = data.name;
            _lastSession = data.lastSession;
            UpdateLastSessionDisplay(_lastSession);
            exerciseAmountDisplay.text = data.GetExerciseAmount().ToString();
            pauseAmountDisplay.text = data.GetPauseAmount().ToString();
        }

        void UpdateLastSessionDisplay(DateTime lastSession)
        {
            lastSessionDisplay.text = GetLastSessionTextByData(lastSession);
        }
        
        private static string GetLastSessionTextByData(DateTime date)
        {
            var span = DateTime.Now - date;
            
            if (date == DateTime.MinValue) return "not done yet";
            if (span.Days == 1) return $"{span.Days} day ago";
            if (span.Days > 1) return $"{span.Days} days ago";
            if (span.Hours == 1) return $"{span.Hours} hour ago";
            if (span.Hours > 1) return $"{span.Hours} hours ago";
            if (span.Minutes == 1) return $"{span.Minutes} minute ago";
            if (span.Minutes > 1) return $"{span.Minutes} minutes ago";
            if (span.Seconds > 10) return $"{span.Seconds} seconds ago";
            return "just finished";
        }
        
        public void ListenForBegin(UnityEvent<int> func)  => onBegin.Add(func);

        public void Begin() => InvokeEvent(onBegin);
    }
}