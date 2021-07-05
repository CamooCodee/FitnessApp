using System;

namespace FitnessApp.BackEnd
{
    public interface IWorkoutElement
    {
        void StartElement();
        string GetElementInformation();
        void StartListeningForElementEnding(Action func);
    }
}