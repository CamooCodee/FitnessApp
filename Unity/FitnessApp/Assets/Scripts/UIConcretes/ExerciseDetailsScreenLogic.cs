using CustomAttributes;
using DefaultNamespace;
using FitnessApp.Domain;
using UnityEngine;

namespace FitnessApp.UIConcretes
{
    public class ExerciseDetailsScreenLogic : MonoBehaviour
    {
        [SerializeField] private MyFitnessDomain domain;
        private IExerciseScreenApplyBehaviour _applyBehaviour;
        
        [SerializeField, AcceptOnly(typeof(IExercisePopulatable))] MonoBehaviour screenPopulatorReference;
        [SerializeField, AcceptOnly(typeof(IExerciseReadable))] MonoBehaviour screenReaderReference;
        private IExercisePopulatable _screenPopulator;
        private IExerciseReadable _screenReader;

        private int _currentlyEditedId = -1;
        
        private void Awake()
        {
            InitializeInterfaceReferences();
        }

        void InitializeInterfaceReferences()
        {
            _screenPopulator = screenPopulatorReference as IExercisePopulatable;
            _screenPopulator.Require();
            _screenReader = screenReaderReference as IExerciseReadable;
            _screenReader.Require();
        }
        
        public void StartEdit(int exercise)
        {
            _currentlyEditedId = exercise;
            PopulateScreen(exercise);
            _applyBehaviour = new EditExerciseApplyBehaviour();
        }
        public void StartCreate()
        {
            _applyBehaviour = new CreateExerciseApplyBehaviour();   
        }
        
        public void Apply()
        {
            var exerciseData = SimpleExerciseData.Empty;
            _screenReader.ReadInto(exerciseData);
            
            _applyBehaviour.Apply(domain, exerciseData, _currentlyEditedId);
        }

        void PopulateScreen(int exerciseDataId)
        {
            var data = domain.PerformSingleAction().GetExerciseData(exerciseDataId);
            
            _screenPopulator.Populate(new SimpleExerciseData(data));
        }
    }
}
