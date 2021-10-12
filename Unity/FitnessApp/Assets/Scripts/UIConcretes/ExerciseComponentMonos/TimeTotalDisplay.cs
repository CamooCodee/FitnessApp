using FitnessApp.UIConcretes.Screens.ExerciseDetails;
using FitnessAppAPI;
using TMPro;
using UnityEngine;

namespace FitnessApp.UIConcretes.ExerciseComponentMonos
{
    public class TimeTotalDisplay : MonoBehaviour, IExercisePopulatable
    {
        [SerializeField] private TMP_InputField timeInput;
        [SerializeField] private TextMeshProUGUI display;
        private PerformanceArgs _original;
        
        private void Awake()
        {
            display.Require(this);
            timeInput.Require(this);
        }

        public void Populate(SimpleExerciseData data)
        {
            if(!(data is SimpleOffsetExerciseData oData)) return;
            _original = oData.offset;
            UpdateTotalDisplay();
        }

        public void UpdateTotalDisplay()
        {
            if(_original == null) return;
            
            var timeOffset = new PerformanceArgs();
            
            if (Extensions.TryParseTime(timeInput.text, out int length))
                timeOffset.AddArgs(new TimeComponentArgs(length));
            else
                Debug.LogError("The time component length input field returns an invalid string!");
            
            var calc = new OffsetCalculator(_original, timeOffset);
            var val = calc.GetPerformanceValueFor(PerformanceType.Time);
            Extensions.TryParseTime(val, out val);
            display.text = "Total: " + val;
        }
    }
}