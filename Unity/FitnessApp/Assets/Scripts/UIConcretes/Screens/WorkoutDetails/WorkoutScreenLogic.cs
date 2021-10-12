using System.Linq;
using CustomAttributes;
using FitnessAppAPI;
using TMPro;
using UnityEngine;

namespace FitnessApp.UIConcretes.Screens.WorkoutDetails
{
    public class WorkoutScreenLogic : MonoBehaviour, IWorkoutPopulatable, IWorkoutReadable, IWorkoutScreenBehaviour
    {
        [SerializeField] private TMP_InputField nameInput;
        [SerializeField] private string defaultName = "Untitled Workout";
        [Space(10f)]
        [SerializeField, AcceptOnly(typeof(IWorkoutElementList))] private MonoBehaviour elementListReference;
        private IWorkoutElementList _elementList;

        private void Awake()
        {
            _elementList = elementListReference as IWorkoutElementList;
            _elementList.Require(this);
            nameInput.Require(this);
            ResetUI();
        }
        
        public void Initialize(WorkoutDetailsScreen screen)
        {
            screen.ListenForScreenClose(ResetUI);
        }

        public void OnScreenOpen(WorkoutDetailsScreen screen)
        {
            
        }
        
        public void Populate(SimpleWorkoutData data)
        {
            nameInput.text = data.name;
            
            foreach (var workoutDataElement in data.elements)
            {
                if (workoutDataElement is OffsetExerciseData)
                {
                    _elementList.AddExercise(workoutDataElement.GetId());
                    _elementList.GetLastElement()?.Populate(data);
                }
                else if (workoutDataElement is PauseData)
                {
                    _elementList.AddPause();
                }
            }
            
            var elements = _elementList.GetElements();
            foreach (var element in elements)
            {
                element.Populate(data);
            }
        }

        public void ReadInto(SimpleWorkoutData data)
        {
            data.name = nameInput.text;
            if (data.name == string.Empty) data.name = defaultName;
            
            var elements = _elementList.GetElements();
            foreach (var element in elements)
            {
                element.ReadInto(data);
            }
        }

        void ResetUI()
        {
            nameInput.text = "";
            _elementList.Clear();
        }
    }
}