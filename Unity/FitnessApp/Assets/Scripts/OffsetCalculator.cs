using System;
using FitnessAppAPI;

namespace FitnessApp
{
    public class OffsetCalculator
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
}