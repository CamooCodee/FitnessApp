using System.Collections.Generic;
using FitnessApp.UIConcretes.Elements.Exercise;
using FitnessApp.UICore;
using FitnessAppAPI;
using UnityEngine;

namespace FitnessApp.UIConcretes.Screens.HomeExerciseList
{
    public class MainExerciseList : FitnessAppMonoBehaviour
    {
        protected IExerciseElementFactory elementFactory;

        [SerializeField] private UIRebuilder rebuilder;
        [SerializeField] private RectTransform listRebuildTransform;

        private readonly List<ExerciseElement> _currentElements = new List<ExerciseElement>();
        private readonly List<int> _currentElementIds = new List<int>();

        private void Awake()
        {
            InitializeElementFactory();
            UpdateExerciseElements();
            rebuilder.Require(this);
            listRebuildTransform.Require(this);
        }

        void InitializeElementFactory()
        {
            elementFactory = GetComponent<IExerciseElementFactory>();
            elementFactory.Require(this);
        }
        
        void InstantiateOrUpdateExerciseElements()
        {
            var exerciseData = AppAPI.GetExerciseData();
            var allExerciseIds = new List<int>();
            foreach (var data in exerciseData)
            {
                allExerciseIds.Add(data.id);
                if(_currentElementIds.Contains(data.id)) 
                    UpdateExerciseElement(data);
                else InstantiateExerciseElement(data);
            }
            for (var i = 0; i < _currentElementIds.Count; i++)
            {
                if(!allExerciseIds.Contains(_currentElementIds[i]))
                   DestroyExerciseElement(_currentElementIds[i]); 
            }
        }

        void UpdateExerciseElement(ExerciseData data)
        {
            for (var i = 0; i < _currentElements.Count; i++)
            {
                if (_currentElementIds[i] == data.id)
                {
                    _currentElements[i].SetData(data);
                }
            }
        }
        void InstantiateExerciseElement(ExerciseData data)
        {
            var instance = elementFactory.InstantiateElement(data, transform);
            _currentElements.Add(instance);
            _currentElementIds.Add(instance.Id);
        }
        
        void DestroyExerciseElement(int id)
        {
            for (var i = 0; i < _currentElements.Count; i++)
            {
                if(_currentElements[i].Id != id) continue;
                Destroy(_currentElements[i].gameObject);
            }
        }
        
        void DestroyExerciseElements()
        {
            for (int i = 0; i < _currentElements.Count; i++)
            {
                Destroy(_currentElements[i].gameObject);
            }
        }
        
        public void UpdateExerciseElements()
        {
            InstantiateElements();
            Rebuild();
        }

        public void RedrawExerciseElements()
        {
            DestroyExerciseElements();
            InstantiateElements();
            Rebuild();
        }
        
        protected void Rebuild()
        {
            rebuilder.RebuildUILayout(listRebuildTransform);
        }

        protected virtual void InstantiateElements()
        {
            InstantiateOrUpdateExerciseElements();
        }

        protected ExerciseElement[] GetAllElements()
        {
            return GetComponentsInChildren<ExerciseElement>();
        }
    }
}