using UnityEditor;
using UnityEngine;

namespace SOArchitecture
{
    public abstract class VariableEditorBase : Editor
    {
        private SerializedProperty _valueProperty;
        private SerializedProperty _readOnlyProperty;
        private SerializedProperty _gameEventProperty;
        
        private void OnEnable()
        {
            _valueProperty = serializedObject.FindProperty("value");
            _readOnlyProperty = serializedObject.FindProperty("readOnly");
            _gameEventProperty = serializedObject.FindProperty("gameEvent");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUI.BeginDisabledGroup(true);
            {
                EditorGUILayout.ObjectField(
                    "Script:", 
                    MonoScript.FromScriptableObject((ScriptableObject) target), serializedObject.GetType(), 
                    false);
            }
            EditorGUI.EndDisabledGroup();

            EditorGUILayout.PropertyField(_readOnlyProperty);
            if (!_readOnlyProperty.boolValue)
            {
                EditorGUILayout.PropertyField(_gameEventProperty);
            }
            EditorGUILayout.PropertyField(_valueProperty);
            serializedObject.ApplyModifiedProperties();
        }
    }
}
