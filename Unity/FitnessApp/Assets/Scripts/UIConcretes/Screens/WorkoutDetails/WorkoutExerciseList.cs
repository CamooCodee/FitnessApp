using System;
using System.Collections.Generic;
using FitnessApp.UIConcretes.Elements.Exercise;
using FitnessApp.UIConcretes.Screens.HomeExerciseList;
using FitnessAppAPI;

namespace FitnessApp.UIConcretes.Screens.WorkoutDetails
{
    // TODO: Make this WorkoutElementList not ExerciseList and behaviour to support pauses
    public class WorkoutExerciseList : MainExerciseList, IWorkoutElementList
    {
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
            // The user might delete exercises which are in this workout
            var data = AppAPI.GetExerciseData(id, true);
            if(data == null) return;
            elementFactory.InstantiateElement(data, transform);
            Rebuild();
        }
        
        public void CopyExercise(int index)
        {
            var id = GetExerciseIdOfElementAt(index);
            var instance = elementFactory.InstantiateElement(AppAPI.GetExerciseData(id), transform);
            instance.transform.SetSiblingIndex(index + 1);
            Rebuild();
        }

        public void RemoveExercise(int index)
        {
            if(index < 0 || index > transform.childCount) throw new IndexOutOfRangeException();
            
            Destroy(transform.GetChild(index).gameObject);
            
            UpdateExerciseElements();
        }

        public WorkoutExerciseElement GetExerciseElementAt(int index)
        {
            if(index >= transform.childCount || index < 0) throw new IndexOutOfRangeException($"{index}");

            var element = transform.GetChild(index).GetComponent<WorkoutExerciseElement>();
            if(element == null) throw new NullReferenceException("Trying to get exercise id of a game object without an exercise element!");

            return element;
        }

        public WorkoutExerciseElement GetLastElement()
        {
            var count = transform.childCount;
            if (count == 0) return null;
            return transform.GetChild(count - 1).GetComponent<WorkoutExerciseElement>() ?? throw new Exception("The last element was not WorkoutExerciseElement");
        }

        public IEnumerable<WorkoutExerciseElement> GetElements()
        {
            return GetComponentsInChildren<WorkoutExerciseElement>();
        }

        private int GetExerciseIdOfElementAt(int index)
        {
            if(index >= transform.childCount || index < 0) throw new IndexOutOfRangeException($"{index}");

            var element = transform.GetChild(index).GetComponent<ExerciseElement>();
            if(element == null) throw new NullReferenceException("Trying to get exercise id of a game object without an exercise element!");

            return element.Id;
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