using System;
using System.Collections.Generic;
using FitnessAppAPI;
using UnityEngine;
using UnityEngine.Events;
using IWorkoutElement = UIConcretes.Elements.IWorkoutElement;

namespace FitnessApp.UIConcretes.Elements.Exercise
{
    public class WorkoutExerciseElement : ExerciseElement, IWorkoutElement
    {
        private readonly List<UnityEvent<int>> _onEditOffset = new List<UnityEvent<int>>();
        private PerformanceArgs _offset = new PerformanceArgs();

        public override void Copy() => InvokeEvent(onCopy, transform.GetSiblingIndex());
        public override void Delete() => InvokeEvent(onDelete, transform.GetSiblingIndex());
        public void EditOffset() => InvokeEvent(_onEditOffset, transform.GetSiblingIndex());

        public void ListenForEditOffset(UnityEvent<int> func) => _onEditOffset.Add(func);

        public void SetOffset(PerformanceArgs offset)
        {
            if(offset == null) return;
            _offset = offset;
            UpdateDisplay();
        }

        protected override void OnNewData(ExerciseData data, bool receivedNewId)
        {
            AdaptOffsetToExerciseComponents(data, receivedNewId);
        }

        void AdaptOffsetToExerciseComponents(ExerciseData data, bool resetOffsetValues)
        {
            var requiredComponentTypes = new List<PerformanceType>();
            var offsetComponentTypes = new List<PerformanceType>();
            for (var i = 0; i < data.performance.Count; i++)
            {
                requiredComponentTypes.Add(data.performance[i].GetPerformanceType());
            }
            
            for (var i = 0; i < _offset.Count; i++)
            {
                if(resetOffsetValues) _offset[i].InitializeMainValueWithDefault();
                
                if (!requiredComponentTypes.Contains(_offset[i].GetPerformanceType()))
                {
                    _offset.RemoveArgs(_offset[i]);
                }
                else offsetComponentTypes.Add(_offset[i].GetPerformanceType());
            }
            
            var factory = new SimplePerformanceComponentFactory();

            for (int i = 0; i < requiredComponentTypes.Count; i++)
            {
                if (!offsetComponentTypes.Contains(requiredComponentTypes[i]))
                {
                    _offset.AddArgs(factory.CreateEmptyArgsByPerformanceType(requiredComponentTypes[i]));
                }
            }
        }

        public PerformanceArgs GetOffset() => _offset;
        public PerformanceArgs GetOriginal() => CurrentData.performance;

        protected override string GetPerformanceValue(ExerciseData data, int currentComponentIndex)
        {
            var currentType = data.performance[currentComponentIndex].GetPerformanceType();
            var offsetCalculator = new OffsetCalculator(data.performance, _offset);
            
            return offsetCalculator.GetPerformanceValueFor(currentType);
        }

        public void ReadInto(SimpleWorkoutData data)
        {
            var element = new OffsetExerciseData(CurrentData, _offset);
            data.elements.Add(element);
        }

        public void Populate(SimpleWorkoutData data)
        {
            var elements = data.elements.ToArray();
            var elementData = elements[transform.GetSiblingIndex()] as OffsetExerciseData;

            if (elementData == null)
                throw new Exception("Failed to populate. The data was null or not an offset exercise.");
            
            SetOffset(elementData.offset);
        }
    }
}