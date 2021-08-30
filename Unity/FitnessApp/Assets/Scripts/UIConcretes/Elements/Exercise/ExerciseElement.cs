using System;
using System.Collections.Generic;
using FitnessApp.UICore;
using FitnessAppAPI;
using TMPro;
using UIConcretes.Elements;
using UnityEngine;
using UnityEngine.Events;

namespace FitnessApp.UIConcretes.Elements.Exercise
{
    public class ExerciseElement : DropdownElementMono
    {
        [Serializable] // The icon-value pair in the visual exercise element.
        private class ComponentDisplay
        {
            public PerformanceType componentType;
            public GameObject displayGameObject;
            public TextMeshProUGUI textDisplay;

            public bool IsFullyInitialized
            {
                get
                {
                    bool isInit = componentType != PerformanceType.None;
                    isInit = isInit && displayGameObject != null;
                    isInit = isInit && textDisplay != null;
                    return isInit;
                }
            }
        }
        
        [Space(15f)]
        [Header("Information Display References"), Space(15f)]
        [SerializeField] private TextMeshProUGUI exerciseNameText;
        [Space(10f)]
        [SerializeField] private List<ComponentDisplay> componentDisplayList = new List<ComponentDisplay>();

        public int Id { get; set; }

        private List<UnityEvent<int>> _onDelete = new List<UnityEvent<int>>();
        private List<UnityEvent<int>> _onCopy = new List<UnityEvent<int>>();
        private List<UnityEvent<int>> _onEdit = new List<UnityEvent<int>>();

        public void SetData(ExerciseData data)
        {
            Id = data.id;
            exerciseNameText.text = data.name;
            for (var i = 0; i < componentDisplayList.Count; i++)
            {
                bool componentDisplayIsUsed = false;
                for (int j = 0; j < data.performance.Count; j++)
                {
                    if (componentDisplayList[i].componentType == data.performance[j].GetPerformanceType())
                    {
                        componentDisplayList[i].textDisplay.text = data.performanceValues[j];
                        componentDisplayIsUsed = true;
                    }
                }
                if(!componentDisplayIsUsed) componentDisplayList[i].displayGameObject.SetActive(false);
            }
        }

        #region Initialization

        private void Awake()
        {
            InitializeDropdown();
            
            ClearEmptyComponentDisplays();
            exerciseNameText.Require(this);
        }

        /// <summary>
        /// [Initialization Method]
        /// Removes entries which aren't fully set in the inspector.
        /// </summary>
        void ClearEmptyComponentDisplays()
        {
            for (var i = 0; i < componentDisplayList.Count; i++)
            {
                if (!componentDisplayList[i].IsFullyInitialized)
                {
                    componentDisplayList.RemoveAt(i);
                    i--;
                }
            }
        }

        #endregion

        #region Dropdown Functionallity
        
        public void ListenForDelete(UnityEvent<int> func) => _onDelete.Add(func);
        public void ListenForEdit(UnityEvent<int> func) => _onEdit.Add(func);
        public void ListenForCopy(UnityEvent<int> func) => _onCopy.Add(func);

        public void Delete() => InvokeEvent(_onDelete);
        public void Edit() => InvokeEvent(_onEdit);
        public void Copy() => InvokeEvent(_onCopy);

        void InvokeEvent(List<UnityEvent<int>> onEvent)
        {
            for (var i = 0; i < onEvent.Count; i++)
            {
                for(int j = 0 ; j < onEvent[i].GetPersistentEventCount();j++ )
                {    
                    ((MonoBehaviour)onEvent[j].GetPersistentTarget(j))
                        .SendMessage(onEvent[j].GetPersistentMethodName(j),Id);
                }
            }
        }
        
        #endregion
    }
}