﻿using System;
using System.Collections.Generic;
using FitnessAppAPI;
using TMPro;
using UIConcretes.Elements;
using UnityEngine;

namespace FitnessApp.UIConcretes.Elements.Exercise
{
    public class ExerciseElement : DefaultElement
    {
        [Space(15f)]
        [Header("Information Display References"), Space(15f)]
        [SerializeField] private TextMeshProUGUI exerciseNameText;
        [Space(10f)]
        [SerializeField] private List<ComponentDisplay> componentDisplayList = new List<ComponentDisplay>();

        protected ExerciseData CurrentData { get; private set; }

        public void SetData(ExerciseData data)
        {
            CurrentData = data ?? throw new ArgumentException("Cannot set data to null!");
            bool receivedNewId = data.id != Id;
            Id = data.id;
            OnNewData(CurrentData, receivedNewId);
            UpdateDisplay();
        }
        
        protected void UpdateDisplay()
        {
            exerciseNameText.text = CurrentData.name;
            for (var i = 0; i < componentDisplayList.Count; i++)
            {
                bool componentDisplayIsUsed = false;
                for (int j = 0; j < CurrentData.performance.Count; j++)
                {
                    if (componentDisplayList[i].componentType != CurrentData.performance[j].GetPerformanceType()) continue;

                    var performanceVal = GetPerformanceValue(CurrentData, j);
                    if (componentDisplayList[i].treatAsTime)
                        Extensions.TryParseTime(performanceVal, out performanceVal);

                    componentDisplayList[i].textDisplay.text = performanceVal;
                    componentDisplayIsUsed = true;
                }
                componentDisplayList[i].displayGameObject.SetActive(componentDisplayIsUsed);
            }
        }

        protected virtual string GetPerformanceValue(ExerciseData data, int currentComponentIndex)
        {
            return data.performanceValues[currentComponentIndex];
        }

        protected virtual void OnNewData(ExerciseData data, bool receivedNewId)
        {
            
        }
        
        #region Initialization

        private void Awake()
        {
            Setup();
        }

        /// <summary>
        /// Equivalent to Awake
        /// </summary>
        protected virtual void Setup()
        {
            InitializeDropdown();
            ClearEmptyComponentDisplays();
            exerciseNameText.Require(this);
        }
        
        /// <summary>
        /// [Initialization Method]
        /// Removes entries which aren't fully set in the inspector.
        /// </summary>
        void ClearEmptyComponentDisplays()
        {
            for (var i = 0; i < componentDisplayList.Count; i++)
            {
                if (componentDisplayList[i].IsFullyInitialized) continue;
                
                componentDisplayList.RemoveAt(i);
                i--;
            }
        }

        #endregion
    }
}