namespace FitnessApp.BackEnd
{
    public class Exercise : ISetElement
    {
        public float PerformanceValue { get; private set; }
        public string PerformancePrefix { get; private set; }

        public string Name { get; private set; }
        public string Notes { get; private set; }

        public Exercise(float performanceValue, string performancePrefix, string name, string notes)
        {
            this.PerformanceValue = performanceValue;
            PerformancePrefix = performancePrefix;
            Name = name;
            Notes = notes;
        }
    }
}
