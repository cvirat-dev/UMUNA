using UnityEngine;
using UnityEditor;

namespace UUP.UI.Customisation.Editor
{
    [CustomEditor(typeof(CustomView))]
    public class CustomViewEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            CustomView customView = (CustomView)target;

            if (GUILayout.Button("Configure()"))
            {
                customView.Configure();
            }
        }
    }
}