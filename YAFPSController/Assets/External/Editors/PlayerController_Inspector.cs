using UnityEditor;
using UnityEngine.UIElements;

namespace Hotiovip.YAFPSController.Editors
{
    [CustomEditor(typeof(PlayerController))]
    public class Player_Inspector : Editor
    {
        public override void OnInspectorGUI ()
        {
            //PlayerController playerController = (PlayerController)target;
            DrawDefaultInspector();

            //EditorGUILayout.HelpBox("Helpbox!", MessageType.Info);
        }

        /*
        public override VisualElement CreateInspectorGUI()
        {
            VisualElement myInspector = new VisualElement();

            myInspector.Add(new Label("This is a custom inspector"));

            return myInspector;
        }
        */
    }
}
