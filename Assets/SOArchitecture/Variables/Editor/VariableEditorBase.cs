using UnityEditor;
using UnityEngine;

namespace SOArchitecture
{
    public abstract class VariableEditorBase : Editor
    {
        private const string ValueProperty = "value";
        private const string IsReadOnlyProperty = "isReadOnly";
        private const string GameEventProperty = "gameEvent";
        private const string DescriptionProperty = "description"; 
        
        private bool _isShowingDescription;
        
        private SerializedProperty _valueProperty;
        private SerializedProperty _readOnlyProperty;
        private SerializedProperty _gameEventProperty;
        private SerializedProperty _descriptionProperty;
        
        private void OnEnable()
        {
            _valueProperty = serializedObject.FindProperty(ValueProperty);
            _readOnlyProperty = serializedObject.FindProperty(IsReadOnlyProperty);
            _gameEventProperty = serializedObject.FindProperty(GameEventProperty);
            _descriptionProperty = serializedObject.FindProperty(DescriptionProperty);
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
                EditorGUILayout.PropertyField(_gameEventProperty);
            
            EditorGUILayout.PropertyField(_valueProperty);

            GUILayout.Space(10);
            SOArchitectureEditorHelpers.DrawDescription(_descriptionProperty, ref _isShowingDescription);
            
            serializedObject.ApplyModifiedProperties();
        }
    }
}
