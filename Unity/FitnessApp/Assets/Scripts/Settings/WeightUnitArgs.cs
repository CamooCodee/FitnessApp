namespace FitnessApp.Setting
{
    public class WeightUnitArgs : ISettingsEventArgs
    {
        public readonly string unit;
        
        public WeightUnitArgs(string unit)
        {
            this.unit = unit;
        }
    }
}