using UnityEditor;
using UnityEngine;

namespace Hotiovip.YAFPSController.Attributes
{
    [CustomPropertyDrawer(typeof(DisplayNameAttribute))]
    public class DisplayNamePropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            DisplayNameAttribute displayNameAttribute = attribute as DisplayNameAttribute;

            label.text = displayNameAttribute.displayName;

            EditorGUI.PropertyField(position, property, label);
        }
    }
}
