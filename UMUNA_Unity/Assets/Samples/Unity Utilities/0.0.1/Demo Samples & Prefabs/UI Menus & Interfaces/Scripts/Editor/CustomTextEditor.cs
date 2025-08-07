using UnityEngine;
using UnityEditor;

namespace UUP.UI.Customisation.Editor
{
    [CustomEditor(typeof(CustomText))]
    public class CustomTextEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            CustomText customText = (CustomText)target;

            if (GUILayout.Button("Configure()"))
            {
                customText.Configure();
            }
        }
    }
}
