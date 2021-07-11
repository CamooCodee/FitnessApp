using System.Collections.Generic;
using System.Linq;

namespace FitnessApp.BackEnd
{
    public class DefaultForceFinish : IForceFinishBehaviour
    {
        public void ForceFinish(List<IPerformanceComponent> performanceComponents)
        {
            bool exerciseIsTimed =
                performanceComponents.Where(t => t.GetType() == typeof(TimeComponent)).Count() > 0;
            if (exerciseIsTimed)
            {
                FinishAllNonTimedComponents(performanceComponents);
            }
        }
        
        void FinishAllNonTimedComponents(List<IPerformanceComponent> performanceComponents)
        {
            for (var i = 0; i < performanceComponents.Count; i++)
            {
                if (performanceComponents[i].GetType() != typeof(TimeComponent))
                {
                    performanceComponents[i].ForceFinish();
                }
            }
        }
    }
}