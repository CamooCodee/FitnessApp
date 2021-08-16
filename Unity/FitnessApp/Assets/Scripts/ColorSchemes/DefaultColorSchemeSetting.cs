using System.Collections.Generic;
using UnityEngine;

namespace ColorSchemes
{
    public class ColorSchemeSetting : MonoBehaviour
    {
        private const string COLOR_SCHEMES_PATH = "ColorSchemes";
        
        public List<ColorScheme> supportedSchemes = new List<ColorScheme>();

        private void OnGUI()
        {
            if (GUILayout.Button("Find all schemes"))
            {
                var schemes = Resources.LoadAll<ColorScheme>(COLOR_SCHEMES_PATH);
                for (var i = 0; i < schemes.Length; i++)
                    if (!supportedSchemes.Contains(schemes[i]))
                        supportedSchemes.Add(schemes[i]);
            }
        }
    }
}