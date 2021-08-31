using System;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ColorSchemes
{
    [CustomPropertyDrawer(typeof(ColorSchemeData))]
    public class ColorSchemeDataDrawer : PropertyDrawer
    {
        private const string BAR_TEXTURE_PATH = "EditorGUI/Bar";
        private Texture _texture = null;
        
        public override void OnGUI(Rect labelRect, SerializedProperty property, GUIContent label)
        {
            if(_texture == null) _texture = Resources.Load<Texture2D>(BAR_TEXTURE_PATH);

            property.serializedObject.Update();
            
            bool editingMultiple = property.serializedObject.targetObjects.Length > 1;
            EditorGUI.BeginProperty(labelRect, label, property);
            var targetColorSchemeElement = property.serializedObject.targetObject as IColorSchemeElement;
            if (targetColorSchemeElement == null) 
                throw new InvalidColorSchemeDataUsageException();
            var colorSchemeData = targetColorSchemeElement.GetData(GetIndexByPropertyPath(property.propertyPath));
            var previous = GUI.contentColor;
            if (colorSchemeData != null && colorSchemeData.colorScheme != null)
            {
                if(!editingMultiple)
                {
                    GUI.contentColor = colorSchemeData.GetSelectedColor();
                    label = new GUIContent(_texture);
                }
                else label = new GUIContent("-");
            }
            labelRect = EditorGUI.PrefixLabel(labelRect, GUIUtility.GetControlID(FocusType.Passive), label);
            GUI.contentColor = previous;
            
            
            var schemeLabel = new GUIContent("Color Scheme");
            var schemeRect = new Rect(new Vector2(155f - 213f * 0.5f + 10f, labelRect.position.y + 20f),  new Vector2(240f, EditorGUIUtility.singleLineHeight));
            var schemeProp = property.FindPropertyRelative("colorScheme");
            EditorGUI.BeginProperty(schemeRect, schemeLabel, schemeProp);
            EditorGUI.PropertyField(schemeRect, schemeProp);
            EditorGUI.EndProperty();

            if (!editingMultiple && colorSchemeData != null && colorSchemeData.colorScheme != null)
            {
                var scheme = colorSchemeData.colorScheme;

                int selectedColorIndex;
                selectedColorIndex = colorSchemeData.GetSelectedColorIndex();
                var tags = GetAvailableColorTags(scheme);

                var colorPopupLabel = new GUIContent("Color");
                var colorPopupRect = new Rect(new Vector2(schemeRect.position.x, schemeRect.position.y + 20f),
                    schemeRect.size);
                colorPopupRect = EditorGUI.PrefixLabel(colorPopupRect, GUIUtility.GetControlID(FocusType.Passive),
                    colorPopupLabel);
                int previousIndex = selectedColorIndex;
                selectedColorIndex = EditorGUI.Popup(colorPopupRect, selectedColorIndex, tags);


                if (previousIndex != selectedColorIndex)
                { 
                    colorSchemeData.SelectColor(selectedColorIndex);
                    targetColorSchemeElement.OnSelectedColorUpdated();
                }
            }

            EditorGUI.EndProperty();
            property.serializedObject.ApplyModifiedProperties();
            PrefabUtility.RecordPrefabInstancePropertyModifications((UnityEngine.Object)targetColorSchemeElement);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight * 3.5f;
        }

        private static string[] GetAvailableColorTags(ColorScheme scheme)
        {
            if(scheme == null) return new string[0];
            
            var colors = new string[scheme.ColorCount];

            for (var i = 0; i < colors.Length; i++)
            {
                colors[i] = scheme.GetColor(i).tag;
            }

            return colors;
        }

        private static bool IsNumber(char c)
        {
            return c >= '0' && c <= '9';
        }
        
        private static int GetIndexByPropertyPath(string path)
        {
            string n = "0";
            bool foundN = false;
            
            for (var i = path.Length - 1; i >= 0; i--)
            {
                if (foundN && !IsNumber(path[i]))
                    return int.Parse(n);

                if (IsNumber(path[i]))
                {
                    if (foundN == false) n = "";
                    n = n.Insert(0, path[i].ToString());
                    foundN = true;
                }
            }

            return int.Parse(n);
        }
    }
}