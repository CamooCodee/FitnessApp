using System.Collections.Generic;
using DefaultNamespace;
using FitnessApp.UICore;
using UnityEngine;
using FitnessAppAPI;
using TMPro;

namespace FitnessApp.UIConcretes
{
    [AddComponentMenu("Element Controller - Exercises")]
    public class ExerciseDetailsScreenVisuals : MonoBehaviour, IExercisePopulatable, IExerciseReadable
    {
        [SerializeField] private TMP_InputField nameInput;
        [SerializeField] private GameObject[] buttons;
        [SerializeField] private GameObject[] componentPrefabs;
        private Dictionary<PerformanceType, GameObject> _componentDictionary;
        private Dictionary<PerformanceType, GameObject> _buttonDictionary;

        [SerializeField] private Transform componentHolder;

        [SerializeField] private RectTransform componentContentHolder;
        [SerializeField] private UIRebuilder rebuilder;
        

        private IVisualComponentFactory _componentInstantiator;

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

        public void ClickAddButton(GameObject button)
        {
            CreateComponentByButtonGameObject(button);
            HideButton(button);
        }
        
        void CreateComponentByButtonGameObject(GameObject button)
        {
            var type = button.GetPerformanceTypeWithTagComponent();
            InstantiateComponent(type);
        }
        
        void ClickDeleteButton(GameObject toDelete)
        {
            var type = toDelete.GetPerformanceTypeWithTagComponent();
            ShowButtonOfType(type);
            Destroy(toDelete);
            Rebuild();
        }
        
        GameObject InstantiateComponent(PerformanceType type)
        {
            return _componentInstantiator.InstantiateComponent(_componentDictionary[type], componentHolder, ClickDeleteButton);
        }

        void HideButton(GameObject button)
        {
            button.SetActive(false);
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
        
        private void Rebuild()
        {
            rebuilder.RebuildUILayout(componentContentHolder);
        }
        
        public void ResetUI()
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

        public void PopulateScreen(ExerciseData data)
        {
            for (var i = 0; i < data.performance.Count; i++)
            {
                var type = data.performance[i].GetPerformanceType();
                InstantiateComponent(type);
                HideButton(_buttonDictionary[type]);
            }
        }

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
        }

        public void ReadInto(SimpleExerciseData data)
        {
            if (nameInput.text.IsNullOrWhitespace()) data.name = "Untitled Exercise";
            else data.name = nameInput.text;
            PopulateComponents(data);
            ReadComponents(data);
        }
        
        void ReadComponents(SimpleExerciseData data)
        {
            for (var i = 0; i < componentHolder.childCount; i++)
            {
                var readable = componentHolder.GetChild(i).GetComponent<IExerciseReadable>();
                if(readable != null) readable.ReadInto(data);
            }
        }
    }
}