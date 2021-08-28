using System;
using System.Collections.Generic;
using FitnessApp.UICore;
using FitnessAppAPI;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace FitnessApp.UIConcretes
{
    public class ExerciseElement : MonoBehaviour
    {
        [Serializable]
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
        
        [SerializeField] private LeanTweenAnimationSpec animationSpec;
        [SerializeField] private float dropdownHeightDifference = 50f;
        
        private RectTransform _rectTransform;
        private float _noDropdownHeight;
        private float _withDropdownHeight;
        private bool _dropdownIsActive = false;
        
        [Space(15f)]
        [Header("Information Display References"), Space(15f)]
        [SerializeField] private TextMeshProUGUI exerciseNameText;
        [Space(10f)]
        [SerializeField] private List<ComponentDisplay> componentDisplayList = new List<ComponentDisplay>();

        public int Id { get; set; }

        private List<UnityEvent<int>> _onDelete = new List<UnityEvent<int>>();
        private List<UnityEvent<int>> _onCopy = new List<UnityEvent<int>>();
        private List<UnityEvent<int>> _onEdit = new List<UnityEvent<int>>();
        
        private float AnimationLength
        {
            get
            {
                if (animationSpec == null) return 0.6f;
                else return animationSpec.GetAnimationLength();
            }
        }
        private LeanTweenType AnimationEaseType
        {
            get
            {
                if (animationSpec == null) return LeanTweenType.easeOutCubic;
                else return animationSpec.GetEaseType();
            }
        }

        private void OnValidate()
        {
            dropdownHeightDifference = Mathf.Clamp(dropdownHeightDifference, 0f, 1080f);
        }

        private void Awake()
        {
            InitializeRectTransform();
            InitializeHeights();
            ClearEmptyComponentDisplays();
            exerciseNameText.Require(this);
        }
        
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
        void InitializeHeights()
        {
            _noDropdownHeight = _rectTransform.sizeDelta.y;
            _withDropdownHeight = _noDropdownHeight + dropdownHeightDifference;
        }
        void InitializeRectTransform()
        {
            _rectTransform = GetComponent<RectTransform>();
            _rectTransform.Require(this);
        }

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

        public void ToggleDropdown()
        {
            if(_dropdownIsActive) HideDropdown();
            else ShowDropdown();
        }

        public void HideDropdown()
        {
            if(!_dropdownIsActive) return;
            TweenDropdown(_noDropdownHeight);
            _dropdownIsActive = false;
        }
        
        public void ShowDropdown()
        {
            if(_dropdownIsActive) return;
            TweenDropdown(_withDropdownHeight);
            _dropdownIsActive = true;
        }

        void TweenDropdown(float targetY)
        {
            LeanTween.cancel(gameObject);
            LeanTween.size(_rectTransform, new Vector2(_rectTransform.sizeDelta.x, targetY),
                    AnimationLength)
                .setEase(AnimationEaseType);
        }

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
    }
}