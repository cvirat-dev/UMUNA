using UnityEditor;

namespace UUP.UI.Customisation
{
    [CustomEditor(typeof(CustomButton))]
    public class CustomButtonEditor : UnityEditor.Editor
    {
        #region SerialedProperties
        SerializedProperty ButtonHoverMessage;
        SerializedProperty OnClick;
        SerializedProperty OnClickGE;
        SerializedProperty OnButtonHoverEnterSGE;
        SerializedProperty ConfigurationState;

        SerializedProperty ButtonTheme;
        SerializedProperty ButtonType;

        bool configureButtonStyle = false;
        #endregion

        private void OnEnable()
        {
            ButtonHoverMessage = serializedObject.FindProperty("buttonHoverMessage");
            OnClick = serializedObject.FindProperty("OnClick");
            OnClickGE = serializedObject.FindProperty("OnClickGE");
            OnButtonHoverEnterSGE = serializedObject.FindProperty("OnButtonHoverEnterSGE");
            ConfigurationState = serializedObject.FindProperty("configurationState");
            
            ButtonTheme = serializedObject.FindProperty("ButtonTheme");
            ButtonType = serializedObject.FindProperty("ButtonType");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            CustomButton customButton = (CustomButton)target;

            EditorGUILayout.BeginVertical();
            // ------------ DEFAULT-FIELDS ------------
            EditorGUILayout.PropertyField(ButtonHoverMessage);
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(OnClick);
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(OnClickGE);
            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(OnButtonHoverEnterSGE);
            EditorGUILayout.Space();
            // ----------------------------------------

            configureButtonStyle = EditorGUILayout.BeginFoldoutHeaderGroup(configureButtonStyle, "Custom-Button-Configuration");
            if (configureButtonStyle)
            {
                EditorGUILayout.Space();
                EditorGUILayout.PropertyField(ConfigurationState);
                EditorGUILayout.Space();

                if(customButton.configurationState == CustomButton.ConfigurationEnum.Yes)
                {
                    EditorGUILayout.PropertyField(ButtonTheme);
                    EditorGUILayout.Space();
                    EditorGUILayout.PropertyField(ButtonType);
                }
            }
            
            EditorGUILayout.EndFoldoutHeaderGroup();
            EditorGUILayout.EndVertical();
            serializedObject.ApplyModifiedProperties();
        }
    }
}
