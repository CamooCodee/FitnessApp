using FitnessApp.UIConcretes.Screens.ExerciseDetails;
using FitnessAppAPI;
using TMPro;
using UnityEngine;

namespace FitnessApp.UIConcretes.ExerciseComponentMonos
{
    public class WeightTotalDisplay : MonoBehaviour, IExercisePopulatable
    {
        [SerializeField] private TMP_InputField weightInput;
        [SerializeField] private TextMeshProUGUI display;
        private PerformanceArgs _original;
        
        private void Awake()
        {
            display.Require(this);
            weightInput.Require(this);
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
            
            var weightOffset = new PerformanceArgs();
            
            if (float.TryParse(weightInput.text, out float weight))
                weightOffset.AddArgs(new WeightComponentArgs(weight, ""));
            else
                Debug.LogError("The weight component input field returns an invalid string!");
            
            var calc = new OffsetCalculator(_original, weightOffset);
            var val = calc.GetPerformanceValueFor(PerformanceType.Weight);
            display.text = "Total: " + val;
        }
    }
}