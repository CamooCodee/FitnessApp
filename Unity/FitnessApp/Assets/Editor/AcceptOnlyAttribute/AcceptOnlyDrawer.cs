using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace CustomAttributes
{
    [CustomPropertyDrawer(typeof(AcceptOnlyAttribute))]
    public class AcceptOnlyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property,
            GUIContent label)
        {
            EditorGUI.PropertyField(position, property, label);
            if(property.objectReferenceValue == null)
                return;
            
            if (property.objectReferenceValue != null && !(property.objectReferenceValue is MonoBehaviour))
            {
                Debug.LogError($"Properties using the Accept Only Attribute must be of type MonoBehaviour!");
                return;
            } 
            
            var myAttribute = (AcceptOnlyAttribute) attribute;
            var acceptedType = myAttribute.GetAcceptedType();
            
            if (!acceptedType.IsAssignableFrom(property.objectReferenceValue.GetType()))
                property.objectReferenceValue = null;
        }
    }
}