using FitnessAppAPI;
using TMPro;
using UnityEngine;

namespace FitnessApp.UIConcretes.ExerciseComponentMonos
{
    public class RepsComponentReader : ComponentReaderMono
    {
        [SerializeField] private TMP_InputField repsInput;
        
        protected override PerformanceComponentArgs GetArgs(int id = -1)
        {
            int reps;
            if (!int.TryParse(repsInput.text, out reps))
            {
                Debug.LogError("The reps input returned an invalid value!");
                return null;
            }
            
            return new RepsComponentArgs(reps, id);
        }
    }
}