using UnityEditor;
using UnityEngine;

namespace ColorSchemes
{
    [CustomEditor(typeof(DefaultColorSchemeSetting))]
    public class DefaultColorSchemeSettingEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var myTarget = target as DefaultColorSchemeSetting;
            if(myTarget == null) return;
            
            GUILayout.BeginVertical();
            if (GUILayout.Button("Support All Color Schemes"))
            {
                var schemes = Resources.LoadAll<ColorScheme>(ColorScheme.COLOR_SCHEMES_PATH);
                
                for (var i = 0; i < schemes.Length; i++)
                {
                    if (!myTarget.SupportsScheme(schemes[i].name))
                        myTarget.AddSupportedScheme(schemes[i]);
                }
            }

            if (myTarget.GetSupportedSchemesCount() > 0) GUILayout.Space(20f);

            for (var i = 0; i < myTarget.GetSupportedSchemesCount(); i++)
            {
                var newScheme = myTarget.GetScheme(i);
                if (newScheme == null)
                {
                    continue;
                }
                var newSchemeName = newScheme.name;
                if (GUILayout.Button("Set " + GetSchemeNameWithoutSchemePostFix(newSchemeName) + " Scheme"))
                {
                    myTarget.SetScheme(newSchemeName, reInitBeforeSet: !Application.isPlaying);
                }
            }
            GUILayout.EndVertical();
        }

        private static string GetSchemeNameWithoutSchemePostFix(string name)
        {
            if (!name.EndsWith("Scheme")) return name;
            string cleaned = name.Remove(name.Length - 6);
            return cleaned;
        }
    }
}