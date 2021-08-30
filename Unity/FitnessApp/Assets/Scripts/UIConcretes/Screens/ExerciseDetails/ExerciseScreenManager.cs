using CustomAttributes;
using FitnessApp.Domain;
using TMPro;
using UnityEngine;

namespace FitnessApp.UIConcretes.Screens.ExerciseDetails
{
    /// <summary>
    /// Starting point for reading and populating.
    /// Responsible for handling edit and create mode. 
    /// </summary>
    public class ExerciseScreenManager : MonoBehaviour, IExerciseScreenBehaviour, IExerciseEditor
    {
        [SerializeField] private MyFitnessDomain domain;
        private IExerciseScreenApplyBehaviour _applyBehaviour;
        
        [SerializeField, AcceptOnly(typeof(IExercisePopulatable))] MonoBehaviour screenPopulatorReference;
        [SerializeField, AcceptOnly(typeof(IExerciseReadable))] MonoBehaviour screenReaderReference;
        private IExercisePopulatable _screenPopulator;
        private IExerciseReadable _screenReader;
        
        [Space(10f)]
        [SerializeField] private TextMeshProUGUI header;
        private bool ShouldSetHeader => header != null;
        
        private int _currentlyEditedId = -1;

        #region Awake Setup

        private void Awake()
        {
            InitializeInterfaceReferences();
            ResetApplyBehaviour();
        }

        void InitializeInterfaceReferences()
        {
            _screenPopulator = screenPopulatorReference as IExercisePopulatable;
            _screenPopulator.Require();
            _screenReader = screenReaderReference as IExerciseReadable;
            _screenReader.Require();
        }

        #endregion
        
        public void Initialize(ExerciseDetailsScreen screen)
        {
            screen.ListenForScreenClose(ResetApplyBehaviour);
            screen.ListenForScreenClose(SetCreateHeader);
        }

        public void OnScreenOpen(ExerciseDetailsScreen screen)
        {
            screen.ListenForApply(Apply);
        }

        #region Main Responsibilities

        public void StartEdit(int exercise)
        {
            _currentlyEditedId = exercise;
            PopulateScreen(exercise);
            _applyBehaviour = new EditExerciseApplyBehaviour();
            SetEditHeader();
        }
        
        private void Apply()
        {
            var read = ReadScreen();
            _applyBehaviour.Apply(domain, read, _currentlyEditedId);
        }
        
        #endregion

        private void ResetApplyBehaviour()
        {
            _currentlyEditedId = -1;
            _applyBehaviour = new CreateExerciseApplyBehaviour();
        }

        private void PopulateScreen(int exerciseDataId)
        {
            var data = domain.PerformSingleAction().GetExerciseData(exerciseDataId);
            _screenPopulator.Populate(new SimpleExerciseData(data));
        }
        
        private SimpleExerciseData ReadScreen()
        {
            var exerciseData = SimpleExerciseData.Empty;
            _screenReader.ReadInto(exerciseData);

            return exerciseData;
        }

        void SetEditHeader()
        {
            if(!ShouldSetHeader) return;

            header.text = "Edit Exercise";
        }
        void SetCreateHeader()
        {
            if(!ShouldSetHeader) return;

            header.text = "Create Exercise";
        }
    }
}
