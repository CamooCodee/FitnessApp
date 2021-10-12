using FitnessAppAPI;

namespace FitnessApp
{
    public class SimpleExerciseData
    {
        public static SimpleExerciseData Empty => new SimpleExerciseData();
        
        public string name;
        public readonly PerformanceArgs performance;
        
        private SimpleExerciseData()
        {
            name = string.Empty;
            performance = new PerformanceArgs();
        }
        
        public SimpleExerciseData(string name, PerformanceArgs performance)
        {
            this.name = name;
            this.performance = performance;
        }

        public SimpleExerciseData(ExerciseData source)
        {
            name = source.name;
            performance = source.performance;
        }
    }

    public class SimpleOffsetExerciseData : SimpleExerciseData
    {
        public readonly PerformanceArgs offset;

        public SimpleOffsetExerciseData(string name, PerformanceArgs offset, PerformanceArgs performance) : base(name, performance)
        {
            this.offset = offset;
        }
    }
}