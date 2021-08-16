using System;
using System.Collections.Generic;
using UnityEngine;

namespace ColorSchemes
{
    public class ColorSchemeElement : MonoBehaviour
    {
        private Graphics _targetElement;
        private Graphics TargetElement
        {
            get
            {
                if (_targetElement == null)
                {
                    var graphics = GetComponent<Graphics>();
                    if (graphics == null) throw new NoGraphicsOnColorSchemeElementException();
                    else _targetElement = graphics;
                }
                return _targetElement;
            }
        }

        [SerializeField] private List<ColorSchemeData> supportedColorSchemes;

        public ColorSchemeData GetData(int index)
        {
            if (index < -1 || index >= supportedColorSchemes.Count)
            {
                return null;
            }
            return supportedColorSchemes[index];
        }
    }
}