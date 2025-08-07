using UnityEngine;
using UnityEditor;

namespace UUP.UI.Customisation.Editor
{
    [CustomEditor(typeof(CustomUiComponent))]
    public class CustomuiComponentEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            CustomUiComponent CustomUiComponent = (CustomUiComponent)target;

            if (GUILayout.Button("Init()"))
            {
                CustomUiComponent.Init();
            }
        }
    }
}
