using FitnessAppAPI;
using TMPro;
using UnityEngine;

namespace FitnessApp.UIConcretes.ExerciseComponentMonos
{
    public class TimeComponentPopulator : ComponentPopulator, IExercisePopulatable
    {
        private const int MAX_TIME_COMPONENT_LENGTH = 6039;
        [SerializeField] private TMP_InputField timeField;
        
        public void Populate(SimpleExerciseData data)
        {
            var performance = GetArgs<TimeComponentArgs>(data, PerformanceType.Time);
            if (performance == null)
            {
                Debug.LogWarning("Trying to populate component which doesn't have corresponding data!");
                return;
            }
            timeField.text = GetTimeStringByLength(performance.Length);
        }

        private static string GetTimeStringByLength(int length)
        {
            length = Mathf.Abs(length);
            if (length > MAX_TIME_COMPONENT_LENGTH)
            {
                Debug.LogWarning($"The time components length was too long. Max Length: '{MAX_TIME_COMPONENT_LENGTH}' | Given: '{length}'");
                return "";
            }

            int seconds = length % 60;
            int minutes = (length - seconds) / 60;

            return $"{ConvertToTwoDigitString(minutes)}:{ConvertToTwoDigitString(seconds)}";
        }

        static string ConvertToTwoDigitString(int n)
        {
            n = Mathf.Abs(n);
            if (n == 0) return "00";
            if (n < 10) return $"0{n}";
            if (n < 100) return n.ToString();
            else
            {
                string nS = n.ToString();
                return nS[nS.GetLastIndex()].ToString() + nS[nS.GetLastIndex() - 1].ToString();
            }
        }
    }
}