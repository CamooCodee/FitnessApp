using DefaultNamespace;
using FitnessAppAPI;
using UnityEngine;

namespace FitnessApp.UIConcretes.ExerciseComponentMonos
{
    public abstract class ComponentReaderMono : MonoBehaviour, IExerciseReadable
    {
        public void ReadInto(SimpleExerciseData data)
        {
            int componentId = data.exerciseId;
            
            var performanceArgs = GetArgs(componentId);
            if(performanceArgs == null) return;
            
            bool isOverwritingExitingComponent = componentId >= 0;
            if(isOverwritingExitingComponent)
                data.performance.RemoveArgs(componentId);
            
            data.performance.AddArgs(performanceArgs);
        }

        protected abstract PerformanceComponentArgs GetArgs(int id = -1);
    }
}