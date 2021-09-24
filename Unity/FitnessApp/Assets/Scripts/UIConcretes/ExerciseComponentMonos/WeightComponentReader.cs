using FitnessAppAPI;
using TMPro;
using UnityEngine;

namespace FitnessApp.UIConcretes.ExerciseComponentMonos
{
    public class WeightComponentReader : ComponentReaderMono
    {
        [SerializeField] private TMP_InputField weightInput;
        
        protected override PerformanceComponentArgs ReadUserInputAndReturnAsComponentArgs(int id = -1)
        {
            float weight;
            if (weightInput.text == "") weight = 0f;
            else if (!float.TryParse(weightInput.text, out weight))
            {
                Debug.LogError("The weight input returned an invalid value!");
                return null;
            }

            return new WeightComponentArgs(weight, "-", id);
        }
    }
}