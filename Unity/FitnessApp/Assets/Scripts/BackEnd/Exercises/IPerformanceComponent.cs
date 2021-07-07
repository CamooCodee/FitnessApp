namespace FitnessApp.BackEnd
{
    public interface IPerformanceComponent
    {
        bool PerformingFinished();
        string GetPerformanceInformation();
    }
}