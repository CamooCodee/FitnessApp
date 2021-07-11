using System;

namespace FitnessApp.BackEnd
{
    public interface IPerformanceComponent
    {
        void StartListeningForFinishedPerformance(Action func);
        void StopListeningForFinishedPerformance(Action func);
        string GetPerformanceInformation();
        void ForceFinish(bool fireEvent = true);
    }
}