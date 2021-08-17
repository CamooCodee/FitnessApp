using System;
using FitnessApp;
using UnityEngine;

namespace ColorSchemes
{
    [Serializable]
    public class ColorSchemeData
    {
        public ColorScheme colorScheme;
        [SerializeField]
        private int selectedColorIndex = 0;

        public int GetSelectedColorIndex()
        {
            selectedColorIndex = Mathf.Clamp(selectedColorIndex, 0, colorScheme.ColorCount - 1);
            return selectedColorIndex;
        }

        public void SelectColor(int index)
        {
            if (index < 0 || index >= colorScheme.ColorCount)
                throw new Exception("Can't set color with an invalid index!");

            selectedColorIndex = index;
        }
        
        public Color GetSelectedColor()
        {
            if (colorScheme == null) throw new Exception("Can't find selected color if there is no color scheme set!");
            selectedColorIndex = Mathf.Clamp(selectedColorIndex, 0, colorScheme.ColorCount - 1);
            
            var schemeColor = colorScheme.GetColor(selectedColorIndex);
            if(schemeColor == null) throw new Exception($"Can't work with received Scheme Color. It's null.");
            return colorScheme.GetColor(selectedColorIndex).color;
        }
    }
}