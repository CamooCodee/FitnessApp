using System.Collections.Generic;
using FitnessAppAPI;
using UnityEngine;
using UnityEngine.UI;

namespace ColorSchemes
{
    [AddComponentMenu("Color Schemes/Default Element - Color Schemes", 1)]
    [DefaultExecutionOrder(-3)]
    public class DefaultColorSchemeElement : ColorSchemeMonoBehaviour, IColorSchemeElement
    {
        private Graphic _targetElement;
        private Graphic TargetElement
        {
            get
            {
                if (_targetElement == null)
                {
                    var graphic = GetComponent<Graphic>();
                    if (graphic == null) Debug.LogError($"No Graphic on: '{gameObject.name}'", this);
                    else _targetElement = graphic;
                }

                return _targetElement;
            }
        }

        private bool HasGraphic => _targetElement != null || TargetElement != null;
        
        [SerializeField] private List<ColorSchemeData> supportedColorSchemes = new List<ColorSchemeData>();

        [SerializeField, HideInInspector]
        private int selectedSchemeIndex = - 1;

        private ColorSchemeData SelectedScheme
        {
            get
            {
                if (supportedColorSchemes.Count <= 0) return null;
                if (selectedSchemeIndex < 0) return null;
                return supportedColorSchemes[selectedSchemeIndex];
            }
            set
            {
                if (value == null || !supportedColorSchemes.Contains(value))
                {
                    Debug.LogWarning("Trying to set selected scheme with invalid values!");
                    return;
                }

                for (var i = 0; i < supportedColorSchemes.Count; i++)
                {
                    if (supportedColorSchemes[i] == value)
                    {
                        selectedSchemeIndex = i;
                    }
                }
            }
        }

        #region Editor

        private void OnValidate()
        {
            var alreadyAddedSchemes = new List<ColorScheme>();

            for (var i = 0; i < supportedColorSchemes.Count; i++)
            {
                var scheme = supportedColorSchemes[i].colorScheme;
                
                if(scheme == null) continue;
                
                if (alreadyAddedSchemes.Contains(scheme))
                {
                    supportedColorSchemes[i].colorScheme = null;
                    Debug.Log("This scheme already is supported!");
                }
                else alreadyAddedSchemes.Add(scheme);
            }
        }
        

        #endregion
        
        private bool _started;
        
        private void Start()
        {
            if (!HasGraphic)
            {
                Debug.Log("Destroyed unused element due to missing graphic.");
                Destroy(this);
                return;
            }
            _started = true;
            Setup();
        }

        private void OnEnable()
        {
            if(_started) Setup();
        }

        private void OnDisable()
        {
            if(_started) Teardown();
        }
        
        public ColorSchemeData GetData(int index)
        {
            if (index < 0 || index >= supportedColorSchemes.Count)
            {
                return null;
            }
            return supportedColorSchemes[index];
        }

        public void OnSelectedColorUpdated()
        {
            if(selectedSchemeIndex >= 0) SetTargetElementColor(SelectedScheme.GetSelectedColor());
        }

        public override void Execute(IColorSchemeEventArgs args)
        {
            var scheme = args.GetScheme();

            for (var i = 0; i < supportedColorSchemes.Count; i++)
            {
                if (scheme.name == supportedColorSchemes[i].colorScheme.name)
                {
                    SelectedScheme = supportedColorSchemes[i];
                    SetTargetElementColor(supportedColorSchemes[i].GetSelectedColor());
                    return;
                }
            }

            bool isSetup = supportedColorSchemes.Count > 0;
            if(isSetup) Debug.LogWarning($"Element doesn't support '{scheme.name}' scheme.");
        }

        void SetTargetElementColor(Color color)
        {
            TargetElement.color = color;
            TargetElement.enabled = false;
            TargetElement.enabled = true;
        }

        private bool SupportsScheme(string schemeName)
        {
            for (var i = 0; i < supportedColorSchemes.Count; i++)
            {
                if (supportedColorSchemes[i].colorScheme.name == schemeName)
                    return true;
            }

            return false;
        }

        public void AddSupportedScheme(ColorScheme scheme)
        {
            if (!SupportsScheme(scheme.name))
            {
                supportedColorSchemes.Add(new ColorSchemeData { colorScheme = scheme});
            }
        }

        public override bool CanBeAutoAssigned()
        {
            return base.CanBeAutoAssigned() && HasGraphic;
        }

        public void CleanUp()
        {
            for (var i = 0; i < supportedColorSchemes.Count; i++)
            {
                if (supportedColorSchemes[i] == null || supportedColorSchemes[i].colorScheme == null)
                {
                    supportedColorSchemes.RemoveAt(i);
                    i--;
                }
            }
        }
    }
}