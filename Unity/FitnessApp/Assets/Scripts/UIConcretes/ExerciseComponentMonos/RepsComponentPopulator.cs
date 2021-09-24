using FitnessApp.UIConcretes.Screens.ExerciseDetails;
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
                Debug.LogWarning("Trying to populate a component with data that doesn't contain anything that is supported by the component!");
                return;
            }
            repsField.text = performance.RepAmount.ToString();
        }
    }
}