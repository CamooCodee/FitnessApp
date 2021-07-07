using System;

namespace FitnessApp.BackEnd
{
    public interface IFollowAlongElement
    {
        void StartElement();
        string GetElementInformation();
        void StartListeningForElementEnding(Action func);
    }
}