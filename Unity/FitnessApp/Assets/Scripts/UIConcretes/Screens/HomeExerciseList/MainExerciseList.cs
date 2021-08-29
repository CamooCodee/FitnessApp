using FitnessApp.UIConcretes.Elements.Exercise;
using FitnessApp.UICore;
using FitnessAppAPI;
using UnityEngine;

namespace FitnessApp.UIConcretes.Screens.HomeExerciseList
{
    public class MainExerciseList : FitnessAppMonoBehaviour
    {
        private IExerciseElementFactory _elementFactory;

        [SerializeField] private UIRebuilder rebuilder;
        [SerializeField] private RectTransform listTransform;
        

        private void Awake()
        {
            InitializeElementFactory();
            UpdateExerciseElements();
            rebuilder.Require(this);
            listTransform.Require(this);
        }

        void InitializeElementFactory()
        {
            _elementFactory = GetComponent<IExerciseElementFactory>();
            _elementFactory.Require(this);
        }
        
        void InstantiateExerciseElements()
        {
            var exerciseData = AppAPI.GetExerciseData();

            foreach (var data in exerciseData)
            {
                _elementFactory.InstantiateElement(data, transform);
            }
        }

        void DestroyExerciseElements()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }
        
        public void UpdateExerciseElements()
        {
            DestroyExerciseElements();
            InstantiateExerciseElements();
            rebuilder.RebuildUILayout(listTransform);
        }
    }
}