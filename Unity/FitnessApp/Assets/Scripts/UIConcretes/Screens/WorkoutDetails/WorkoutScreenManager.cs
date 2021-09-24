using CustomAttributes;
using FitnessApp.Domain;
using FitnessAppAPI;
using TMPro;
using UnityEngine;

namespace FitnessApp.UIConcretes.Screens.WorkoutDetails
{
    public class WorkoutScreenManager : MonoBehaviour, IWorkoutScreenBehaviour, IElementEditor
    {
        [SerializeField] private MyFitnessDomain domain;
        [SerializeField, AcceptOnly(typeof(IWorkoutReadable))] private MonoBehaviour readerReference;
        [SerializeField, AcceptOnly(typeof(IWorkoutReadable))] private MonoBehaviour populatorReference;
        private IWorkoutReadable _reader;
        private IWorkoutPopulatable _populator;
        
        [SerializeField] private TextMeshProUGUI screenHeader;

        private IWorkoutScreenApplyBehaviour _applyBehaviour = new CreateWorkoutApplyBehaviour();

        private int _currentlyEditedWorkout = -1;
        
        private void Awake()
        {
            _reader = readerReference as IWorkoutReadable;
            _reader.Require(this);
            _populator = populatorReference as IWorkoutPopulatable;
            _populator.Require(this);
            screenHeader.Require(this);
        }

        public void Initialize(WorkoutDetailsScreen screen)
        {
            
        }

        public void OnScreenOpen(WorkoutDetailsScreen screen)
        {
            screen.ListenForApply(Apply);
        }

        public void StartEdit(int elementId)
        {
            _currentlyEditedWorkout = elementId;
            _applyBehaviour = new EditWorkoutApplyBehaviour();
            screenHeader.text = "Edit Workout";
            Populate(elementId);
        }
        
        void Apply()
        {
            _applyBehaviour.Apply(domain, Read(), _currentlyEditedWorkout);
            ResetThis();
        }

        SimpleWorkoutData Read()
        {
            var data = SimpleWorkoutData.Empty;
            _reader.ReadInto(data);
            return data;
        }

        void Populate(int workoutId)
        {
            var populationData = domain.PerformSingleAction(false)
                .GetWorkoutData(workoutId);
            var data = new SimpleWorkoutData(populationData);
            _populator.Populate(data);
        }
        
        void ResetThis()
        {
            _applyBehaviour = new CreateWorkoutApplyBehaviour();
            _currentlyEditedWorkout = -1;
            screenHeader.text = "Create Workout";
        }
    }
}