using FitnessAppAPI;

namespace FitnessApp.UIConcretes
{
    public interface IVisualPerformanceComponent
    {
        void Populate(PerformanceComponentArgs args);
        PerformanceComponentArgs Read();
    }
}