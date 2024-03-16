using UnityEditor;
using UnityEngine;

namespace Hotiovip.YAFPSController.Attributes
{
    [CustomPropertyDrawer(typeof(RequiredAttribute))]
    public class RequiredPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            // Calculate position for the info box
            Rect infoBoxPosition = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);

            if (property.objectReferenceValue == null)
            {
                EditorGUI.HelpBox(infoBoxPosition, "Field must be assigned!", MessageType.Error);

                // Adjust the property position
                position.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                position.height = EditorGUIUtility.singleLineHeight;
            }

            EditorGUI.PropertyField(position, property, label, true);

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            // Calculate height for the property field based on whether the info box is displayed
            float height = EditorGUI.GetPropertyHeight(property, label, true);

            if (property.objectReferenceValue == null)
            {
                // Add extra height for the info box
                height += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
            }

            return height;
        }
    }
}
