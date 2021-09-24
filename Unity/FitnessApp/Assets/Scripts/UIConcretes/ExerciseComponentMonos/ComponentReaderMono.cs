using FitnessApp.UIConcretes.Screens.ExerciseDetails;
using FitnessAppAPI;
using UnityEngine;

namespace FitnessApp.UIConcretes.ExerciseComponentMonos
{
    public abstract class ComponentReaderMono : MonoBehaviour, IExerciseReadable
    {
        public void ReadInto(SimpleExerciseData data)
        {
            var performanceArgs = ReadUserInputAndReturnAsComponentArgs();
            if(performanceArgs == null) return;
            
            data.performance.AddArgs(performanceArgs);
        }

        protected abstract PerformanceComponentArgs ReadUserInputAndReturnAsComponentArgs(int id = -1);
    }
}