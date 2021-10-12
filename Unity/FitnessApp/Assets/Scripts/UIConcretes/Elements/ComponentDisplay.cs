using System;
using FitnessAppAPI;
using TMPro;
using UnityEngine;

namespace FitnessApp.UIConcretes.Elements
{
    [Serializable]
    public class ComponentDisplay
    {
        public bool treatAsTime;
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
}