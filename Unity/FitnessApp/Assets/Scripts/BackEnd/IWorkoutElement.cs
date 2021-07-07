using System;

namespace FitnessApp.BackEnd
{
    public interface IWorkoutElement
    {
        IFollowAlongElement[] SplitElement();
    }
}