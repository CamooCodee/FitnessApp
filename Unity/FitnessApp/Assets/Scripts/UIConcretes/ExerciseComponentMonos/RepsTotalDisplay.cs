using FitnessApp.UIConcretes.Screens.ExerciseDetails;
using FitnessAppAPI;
using TMPro;
using UnityEngine;

namespace FitnessApp.UIConcretes.ExerciseComponentMonos
{
    public class RepsTotalDisplay : MonoBehaviour, IExercisePopulatable
    {
        [SerializeField] private TMP_InputField repsInput;
        [SerializeField] private TextMeshProUGUI display;
        private PerformanceArgs _original;
        
        private void Awake()
        {
            display.Require(this);
            repsInput.Require(this);
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
            
            var repsOffset = new PerformanceArgs();
            
            if (int.TryParse(repsInput.text, out int repAmount))
                repsOffset.AddArgs(new RepsComponentArgs(repAmount));
            else
                Debug.LogError("The reps component input field returns an invalid string!");
            
            var calc = new OffsetCalculator(_original, repsOffset);
            var val = calc.GetPerformanceValueFor(PerformanceType.Reps);
            display.text = "Total: " + val;
        }
    }
}