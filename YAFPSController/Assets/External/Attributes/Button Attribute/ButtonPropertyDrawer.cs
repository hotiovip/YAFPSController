using UnityEditor;
using UnityEngine;

namespace Hotiovip.YAFPSController.Attributes
{
    [CustomPropertyDrawer(typeof(ButtonAttribute))]
    public class ButtonPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            ButtonAttribute buttonAttribute = attribute as ButtonAttribute;

            if (GUI.Button(position, buttonAttribute.buttonName))
            {
                // Get the target object
                object targetObject = property.serializedObject.targetObject;
                // Invoke the method using reflection
                targetObject.GetType().GetMethod(buttonAttribute.methodName).Invoke(targetObject, null);
            }
        }
    }
}
