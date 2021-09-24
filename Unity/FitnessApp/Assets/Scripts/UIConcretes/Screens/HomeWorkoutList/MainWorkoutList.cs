using System.Collections.Generic;
using FitnessApp.UIConcretes.Elements.Exercise;
using FitnessApp.UICore;
using FitnessAppAPI;
using UIConcretes.Elements.Workout;
using UnityEngine;

namespace FitnessApp.UIConcretes.Screens.HomeWorkoutList
{
    public class MainWorkoutList : FitnessAppMonoBehaviour
    {
        protected IWorkoutElementFactory elementFactory;

        [SerializeField] private UIRebuilder rebuilder;
        [SerializeField] private RectTransform listRebuildTransform;

        private readonly List<WorkoutElement> _currentElements = new List<WorkoutElement>();
        private readonly List<int> _currentElementIds = new List<int>();

        private void Awake()
        {
            InitializeElementFactory();
            UpdateWorkoutElements();
            rebuilder.Require(this);
            listRebuildTransform.Require(this);
        }

        void InitializeElementFactory()
        {
            elementFactory = GetComponent<IWorkoutElementFactory>();
            elementFactory.Require(this);
        }
        
        void InstantiateOrUpdateWorkoutElements()
        {
            var workoutData = AppAPI.GetWorkoutData();
            var allWorkoutIds = new List<int>();
            foreach (var data in workoutData)
            {
                allWorkoutIds.Add(data.id);
                if(_currentElementIds.Contains(data.id)) 
                    UpdateWorkoutElement(data);
                else InstantiateWorkoutElement(data);
            }
            for (var i = 0; i < _currentElementIds.Count; i++)
            {
                if(!allWorkoutIds.Contains(_currentElementIds[i]))
                   DestroyWorkoutElement(_currentElementIds[i]); 
            }
        }

        void UpdateWorkoutElement(WorkoutData data)
        {
            for (var i = 0; i < _currentElements.Count; i++)
            {
                if (_currentElementIds[i] == data.id)
                {
                    _currentElements[i].SetData(data);
                }
            }
        }
        void InstantiateWorkoutElement(WorkoutData data)
        {
            var instance = elementFactory.InstantiateElement(data, transform);
            _currentElements.Add(instance);
            _currentElementIds.Add(instance.Id);
        }
        
        void DestroyWorkoutElement(int id)
        {
            for (var i = 0; i < _currentElements.Count; i++)
            {
                if(_currentElements[i].Id != id) continue;
                Destroy(_currentElements[i].gameObject);
            }
        }
        
        void DestroyWorkoutElements()
        {
            for (int i = 0; i < _currentElements.Count; i++)
            {
                Destroy(_currentElements[i].gameObject);
            }
        }
        
        public void UpdateWorkoutElements()
        {
            InstantiateElements();
            Rebuild();
        }

        public void RedrawWorkoutElements()
        {
            DestroyWorkoutElements();
            InstantiateElements();
            Rebuild();
        }
        
        protected void Rebuild()
        {
            rebuilder.RebuildUILayout(listRebuildTransform);
        }

        protected virtual void InstantiateElements()
        {
            InstantiateOrUpdateWorkoutElements();
        }

        protected WorkoutElement[] GetAllElements()
        {
            return GetComponentsInChildren<WorkoutElement>();
        }
    }
}