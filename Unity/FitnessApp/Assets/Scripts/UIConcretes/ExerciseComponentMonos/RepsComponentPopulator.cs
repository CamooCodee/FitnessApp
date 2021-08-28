using FitnessAppAPI;
using TMPro;
using UnityEngine;

namespace FitnessApp.UIConcretes.ExerciseComponentMonos
{
    public class RepsComponentPopulator : ComponentPopulator, IExercisePopulatable
    {
        [SerializeField] private TMP_InputField repsField;
        
        public void Populate(SimpleExerciseData data)
        {
            var performance = GetArgs<RepsComponentArgs>(data, PerformanceType.Reps);
            if (performance == null)
            {
                Debug.LogWarning("Trying to populate component which doesn't have corresponding data!");
                return;
            }
            repsField.text = performance.RepAmount.ToString();
        }
    }
}