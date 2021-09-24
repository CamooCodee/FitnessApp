using System.Text.RegularExpressions;
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
            int length;
            
            if (!TryParseTime(timeInput.text, out length))
            {
                Debug.LogError("The time component length input field returns an invalid string!");
                return null;
            }
            return new TimeComponentArgs(length, id);
        }

        bool TryParseTime(string s, out int length)
        {
            length = 0;
            
            if (s == "") return true;

            var format = new Regex(@"^[0-9]{2}:[0-9]{2}");
            if (!format.IsMatch(s)) return false;

            var split = s.Split(':');
            if (split.Length != 2) return false;
            string minutesS = split[0];
            string secondsS = split[1];

            int minutes;
            int seconds = 0;
            bool result = int.TryParse(minutesS, out minutes);
            result = result && int.TryParse(secondsS, out seconds);

            if (!result) return false;

            length = minutes * 60 + seconds;
            return true;
        }
    }
}