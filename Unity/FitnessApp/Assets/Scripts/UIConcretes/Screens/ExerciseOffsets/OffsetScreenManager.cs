using System;
using FitnessApp.UIConcretes.Elements.Exercise;
using FitnessApp.UIConcretes.Screens.ExerciseDetails;
using FitnessApp.UICore;
using FitnessAppAPI;
using UnityEngine;

namespace FitnessApp.UIConcretes.Screens.ExerciseOffsets
{
    public class OffsetScreenManager : MonoBehaviour, IExerciseOffsetter, IOffsetScreenBehaviour
    {
        [SerializeField] private UIRebuilder rebuilder;
        [SerializeField] private RectTransform toRebuild;
        [SerializeField] private Transform componentHolder;
        private WorkoutExerciseElement _currentlyOffsetting;

        public void Initialize(ExerciseOffsetScreen screen)
        {
            
        }

        public void OnScreenOpen(ExerciseOffsetScreen screen)
        {
            screen.ListenForApply(ApplyOffsets);
        }
        
        public void StartOffset(int exerciseIndex, WorkoutExerciseElement element)
        {
            if (element == null)
                throw new ArgumentException("The given element was null. Cannot initialize Offset screen.");
            
            _currentlyOffsetting = element;

            InstantiateComponents(element);
            PopulateComponents(element);
        }

        private void InstantiateComponents(WorkoutExerciseElement element)
        {
            var offset = element.GetOffset();
            var performance = offset ?? new PerformanceArgs();
            ActivateNeededComponents(performance);
        }

        private void PopulateComponents(WorkoutExerciseElement element)
        {
            var offset = element.GetOffset();
            var performance = offset ?? new PerformanceArgs();
            
            for (int i = 0; i < componentHolder.childCount; i++)
            {
                var child = componentHolder.GetChild(i);
                if(!child.gameObject.activeSelf) continue;
                
                var toPopulate = child.GetComponent<IExercisePopulatable>();
                toPopulate?.Populate(new SimpleExerciseData("", performance));
            }
        }

        private void ApplyOffsets()
        {
            if(_currentlyOffsetting != null)
                _currentlyOffsetting.SetOffset(ReadComponents());
            _currentlyOffsetting = null;
        }
        
        private PerformanceArgs ReadComponents()
        {
            var targetData = SimpleExerciseData.Empty;
            
            for (int i = 0; i < componentHolder.childCount; i++)
            {
                var child = componentHolder.GetChild(i);
                if(!child.gameObject.activeSelf) continue;
                
                var toRead = child.GetComponent<IExerciseReadable>();
                toRead?.ReadInto(targetData);
            }

            return targetData.performance ?? new PerformanceArgs();
        }
        
        void ActivateNeededComponents(PerformanceArgs performance)
        {
            for (int i = 0; i < componentHolder.childCount; i++)
            {
                var child = componentHolder.GetChild(i);
                var childTypeTag = child.GetComponent<PerformanceTypeTag>();
                if(childTypeTag == null) continue;

                var childType = childTypeTag.GetPerformanceType();
                bool childIsNeeded = PerformanceContainsType(performance, childType);
                child.gameObject.SetActive(childIsNeeded);
            }
            
            Rebuild();
        }

        private static bool PerformanceContainsType(PerformanceArgs target, PerformanceType type)
        {
            if(target == null) throw new ArgumentException("The target cannot be null.", nameof(target));

            for (var i = 0; i < target.Count; i++)
            {
                if (target[i] == null) continue;
                if (target[i].GetPerformanceType() == type) return true;
            }

            return false;
        }

        private void Rebuild()
        {
            rebuilder.RebuildUILayout(toRebuild);
        }
    }
}