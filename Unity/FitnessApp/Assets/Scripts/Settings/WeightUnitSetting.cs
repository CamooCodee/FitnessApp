using System.Collections.Generic;
using UnityEngine;

namespace FitnessApp.Setting
{
    [DefaultExecutionOrder(-5)]
    public class WeightUnitSetting : MonoBehaviour, ISetting<WeightUnitArgs>
    {
        public static WeightUnitSetting Instance
        {
            get
            {
                if(instance == null)
                    Debug.LogError("Trying to access weight unit setting singleton when it's null!");

                return instance;
            }
        }
        private static WeightUnitSetting instance;
        
        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Destroy(this);
        }

        private readonly List<ISettingListener<WeightUnitArgs>> _listeners = new List<ISettingListener<WeightUnitArgs>>();

        // TODO: Load from file
        private string _currentUnit = "kg"; 
        
        public void UpdateSetting(string unit)
        {
            _currentUnit = unit;
            var eventData = new WeightUnitArgs(unit);
            
            foreach (var listener in _listeners)
                listener.Execute(eventData);
        }

        void UpdateListener(ISettingListener<WeightUnitArgs> listener)
        {
            listener?.Execute(new WeightUnitArgs(_currentUnit));
        }
        
        public void AddListenerForSettingsUpdate(ISettingListener<WeightUnitArgs> listener)
        {
            if(listener == null || _listeners.Contains(listener)) return;
            
            _listeners.Add(listener);
            UpdateListener(listener);
        }

        public void RemoveListenerForSettingsUpdate(ISettingListener<WeightUnitArgs> listener)
        {
            if(listener != null && _listeners.Contains(listener))
                _listeners.Remove(listener);
        }

        public void RefreshInEditor()
        {
            
        }
    }
}