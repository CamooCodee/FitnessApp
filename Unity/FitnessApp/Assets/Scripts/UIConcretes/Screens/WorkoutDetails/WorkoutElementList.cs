using System;
using System.Collections.Generic;
using FitnessApp.UIConcretes.Elements.Exercise;
using FitnessApp.UIConcretes.Screens.HomeExerciseList;
using FitnessAppAPI;
using UIConcretes.Elements.Pause;
using UnityEngine;
using IWorkoutElement = UIConcretes.Elements.IWorkoutElement;

namespace FitnessApp.UIConcretes.Screens.WorkoutDetails
{
    // TODO: Make this WorkoutElementList not ExerciseList and behaviour to support pauses
    public class WorkoutElementList : MainExerciseList, IWorkoutElementList
    {
        private IPauseElementFactory _pauseFactory;
        
        protected override void Awake()
        {
            base.Awake();
            _pauseFactory = GetComponent<IPauseElementFactory>();
            _pauseFactory.Require(this);
        }

        protected override void InstantiateElements()
        {
            UpdateAllExerciseElements();
        }

        void UpdateAllExerciseElements()
        {
            var exerciseData = AppAPI.GetExerciseData();
            var dataDictionary = new Dictionary<int, ExerciseData>();
            foreach (var data in exerciseData)
            {
                dataDictionary.Add(data.id, data);
            }
            var elements = GetAllElements();
            for (var i = 0; i < elements.Length; i++)
            {
                var id = elements[i].Id;
                if(dataDictionary.ContainsKey(id))
                    elements[i].SetData(dataDictionary[id]);
            }
        }

        public void AddExercise(int id)
        {
            // canReturnNull = true => The user might delete exercises which are in this workout
            var data = AppAPI.GetExerciseData(id, true);
            if(data == null) return;
            exerciseFactory.InstantiateElement(data, transform);
            Rebuild();
        }

        public void AddPause()
        {
            _pauseFactory.InstantiateElement(new PauseData(10), transform);
            Rebuild();
        }

        public void CopyElement(int index)
        {
            var element = GetElementAt(index);
            
            if(element is WorkoutExerciseElement exercise)
                CopyExercise(exercise, index);
            else if(element is PauseElement pause)
                CopyPause(pause, index);
            
            Rebuild();
        }

        void CopyExercise(WorkoutExerciseElement exercise, int index)
        {
            var instance = exerciseFactory.InstantiateElement(AppAPI.GetExerciseData(exercise.Id), transform);
            instance.transform.SetSiblingIndex(index + 1);

            var workoutExerciseInstance = instance as WorkoutExerciseElement;
            if(workoutExerciseInstance != null)
                workoutExerciseInstance.SetOffset(exercise.GetOffset());
        }

        void CopyPause(PauseElement pause, int index)
        {
            var instance = _pauseFactory.InstantiateElement(new PauseData(pause.Length), transform);
            instance.transform.SetSiblingIndex(index + 1);
        }
        
        public void RemoveElement(int index)
        {
            if(index < 0 || index >= transform.childCount) throw new IndexOutOfRangeException($"'{index}'");
            
            Destroy(transform.GetChild(index).gameObject);
            
            Rebuild();
        }

        public IWorkoutElement GetElementAt(int index)
        {
            if(index >= transform.childCount || index < 0) throw new IndexOutOfRangeException($"{index}");

            var element = transform.GetChild(index).GetComponent<IWorkoutElement>();
            if(element == null) throw new NullReferenceException("Trying to get get IWorkoutElement of a game object without an implementing component!");

            return element;
        }

        public IWorkoutElement GetLastElement()
        {
            var count = transform.childCount;
            if (count == 0) return null;
            return transform.GetChild(count - 1).GetComponent<IWorkoutElement>() ?? throw new Exception("The last element was not an IWorkoutElement");
        }

        public IEnumerable<IWorkoutElement> GetElements()
        {
            return GetComponentsInChildren<IWorkoutElement>();
        }

        public void UpdateList()
        {
            UpdateExerciseElements();
        }

        public void Clear()
        {
            // temporary code TODO
            for (int i = 0; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
            
            Rebuild();
        }
    }
}