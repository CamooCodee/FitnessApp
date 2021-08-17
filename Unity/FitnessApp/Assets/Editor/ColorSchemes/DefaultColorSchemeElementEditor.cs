using UnityEditor;
using UnityEngine;

namespace ColorSchemes
{
    [CustomEditor(typeof(DefaultColorSchemeElement))]
    public class DefaultColorSchemeElementEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            
            var myTarget = target as DefaultColorSchemeElement;
            if(myTarget == null) return;
            
            if (GUILayout.Button("Support All Color Schemes"))
            {
                var schemes = Resources.LoadAll<ColorScheme>(ColorScheme.COLOR_SCHEMES_PATH);
                
                for (var i = 0; i < schemes.Length; i++)
                {
                    myTarget.AddSupportedScheme(schemes[i]);
                }
            }

            if (GUILayout.Button("Initialize With Current Color Scheme"))
            {
                myTarget.InitializeWithCurrentColorScheme();
            }
        }
    }
}