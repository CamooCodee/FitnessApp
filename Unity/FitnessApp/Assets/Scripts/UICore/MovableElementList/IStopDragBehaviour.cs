using System;

namespace FitnessApp.UICore.MovableElementList
{
    public interface IStopDragBehaviour
    {
        void ListenForStop(Action func);
        void StopListeningForStop(Action func);
        void SetEnabled(bool val);
    }
}