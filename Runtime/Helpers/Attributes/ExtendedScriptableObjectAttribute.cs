using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SOArchitecture
{
    public class ExtendedScriptableObject : PropertyAttribute
    {
    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(ExtendedScriptableObject), true)]
    public class ExtendedScriptableObjectDrawer : PropertyDrawer
    {
        private const string ScriptName = "m_Script";
        private static readonly List<string> IgnoreClassFullNames = new List<string> {"TMPro.TMP_FontAsset"};
        
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var objectReferenceValue = property.objectReferenceValue;
            var totalHeight = EditorGUIUtility.singleLineHeight;

            if (objectReferenceValue == null || !property.isExpanded)
                return totalHeight;

            var scriptableObject = objectReferenceValue as ScriptableObject;
            var serializedObject = new SerializedObject(scriptableObject);
            var currentProperty = serializedObject.GetIterator();

            if (currentProperty.NextVisible(true))
            {
                do
                {
                    var subProperty = serializedObject.FindProperty(currentProperty.name);
                    var height = EditorGUI.GetPropertyHeight(subProperty, null, true) +
                                 EditorGUIUtility.standardVerticalSpacing;
                    
                    totalHeight += height;
                } while (currentProperty.NextVisible(false));
            }
            
            totalHeight += EditorGUIUtility.standardVerticalSpacing;
            return totalHeight;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var fieldType = GetFieldType();
            var propertySerializedObject = property.serializedObject;
            var objectReferenceValue = property.objectReferenceValue;
            
            EditorGUI.BeginProperty(position, label, property);
            
            if (fieldType == null || IgnoreClassFullNames.Contains(fieldType.FullName))
            {
                EditorGUI.PropertyField(position, property, label);
                EditorGUI.EndProperty();
                return;
            }
        
            ScriptableObject scriptableObjectProperty = null;
            if (!property.hasMultipleDifferentValues 
                && propertySerializedObject.targetObject != null 
                && propertySerializedObject.targetObject is ScriptableObject targetObject)
            {
                scriptableObjectProperty = targetObject;
            }
        
            var propertyRect = Rect.zero;
            var GUIcontent = new GUIContent(property.displayName);
            var foldoutRect = new Rect(position.x, position.y, EditorGUIUtility.labelWidth,
                EditorGUIUtility.singleLineHeight);
        
            if (objectReferenceValue != null)
                property.isExpanded =
                    EditorGUI.Foldout(foldoutRect, property.isExpanded, GUIcontent, true);
            else
            {
                foldoutRect.x += 12;
                EditorGUI.Foldout(foldoutRect, property.isExpanded, GUIcontent, true, EditorStyles.label);
            }
            
            var indentedPosition = EditorGUI.IndentedRect(position);
            var indentOffset = indentedPosition.x - position.x;
            
            propertyRect = new Rect(position.x + (EditorGUIUtility.labelWidth - indentOffset), position.y,
                position.width - (EditorGUIUtility.labelWidth - indentOffset), EditorGUIUtility.singleLineHeight);
        
            property.objectReferenceValue = EditorGUI.ObjectField(propertyRect, GUIContent.none,
                objectReferenceValue, fieldType, false);
            
            if (GUI.changed) 
                propertySerializedObject.ApplyModifiedProperties();
            
            if (property.propertyType == SerializedPropertyType.ObjectReference && objectReferenceValue != null)
            {
                var data = (ScriptableObject) objectReferenceValue;
                if (property.isExpanded)
                {
                    EditorGUI.indentLevel++;
                    var serializedObject = new SerializedObject(data);
        
                    GUI.Box(
                        new Rect(0,
                            position.y + EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing -
                            1, Screen.width,
                            position.height - EditorGUIUtility.singleLineHeight -
                            EditorGUIUtility.standardVerticalSpacing), "");
                    
                    var currentProperty = serializedObject.GetIterator();
                    var y = position.y + EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;
                    if (currentProperty.NextVisible(true))
                    {
                        do
                        {
                            var isScript = currentProperty.name == ScriptName;
                            
                            if (isScript)
                                EditorGUI.BeginDisabledGroup(true);
                            
                            var height = EditorGUI.GetPropertyHeight(
                                currentProperty, new GUIContent(currentProperty.displayName), true
                            );

                            EditorGUI.PropertyField(new Rect(position.x, y, position.width, height),
                                currentProperty,
                                true
                            );

                            y += height + EditorGUIUtility.standardVerticalSpacing;
                            
                            if (isScript)
                                EditorGUI.EndDisabledGroup();
                            
                        } while (currentProperty.NextVisible(false));
                    }
        
                    if (GUI.changed)
                        serializedObject.ApplyModifiedProperties();
        
                    EditorGUI.indentLevel--;
                }
            }
        
            property.serializedObject.ApplyModifiedProperties();
            EditorGUI.EndProperty();
        }

        private Type GetFieldType()
        {
            var type = fieldInfo.FieldType;
            if (type.IsArray)
            {
                type = type.GetElementType();
            }
            else if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>))
            {
                type = type.GetGenericArguments()[0];
            }
            
            return type;
        }
    }
#endif
}
