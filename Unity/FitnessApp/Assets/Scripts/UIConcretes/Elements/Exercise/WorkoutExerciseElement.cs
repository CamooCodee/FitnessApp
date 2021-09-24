using System;
using System.Collections.Generic;
using FitnessApp.UIConcretes.Screens.WorkoutDetails;
using FitnessAppAPI;
using UnityEngine.Events;

namespace FitnessApp.UIConcretes.Elements.Exercise
{
    public class WorkoutExerciseElement : ExerciseElement, IWorkoutReadable, IWorkoutPopulatable
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

        public PerformanceArgs GetOffset()
        {
            return _offset;
        }

        protected override string GetPerformanceValue(ExerciseData data, int currentComponentIndex)
        {
            var currentType = data.performance[currentComponentIndex].GetPerformanceType();
            var offsetCalculator = new OffsetCalculator(data.performance, _offset);
            
            return offsetCalculator.GetPerformanceValueFor(currentType);
        }

        private class OffsetCalculator
        {
            private readonly PerformanceArgs _offsetArgs;
            private readonly PerformanceArgs _originalArgs;
            
            public OffsetCalculator(PerformanceArgs originalPerformance, PerformanceArgs offsetPerformance)
            {
                _offsetArgs = offsetPerformance ?? throw new ArgumentException("The offsetPerformance cannot be null!");
                _originalArgs = originalPerformance ?? throw new ArgumentException("The originalPerformance cannot be null!");
            }

            public string GetPerformanceValueFor(PerformanceType type)
            {
                if (_offsetArgs.Count == 0) return "";
                
                var factory = new SimplePerformanceComponentFactory();
                
                for (int i = 0; i < _originalArgs.Count; i++)
                {
                    if(_originalArgs[i].GetPerformanceType() != type) continue;
                    
                    for (var j = 0; j < _offsetArgs.Count; j++)
                    {
                        if (type != _offsetArgs[j].GetPerformanceType()) continue;
                        
                        var originalComponent = factory.CreateComponentByArgs(_originalArgs[i]);
                        var offsetComponent = factory.CreateComponentByArgs(_offsetArgs[j]);
                        return originalComponent.Merge(offsetComponent).GetMainPerformanceValue();
                    }
                }

                return "";
            }
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