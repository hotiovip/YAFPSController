using UnityEngine;
using UnityEditor;

namespace Hotiovip.YAFPSController.Attributes
{
    [CustomPropertyDrawer(typeof(TitleAttribute))]
    public class TitlePropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            TitleAttribute titleAttribute = (TitleAttribute)attribute;
            string originalLabelText = label.text;

            GUIStyle headerStyle = new GUIStyle(GUI.skin.label);
            headerStyle.fontStyle = FontStyle.Bold;
            headerStyle.fontSize = 12;

            Rect headerRect = new Rect(position.x, position.y + (EditorGUIUtility.standardVerticalSpacing * 3), position.width, EditorGUIUtility.singleLineHeight);
            EditorGUI.LabelField(headerRect, titleAttribute.title, headerStyle);

            // Draw a line underneath the header
            Handles.color = Color.grey;
            Handles.DrawLine(new Vector3(position.x, position.y + EditorGUIUtility.singleLineHeight + 6), new Vector3(position.x + position.width, position.y + EditorGUIUtility.singleLineHeight + 6));

            // Adjust the position for the property field
            position.y += EditorGUIUtility.singleLineHeight + 10;
            position.height -= EditorGUIUtility.singleLineHeight + 10;

            // Draw the property field
            label.text = originalLabelText;
            EditorGUI.PropertyField(position, property, label, true);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            // Increase the height to accommodate the header and line
            return base.GetPropertyHeight(property, label) + EditorGUIUtility.singleLineHeight + 10;
        }
    }
}
