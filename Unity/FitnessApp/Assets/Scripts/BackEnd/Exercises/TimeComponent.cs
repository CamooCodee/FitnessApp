using System;
using System.Linq;

namespace FitnessApp.BackEnd
{
    public class TimeComponent : IPerformanceComponent
    {
        public int Time { get; private set; }
        private event Action OnPerformanceFinished;
        private bool _isFinished = false;

        public void StartListeningForFinishedPerformance(Action func)
        {
            OnPerformanceFinished += func;
        }

        public void StopListeningForFinishedPerformance(Action func)
        {
            if(OnPerformanceFinished == null) return;
            if(OnPerformanceFinished.GetInvocationList().Contains(func))
                OnPerformanceFinished -= func;
        }

        public string GetPerformanceInformation()
        {
            return $"Time: {Time}";
        }

        public void ForceFinish(bool fireEvent = true)
        {
            if(fireEvent)
                InvokeOnFinish();

            _isFinished = true;
        }
        
        private void InvokeOnFinish()
        {
            OnPerformanceFinished?.Invoke();
            OnPerformanceFinished = null;
        }
    }
}