using UnityEditor;
using UnityEngine;

namespace Hotiovip.YAFPSController.Attributes
{
    [CustomPropertyDrawer(typeof(InfoBoxAttribute))]
    public class InfoBoxPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            InfoBoxAttribute infoBoxAttribute = (InfoBoxAttribute)attribute;

            Rect helpBoxPositon = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight * 2);

            // Draw info box above the property field
            EditorGUI.HelpBox(helpBoxPositon, infoBoxAttribute.text, MessageType.Info);

            Rect propertyPosition = new Rect(position.x, position.y + (EditorGUIUtility.singleLineHeight * 2) + EditorGUIUtility.standardVerticalSpacing, position.width, EditorGUIUtility.singleLineHeight);

            // Draw the property field
            EditorGUI.PropertyField(propertyPosition, property, label, true);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            // Increase property height to accommodate the info box
            return base.GetPropertyHeight(property, label) + (EditorGUIUtility.singleLineHeight * 2) + EditorGUIUtility.standardVerticalSpacing;
        }
    }
}
