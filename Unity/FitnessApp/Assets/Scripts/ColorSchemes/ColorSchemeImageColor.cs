using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace FitnessApp.ColorSchemes
{
    [RequireComponent(typeof(Image))]
    [AddComponentMenu("UI/Color Schemes/Image Color - ColorSchemes")]
    [ExecuteAlways]
    public class ColorSchemeImageColor : ColorSchemeMonoBehaviour
    {
        private Image _image;
        [SerializeField] private List<ImageColorColorScheme> imageColors;
        
        private void Awake()
        {
            _image = GetComponent<Image>();
            
            if (Application.isPlaying) // Not Editor Code
            {
                Init();
            }
            else if(imageColors != null) // Editor Code
            { 
                for (var i = 0; i < imageColors.Count; i++)
                {
                    _previousSchemeOrder.Add(imageColors[i].scheme);
                }
                _previousLength = imageColors.Count;
            }
        }

        #region OnValidate

        private void OnValidate()
        {
            if(_image == null) return;
            PreventDoubledColorSchemes();
        }

        private int _previousLength = 0;
        private List<ColorScheme> _previousSchemeOrder = new List<ColorScheme>();
        
        void PreventDoubledColorSchemes()
        {
            if(imageColors == null) return;

            if (_previousLength < imageColors.Count)
            {
                OnAdd();
            }
            else if(_previousLength == imageColors.Count)
            {
                for (int i = 0; i < imageColors.Count; i++)
                {
                    if (_previousSchemeOrder[i] != imageColors[i].scheme)
                    {
                        SwapScheme(i, _previousSchemeOrder[i], imageColors[i].scheme);
                    }
                }
            }
            
            _previousSchemeOrder.Clear();
            for (var i = 0; i < imageColors.Count; i++)
            {
                _previousSchemeOrder.Add(imageColors[i].scheme);
            }
            
            _previousLength = imageColors.Count;
        }

        void SwapScheme(int indexOfChanged, ColorScheme changedFrom, ColorScheme changedTo)
        {
            for (var i = 0; i < imageColors.Count; i++)
            {
                if(i == indexOfChanged) continue;
                if (changedTo == imageColors[i].scheme) imageColors[i].scheme = changedFrom;
            }
        }

        void OnAdd()
        {
            imageColors[imageColors.Count - 1].color = _image.color;
            
            PreventTooMany();
            PreventRepeating();
        }

        void PreventTooMany()
        {
            int colorSchemeCount = Enum.GetValues(typeof(ColorScheme)).Length;
            if (imageColors.Count > colorSchemeCount)
            {
                imageColors.RemoveRange(colorSchemeCount, imageColors.Count - colorSchemeCount);
            }
        }

        void PreventRepeating()
        {
            var alreadyAdded = new List<ColorScheme>();
            var schemes = Enum.GetValues(typeof(ColorScheme));

            
            for (var i = 0; i < imageColors.Count; i++)
            {
                if (!alreadyAdded.Contains(imageColors[i].scheme))
                {
                    alreadyAdded.Add(imageColors[i].scheme);
                    continue;
                }
                foreach (var schemeObj in schemes)
                {
                    var scheme = (ColorScheme) schemeObj;
                    if (!alreadyAdded.Contains(scheme))
                    {
                        imageColors[i].scheme = scheme;
                        alreadyAdded.Add(scheme); 
                    }
                }
            }
        }
        
        #endregion

        protected override void SetColorScheme(ColorScheme scheme)
        {
            for (var i = 0; i < imageColors.Count; i++)
            {
                if (imageColors[i].scheme == scheme)
                {
                    _image.color = imageColors[i].color;
                    if (imageColors[i].makeInvisible) _image.color = new Color(0, 0, 0, 0);
                    break;
                }
            }
        }
    }

    [Serializable]
    public class ImageColorColorScheme
    {
        public ColorScheme scheme;
        public Color color;
        public bool makeInvisible;
    }
}