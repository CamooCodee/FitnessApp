using System.Collections.Generic;
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
                    if (graphic == null) throw new NoGraphicsOnColorSchemeElementException();
                    else _targetElement = graphic;
                }

                return _targetElement;
            }
        }

        [SerializeField] private List<ColorSchemeData> supportedColorSchemes = new List<ColorSchemeData>();

        [SerializeField, HideInInspector]
        private int selectedSchemeIndex;
        public ColorSchemeData SelectedScheme
        {
            get
            {
                if (supportedColorSchemes.Count <= 0) return null;
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

                if (alreadyAddedSchemes.Contains(scheme))
                {
                    supportedColorSchemes[i].colorScheme = null;
                }
                else alreadyAddedSchemes.Add(scheme);
            }
        }
        

        #endregion
        
        private bool _started = false;
        
        private void Start()
        {
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
            SetTargetElementColor(SelectedScheme.GetSelectedColor());
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
            Debug.LogWarning($"Didn't find supported color scheme for '{scheme.name}'.");
        }

        void SetTargetElementColor(Color color)
        {
            TargetElement.color = color;
            TargetElement.enabled = false;
            TargetElement.enabled = true;
        }
        
        public bool SupportsScheme(string schemeName)
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
                supportedColorSchemes.Add(new ColorSchemeData()
                {
                    colorScheme = scheme
                });
            }
        }
    }
}