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
            var schemeColor = colorScheme.GetColor(selectedColorIndex);
            if(schemeColor == null) throw new Exception($"Can't work with received Scheme Color. It's null.");
            return colorScheme.GetColor(selectedColorIndex).color;
        }
    }
}