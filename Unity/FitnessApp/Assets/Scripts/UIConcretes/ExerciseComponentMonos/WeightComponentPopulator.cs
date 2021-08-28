using System.Globalization;
using FitnessAppAPI;
using TMPro;
using UnityEngine;

namespace FitnessApp.UIConcretes.ExerciseComponentMonos
{
    public class WeightComponentPopulator : ComponentPopulator, IExercisePopulatable
    {
        [SerializeField] private TMP_InputField weightField;
        
        public void Populate(SimpleExerciseData data)
        {
            var performance = GetArgs<WeightComponentArgs>(data, PerformanceType.Weight);
            if (performance == null)
            {
                Debug.LogWarning("Trying to populate component which doesn't have corresponding data!");
                return;
            }
            weightField.text = performance.Weight.ToString(CultureInfo.InvariantCulture);
        }
    }
}