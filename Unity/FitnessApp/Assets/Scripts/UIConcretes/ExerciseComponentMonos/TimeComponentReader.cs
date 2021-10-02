using FitnessAppAPI;
using TMPro;
using UnityEngine;

namespace FitnessApp.UIConcretes.ExerciseComponentMonos
{
    public class TimeComponentReader : ComponentReaderMono
    {
        [SerializeField] private TMP_InputField timeInput;

        protected override PerformanceComponentArgs ReadUserInputAndReturnAsComponentArgs(int id = -1)
        {
            if (Extensions.TryParseTime(timeInput.text, out int length)) return new TimeComponentArgs(length, id);
            
            Debug.LogError("The time component length input field returns an invalid string!");
            return null;
        }
    }
}