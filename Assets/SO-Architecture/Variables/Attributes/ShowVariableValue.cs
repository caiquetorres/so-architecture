using UnityEditor;
using UnityEngine;

namespace SOArchitecture
{
    public class ShowVariableValue : PropertyAttribute
    {
        public readonly bool CanEdit;

        public ShowVariableValue()
        {
            CanEdit = false;
        }
        
        public ShowVariableValue(bool canEdit)
        {
            CanEdit = canEdit;
        }
    }
    
#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(ShowVariableValue))]
    public class ShowVariableDrawer : PropertyDrawer
    {
        private float _lastWidth;
        private const float Padding = 4f;
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            _lastWidth = GUILayoutUtility.GetLastRect().width;
            
            EditorGUI.BeginProperty(position, label, property);
            var canEditVariable = attribute as ShowVariableValue;
            
            var rectVariable = new Rect()
            {
                x = position.x,
                y = position.y,
                width = _lastWidth * 0.8f,
                height = EditorGUIUtility.singleLineHeight,
            };
            
            var rectValue = new Rect()
            {
                x = (position.x + _lastWidth * 0.8f + Padding),
                y = position.y,
                width = (_lastWidth * 0.2f - Padding),
                height = EditorGUIUtility.singleLineHeight,
            };
            
            EditorGUI.PropertyField(rectVariable, property, label);

            if (property.objectReferenceValue != null)
            {
                var serializedObject = new SerializedObject(property.objectReferenceValue);
                EditorGUI.BeginDisabledGroup(!canEditVariable.CanEdit);
                
                    EditorGUI.PropertyField(rectValue, serializedObject.FindProperty("value"), GUIContent.none);
                    serializedObject.ApplyModifiedProperties();
                    
                EditorGUI.EndDisabledGroup();
            }
            
            EditorGUI.EndProperty();
        }
    }
#endif
}