using System.Collections;
using System.Collections.Generic;
using ColorSchemes;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ColorSchemeElement))]
public class ColorSchemeElementEditor : Editor
{
    public override void OnInspectorGUI()
    {
        //base.OnInspectorGUI();
        serializedObject.Update();
        EditorList.Show(serializedObject.FindProperty("supportedColorSchemes"));
        serializedObject.ApplyModifiedProperties();
    }
}

public static class EditorList
{
    public static void Show(SerializedProperty list)
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField(list.name);
        EditorGUILayout.EndHorizontal();
        if(!list.isArray) return;
        for (int i = 0; i < list.arraySize; i++)
        {
            EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i));
        }
    }
}
