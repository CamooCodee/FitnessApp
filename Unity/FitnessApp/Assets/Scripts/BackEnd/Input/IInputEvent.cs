using System;

namespace FitnessApp.BackEnd.Input
{
    public interface IInputEvent
    {
        void StartListening(Action func);
        void StopListening(Action func);
    }
}