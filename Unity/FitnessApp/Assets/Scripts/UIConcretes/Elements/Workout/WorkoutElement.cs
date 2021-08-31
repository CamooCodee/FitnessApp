using System;
using FitnessApp;
using FitnessApp.Domain;
using FitnessAppAPI;
using TMPro;
using UnityEngine;

namespace UIConcretes.Elements.Workout
{
    public class WorkoutElement : DropdownElementMono
    {
        [SerializeField] private MyFitnessDomain domain;
        
        [SerializeField] private TextMeshProUGUI nameDisplay;
        [SerializeField] private TextMeshProUGUI lastSessionDisplay;
        [SerializeField] private TextMeshProUGUI exerciseAmountDisplay;
        [SerializeField] private TextMeshProUGUI pauseAmountDisplay;
        
        private void Awake()
        {
            Require();
            InitializeDropdown();
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
            nameDisplay.text = data.name;
            lastSessionDisplay.text = GetLastSessionTextByData(data.lastSession);
            exerciseAmountDisplay.text = data.GetExerciseAmount().ToString();
            pauseAmountDisplay.text = data.GetPauseAmount().ToString();
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
            else return "just finished";
        }
    }
}