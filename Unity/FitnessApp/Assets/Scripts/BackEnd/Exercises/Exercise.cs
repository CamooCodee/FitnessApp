using System;
using System.Collections.Generic;

namespace FitnessApp.BackEnd
{
    public class Exercise
    {
        private List<IPerformanceComponent> _performanceComponents;

        public string Name { get; private set; }
        public string Notes { get; private set; }

        public Exercise(string name, string notes)
        {
            Name = name;
            Notes = notes;
            _performanceComponents = new List<IPerformanceComponent>();
        }
        
        public void AddPerformanceComponent(IPerformanceComponent component)
        {
            if (AlreadyHasPerformanceComponentOfType(component.GetType())) return;
            _performanceComponents.Add(component);
        }
        public void RemovePerformanceComponent(IPerformanceComponent component)
        {
            if (!_performanceComponents.Contains(component)) return;
            _performanceComponents.Remove(component);
        }

        bool AlreadyHasPerformanceComponentOfType(Type t)
        {
            if (t == null) return false;
            
            for (int i = 0; i < _performanceComponents.Count; i++)
            {
                if (_performanceComponents[i].GetType() == t) return true;
            }

            return false;
        }
    }
}
