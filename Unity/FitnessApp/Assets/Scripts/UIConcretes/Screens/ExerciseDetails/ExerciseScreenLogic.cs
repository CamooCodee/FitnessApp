using System;
using System.Collections.Generic;
using FitnessApp.UICore;
using UnityEngine;
using FitnessAppAPI;
using TMPro;

namespace FitnessApp.UIConcretes.Screens.ExerciseDetails
{
    public class ExerciseScreenLogic : MonoBehaviour, IExercisePopulatable, IExerciseReadable, IExerciseScreenBehaviour
    {
        [SerializeField] private TMP_InputField nameInput;
        [SerializeField] private GameObject[] buttons;
        [SerializeField] private GameObject[] componentPrefabs;
        private Dictionary<PerformanceType, GameObject> _componentDictionary;
        private Dictionary<PerformanceType, GameObject> _buttonDictionary;

        [SerializeField] private Transform componentHolder;
        [Space(10f)]
        [SerializeField] private RectTransform componentContentHolder;
        [SerializeField] private UIRebuilder rebuilder;
        

        private IVisualComponentFactory _componentInstantiator;

        #region Awake Setup

        private void Awake()
        {
            InitializeComponentInstantiator();
            InitializeComponentDictionary();
            InitializeButtonDictionary();
            rebuilder.Require(this);
        }

        void InitializeComponentInstantiator()
        {
            _componentInstantiator = GetComponent<IVisualComponentFactory>();
            _componentInstantiator.Require(this);
        }
        
        void InitializeComponentDictionary()
        {
            _componentDictionary = new Dictionary<PerformanceType, GameObject>();

            for (var i = 0; i < componentPrefabs.Length; i++)
            {
                PerformanceTypeTag tagComp;
                if (!componentPrefabs[i].TryGetComponent(out tagComp))
                    continue;
                var typeTag = tagComp.GetPerformanceType();

                _componentDictionary.Add(typeTag, componentPrefabs[i]);
            }
        }
        
        void InitializeButtonDictionary()
        {
            _buttonDictionary = new Dictionary<PerformanceType, GameObject>();

            for (var i = 0; i < buttons.Length; i++)
            {
                PerformanceTypeTag tagComp;
                if (!buttons[i].TryGetComponent(out tagComp))
                    continue;
                var typeTag = tagComp.GetPerformanceType();

                _buttonDictionary.Add(typeTag, buttons[i]);
            }
        }
        
        #endregion
        
        public void Initialize(ExerciseDetailsScreen screen)
        {
            screen.ListenForScreenClose(ResetUI);
        }

        public void OnScreenOpen(ExerciseDetailsScreen screen)
        {
            screen.ListenForAdd(Rebuild);
            screen.ListenForRepsAdd(AddReps);
            screen.ListenForTimerAdd(AddTimer);
            screen.ListenForWeightAdd(AddWeight);
        }
        
        private void AddReps() => AddComponent(PerformanceType.Reps);
        private void AddWeight() => AddComponent(PerformanceType.Weight);
        private void AddTimer() => AddComponent(PerformanceType.Time);

        private void AddComponent(PerformanceType type)
        {
            InstantiateComponent(type);  
            HideButtonOfType(type);
        }
        
        #region UI Logic

        GameObject InstantiateComponent(PerformanceType type)
        {
            return _componentInstantiator.InstantiateComponent
                (_componentDictionary[type], componentHolder, ClickComponentDeleteButton);
        }
        
        void ClickComponentDeleteButton(GameObject toDelete)
        {
            string componentType = "Component";
            
            if(toDelete.TryGetComponent<PerformanceTypeTag>(out var t))
                    componentType = t.GetPerformanceType().ToString();

            if (ConfirmationPopup.instance == null)
            {
                OnDeleteConfirm(toDelete);
                return;
            }
            
            ConfirmationPopup.instance.PopUp($"Delete {componentType}",
                "Deleting a component will get rid of all offsets.",
                null,
                delegate { OnDeleteConfirm(toDelete); });
        }

        void OnDeleteConfirm(GameObject toDelete)
        {
            var type = toDelete.GetPerformanceTypeWithTagComponent();
            ShowButtonOfType(type);
            Destroy(toDelete);
            Rebuild();
        }
        
        void HideButtonOfType(PerformanceType type)
        {
            _buttonDictionary[type].SetActive(false);
        }
        void ShowButtonOfType(PerformanceType type)
        {
            _buttonDictionary[type].SetActive(true);   
        }

        #endregion

        #region UI Rebuilding

        private void Rebuild()
        {
            rebuilder.RebuildUILayout(componentContentHolder);
        }

        private void ResetUI()
        {
            nameInput.text = "";
            for (var i = 0; i < buttons.Length; i++)
            {
                buttons[i].SetActive(true);
            }

            for (int i = 0; i < componentHolder.childCount; i++)
            {
                Destroy(componentHolder.GetChild(i).gameObject);
            }

            Rebuild();
        }

        #endregion

        #region Population

        public void Populate(SimpleExerciseData data)
        {
            nameInput.text = data.name;
            PopulateComponents(data);
        }

        void PopulateComponents(SimpleExerciseData data)
        {
            var performance = data.performance;
            for (var i = 0; i < performance.Count; i++)
            {
                var instance = InstantiateComponent(performance[i].GetPerformanceType());
                var populatable = instance.GetComponent<IExercisePopulatable>();
                if(populatable != null) populatable.Populate(data);
                if(instance != null) HideButtonOfType(performance[i].GetPerformanceType());
            }
            
            Rebuild();
        }

        #endregion

        #region Reading

        public void ReadInto(SimpleExerciseData data)
        {
            data.name = nameInput.text.IsNullOrWhitespace() ? "Untitled Exercise" : nameInput.text;
            ReadComponents(data);
        }
        
        void ReadComponents(SimpleExerciseData data)
        {
            for (var i = 0; i < componentHolder.childCount; i++)
            {
                var readable = componentHolder.GetChild(i).GetComponent<IExerciseReadable>();
                readable?.ReadInto(data);
            }
        }

        #endregion
    }
}