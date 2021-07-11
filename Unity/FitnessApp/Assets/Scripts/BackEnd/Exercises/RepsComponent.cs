using System;
using System.Linq;
using FitnessApp.BackEnd.Input;

namespace FitnessApp.BackEnd
{
    public class RepsComponent : IPerformanceComponent
    {
        public float Reps { get; private set; }
        private event Action OnPerformanceFinished;
        private bool _isFinished = false;
        private IInputEvent _input;

        public RepsComponent(IInputEvent input)
        {
            _input = input;
        }

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
            return $"Reps: {Reps}";
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