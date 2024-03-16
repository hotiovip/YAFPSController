using UnityEditor;
using UnityEngine;

namespace Hotiovip.YAFPSController.Attributes
{
    [CustomPropertyDrawer(typeof(ShowIfAttribute))]
    public class ShowIfPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            ShowIfAttribute showIfAttribute = (ShowIfAttribute)attribute;

            // Find the boolean property based on the provided variable name
            SerializedProperty boolProperty = property.serializedObject.FindProperty(showIfAttribute.variableName);

            if (boolProperty != null && boolProperty.propertyType == SerializedPropertyType.Boolean)
            {
                // Show or hide the property based on the boolean value
                if (boolProperty.boolValue)
                {
                    EditorGUI.PropertyField(position, property, label, true);
                }
            }
            else
            {
                EditorGUI.LabelField(position, label, new GUIContent("Error: Invalid boolean variable"));
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            // Return the property's height if it's visible, otherwise return 0
            ShowIfAttribute showIfAttribute = (ShowIfAttribute)attribute;
            SerializedProperty boolProperty = property.serializedObject.FindProperty(showIfAttribute.variableName);

            if (boolProperty != null && boolProperty.propertyType == SerializedPropertyType.Boolean && boolProperty.boolValue)
            {
                return EditorGUI.GetPropertyHeight(property, label);
            }
            else
            {
                return 0f;
            }
        }
    }
}
