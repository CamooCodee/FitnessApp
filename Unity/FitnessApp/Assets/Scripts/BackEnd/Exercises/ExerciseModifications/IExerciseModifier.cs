using System;

namespace FitnessApp.BackEnd
{
    public interface IExerciseModifier
    {
        Type GetTypeThatCanBeModified();
        IPerformanceComponent GetModified(IPerformanceComponent toModify);
    }
}