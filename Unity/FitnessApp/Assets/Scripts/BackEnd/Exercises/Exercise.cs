using System;
using System.Collections.Generic;

namespace FitnessApp.BackEnd
{
    public class Exercise
    {
        public List<IPerformanceComponent> PerformanceComponents { get; private set; }

        public string Name { get; private set; }
        public string Notes { get; private set; }

        public Exercise(string name, string notes)
        {
            Name = name;
            Notes = notes;
            PerformanceComponents = new List<IPerformanceComponent>();
        }
        
        public void AddPerformanceComponent(IPerformanceComponent component)
        {
            if (AlreadyHasPerformanceComponentOfType(component.GetType())) return;
            PerformanceComponents.Add(component);
        }
        public void RemovePerformanceComponent(IPerformanceComponent component)
        {
            if (!PerformanceComponents.Contains(component)) return;
            PerformanceComponents.Remove(component);
        }

        bool AlreadyHasPerformanceComponentOfType(Type t)
        {
            if (t == null) return false;
            
            for (int i = 0; i < PerformanceComponents.Count; i++)
            {
                if (PerformanceComponents[i].GetType() == t) return true;
            }

            return false;
        }
    }
}
