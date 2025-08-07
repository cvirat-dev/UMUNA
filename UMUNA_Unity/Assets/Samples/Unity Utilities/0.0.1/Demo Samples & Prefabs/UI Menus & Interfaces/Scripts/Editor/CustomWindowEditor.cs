using UnityEngine;
using UnityEditor;

namespace UUP.UI.Customisation.Editor
{
    [CustomEditor(typeof(CustomWindow))]
    public class CustomWindowEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            CustomWindow customWindow = (CustomWindow)target;

            if (GUILayout.Button("Configure()"))
            {
                customWindow.Configure();
            }
        }
    }
}