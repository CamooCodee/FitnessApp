using System;
using System.Collections.Generic;
using FitnessApp;
using UnityEngine;

namespace ColorSchemes
{
    [AddComponentMenu("Color Schemes/Setting - Color Schemes", 0)]
    [DefaultExecutionOrder(-5)]
    [RequireComponent(typeof(AutoColorSchemeSettingInitializer))]
    public class DefaultColorSchemeSetting : MonoBehaviour, ISetting<IColorSchemeEventArgs>
    {
        public List<ColorScheme> supportedSchemes = new List<ColorScheme>();
        
        private List<ISettingListener<IColorSchemeEventArgs>> _settingUpdateListeners;
        public List<ISettingListener<IColorSchemeEventArgs>> SettingUpdateListeners
        {
            get
            {
                if (_settingUpdateListeners == null)
                {
                    _settingUpdateListeners = new List<ISettingListener<IColorSchemeEventArgs>>();
                    if(_isPlayModeInitialized) Debug.LogWarning("The Listener List was Null!");
                }
                return _settingUpdateListeners;
            }
        }

        [SerializeField, HideInInspector]
        private string currentScheme = "";

        private bool _isPlayModeInitialized;
        
        private void Start()
        {
            InitSelf();
            _isPlayModeInitialized = true;
        }

        public void AddListenerForSettingsUpdate(ISettingListener<IColorSchemeEventArgs> listener)
        {
            if (listener != null && !SettingUpdateListeners.Contains(listener))
            {
                SettingUpdateListeners.Add(listener);
                InvokeListener(listener, new DefaultColorSchemeArgs(GetSchemeByName(currentScheme)));
            }
        }

        public void RemoveListenerForSettingsUpdate(ISettingListener<IColorSchemeEventArgs> listener)
        {
            if (listener != null && SettingUpdateListeners.Contains(listener))
            {
                SettingUpdateListeners.Remove(listener);
            }
        }

        void RemoveNullListeners()
        {
            for (var i = 0; i < SettingUpdateListeners.Count; i++)
            {
                if(SettingUpdateListeners[i].Equals(null))
                {
                    SettingUpdateListeners.RemoveAt(i);
                    i--;
                }
            }
        }

        ColorScheme GetSchemeByName(string schemeName)
        {
            for (var i = 0; i < supportedSchemes.Count; i++)
            {
                if (supportedSchemes[i].name == schemeName)
                {
                    return supportedSchemes[i];
                }
            }
            
            throw new Exception($"Can't find scheme: '{schemeName}'");
        }
        
        public void RefreshInEditor()
        {
            if(supportedSchemes.Count == 0) return;
            SetScheme(currentScheme);
        }

        public void SetScheme(string schemeName, bool reInitBeforeSet = false)
        {
            if(reInitBeforeSet) ReInitAll();
            
            for (var i = 0; i < supportedSchemes.Count; i++)
            {
                if(supportedSchemes[i] == null) return;
                if (supportedSchemes[i].name == schemeName)
                {
                    currentScheme = schemeName;
                    InvokeAllListeners(new DefaultColorSchemeArgs(supportedSchemes[i]));
                    return;
                }
            }
            Debug.LogWarning($"Couldn't find supported scheme: '{schemeName}'");
        }

        void InvokeAllListeners(IColorSchemeEventArgs args)
        {
            RemoveNullListeners();
            for (var i = 0; i < SettingUpdateListeners.Count; i++)
            {
                if (SettingUpdateListeners[i] != null) 
                    InvokeListener(SettingUpdateListeners[i], args);
            }
        }

        void InvokeListener(ISettingListener<IColorSchemeEventArgs> listener, IColorSchemeEventArgs args)
        {
            listener.Execute(args);
        }

        public ColorScheme GetScheme(int index)
        {
            if(index < 0 || index > supportedSchemes.Count) 
                throw new ArgumentException("The given index isn't valid for receiving scheme data.");

            return supportedSchemes[index];
        }

        public int GetSupportedSchemesCount()
        {
            return supportedSchemes.Count;
        }

        public bool SupportsScheme(string schemeName)
        {
            for (var i = 0; i < supportedSchemes.Count; i++)
            {
                if(supportedSchemes[i] == null) continue;
                if (supportedSchemes[i].name == schemeName)
                {
                    return true;
                }
            }

            return false;
        }

        public void AddSupportedScheme(ColorScheme scheme)
        {
            int nullIndex = -1;
            
            for (var i = 0; i < supportedSchemes.Count; i++)
            {
                if (supportedSchemes[i] == null && nullIndex == -1)
                {
                    nullIndex = i;
                    continue;
                }
                if (supportedSchemes[i].name == scheme.name)
                {
                    return;
                }
            }

            if (nullIndex > -1) supportedSchemes[nullIndex] = scheme;
            else supportedSchemes.Add(scheme);
        }
        
        [ContextMenu("Reinitialize All (Potential) System Components.")]
        public void ReInitAll()
        {
            bool result = ColorSchemeMonoBehaviour.TryToActAsSettingsObject(this, true);
            if (!result) Debug.LogWarning("Multiple Setting - Color Schemes detected! Definitely NOT recommended!", gameObject);

            var listeners = FindObjectsOfType<ColorSchemeMonoBehaviour>();
            
            SettingUpdateListeners.Clear();

            for (var i = 0; i < listeners.Length; i++)
            {
                if(!listeners[i].CanBeAutoAssigned()) continue;
                AddListenerForSettingsUpdate(listeners[i]);
            }
        }

        public void InitSelf()
        {
            bool result = ColorSchemeMonoBehaviour.TryToActAsSettingsObject(this);
            if (!result) Debug.LogWarning("Multiple Setting - Color Schemes detected!", gameObject);
        }

        [ContextMenu("Remove Component")]
        public void Remove()
        {
            var helper = GetComponent<AutoColorSchemeSettingInitializer>();
            if(helper != null) helper.DestroyAll();
        }
    }
}