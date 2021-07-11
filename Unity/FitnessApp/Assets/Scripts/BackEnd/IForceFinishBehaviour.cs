using System.Collections.Generic;

namespace FitnessApp.BackEnd
{
    public interface IForceFinishBehaviour
    {
        void ForceFinish(List<IPerformanceComponent> performanceComponents);
    }
}