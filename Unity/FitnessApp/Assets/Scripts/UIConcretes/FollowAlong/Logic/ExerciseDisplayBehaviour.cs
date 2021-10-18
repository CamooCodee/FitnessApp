using System;
using FitnessApp.Setting;
using FitnessApp.UIConcretes.Elements;
using FitnessAppAPI;
using TMPro;
using UnityEngine;

namespace FitnessApp.UIConcretes.FollowAlong.Logic
{
    public class ExerciseDisplayBehaviour : MonoBehaviour, IFollowAlongListener, ISettingListener<WeightUnitArgs>
    {
        [SerializeField] private RectTransform timer;
        [SerializeField] private RectTransform timerRect;
        [SerializeField] private Transform exerciseCard;
        
        [SerializeField] private TextMeshProUGUI nameDisplay;
        [SerializeField] private ComponentDisplay[] componentDisplays;

        private string _weightUnit = "-";

        private void Awake()
        {
            nameDisplay.Require(this);
            exerciseCard.Require(this);

            for (var i = 0; i < componentDisplays.Length; i++)
            {
                if (!componentDisplays[i].treatAsTime && !componentDisplays[i].IsFullyInitialized)
                {
                    Debug.LogError("Fully initialize all component displays!");
                }
            }
        }

        private void OnEnable() => WeightUnitSetting.Instance.AddListenerForSettingsUpdate(this);
        private void OnDisable() => WeightUnitSetting.Instance.RemoveListenerForSettingsUpdate(this);

        public void OnNewElement(IWorkoutDataElement[] elements, int current)
        {
            var element = elements[current];
            if (element == null || !(element is OffsetExerciseData exerciseData))
            {
                exerciseCard.SetAsFirstSibling();
                return;
            }
            exerciseCard.SetAsLastSibling();
            timer.SetParent(timerRect);
            timer.sizeDelta = timerRect.sizeDelta;
            timer.position = timerRect.position;

            nameDisplay.text = exerciseData.name;

            EnableAll();
            
            for (int i = 0; i < componentDisplays.Length; i++)
            {
                bool componentIsUsed = false;
                
                for (var j = 0; j < exerciseData.performance.Count; j++)
                {
                    var p = exerciseData.performance[j];
                    var pType = p.GetPerformanceType();
                    
                    if (componentDisplays[i].componentType != pType) continue;

                    var postFix = "";
                    if (pType == PerformanceType.Reps) postFix = "Reps";
                    else if (pType == PerformanceType.Weight) postFix = _weightUnit;

                    componentIsUsed = true;
                    if(pType != PerformanceType.Time) componentDisplays[i].textDisplay.text =
                        $"<b>{exerciseData.performanceValues[j]}</b> {postFix}";
                }
                
                if(!componentIsUsed) componentDisplays[i].displayGameObject.SetActive(false);
            }
        }

        public void ResetListener()
        {
            
        }

        void EnableAll()
        {
            for (int i = 0; i < componentDisplays.Length; i++)
            {
                componentDisplays[i].displayGameObject.SetActive(true);
            }
        }

        public void Execute(WeightUnitArgs args)
        {
            _weightUnit = args.unit;
        }
    }
}