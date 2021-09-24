using FitnessAppAPI;
using UnityEngine;

namespace FitnessApp.UIConcretes.ExerciseComponentMonos
{
    public abstract class ComponentPopulator : MonoBehaviour
    {
        protected static T GetArgs<T>(SimpleExerciseData data, PerformanceType argType) where T : PerformanceComponentArgs
        {
            T args = default;

            for (var i = 0; i < data.performance.Count; i++)
            {
                if (data.performance[i].GetPerformanceType() == argType)
                {
                    args = data.performance[i] as T;
                }
            }

            return args;
        }
    }
}