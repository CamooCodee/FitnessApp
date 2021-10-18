using System;
using System.Collections.Generic;
using lukassacher.ColorSchemes;
using UnityEngine;
using UnityEngine.UI;
using static lukassacher.ColorSchemes.ColorSchemeBehaviour;

namespace FitnessApp.UICore
{
    public class ButtonColorSelectionGroup : SchemeBehaviour
    {
        [SerializeField] private Button[] buttons = new Button[0];
        private Button _selected;

        [SerializeField] private List<SupportedColorScheme> deselectedColors = new List<SupportedColorScheme>();
        [SerializeField] private List<SupportedColorScheme> selectedColors = new List<SupportedColorScheme>();
        private Color selectedColor;
        private Color deselectedColor;
        
        private void Awake()
        {
            foreach (var button in buttons)
            {
                if (button.targetGraphic == null)
                {
                    Debug.LogError("Buttons included in the color selection require a target graphic!");
                    continue;
                }
                
                button.onClick.AddListener(delegate { Select(button); });
            }
        }

        private void Start()
        {
            Select(null);
        }

        private void Select(Button button)
        {
            _selected = button;
            
            for (var i = 0; i < buttons.Length; i++) 
                buttons[i].targetGraphic.color = deselectedColor;
            
            if(button != null)
                button.targetGraphic.color = selectedColor;
        }

        public override void SetScheme(Scheme scheme)
        {
            var colorScheme = scheme as ColorScheme;
            if(colorScheme == null) return;
            
            foreach (var s in selectedColors)
            {
                if(s == null) return;
                if (s.scheme == scheme)
                {
                    selectedColor = s.scheme.colors[s.schemeColorIndex].color;
                }
            }
            
            foreach (var s in deselectedColors)
            {
                if(s == null) return;
                if (s.scheme == scheme)
                {
                    deselectedColor = s.scheme.colors[s.schemeColorIndex].color;
                }
            }
            
            Select(_selected); // Update the colors through this call
        }
    }
}