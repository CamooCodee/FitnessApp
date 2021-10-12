using System;
using System.Collections.Generic;
using System.Linq;
using FitnessAppAPI;

namespace FitnessApp
{
    public class SimpleWorkoutData
    {
        public static SimpleWorkoutData Empty => new SimpleWorkoutData();

        public string name;
        public readonly List<IWorkoutDataElement> elements = new List<IWorkoutDataElement>();
        
        private SimpleWorkoutData()
        {
            name = string.Empty;
        }

        public SimpleWorkoutData(WorkoutData src)
        {
            if (src == null)
                throw new ArgumentException("Cannot create workout data. The source was null!");

            name = src.name;
            elements = src.elements.ToList();
        }
    }
}