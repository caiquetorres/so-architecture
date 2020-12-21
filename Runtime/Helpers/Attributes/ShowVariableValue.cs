using UnityEditor;
using UnityEngine;

namespace SOArchitecture.Runtime.Helpers.Attributes
{
    public class ShowVariableValue : PropertyAttribute
    {
        public readonly bool CanEdit;

        public ShowVariableValue() => CanEdit = false;

        public ShowVariableValue(bool canEdit) => CanEdit = canEdit;
    }
    
#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(ShowVariableValue))]
    public class ShowVariableDrawer : PropertyDrawer
    {
        private readonly float _padding = EditorGUIUtility.standardVerticalSpacing;
        private const string ValuePropertyName = "value";
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var lastWidth = position.width;
            var objectReferenceValue = property.objectReferenceValue; 
            var canEditVariable = attribute as ShowVariableValue;

            var rectVariable = new Rect
            {
                x = position.x,
                y = position.y,
                width = lastWidth * 0.8f,
                height = EditorGUIUtility.singleLineHeight,
            };
            
            var rectValue = new Rect
            {
                x = position.x + lastWidth * 0.8f + _padding,
                y = position.y,
                width = position.width * 0.2f - _padding,
                height = EditorGUIUtility.singleLineHeight,
            };
            
            EditorGUI.BeginProperty(position, label, property);
            {
                EditorGUI.PropertyField(rectVariable, property, label);

                if (objectReferenceValue != null)
                {
                    var serializedObject = new SerializedObject(objectReferenceValue);
                    EditorGUI.BeginDisabledGroup(canEditVariable != null && !canEditVariable.CanEdit);
                    {
                        EditorGUI.PropertyField(
                            rectValue, serializedObject.FindProperty(ValuePropertyName), GUIContent.none
                        );
                        
                        serializedObject.ApplyModifiedProperties();
                    }
                    EditorGUI.EndDisabledGroup();
                }
            }
            EditorGUI.EndProperty();
        }
    }
#endif
}
