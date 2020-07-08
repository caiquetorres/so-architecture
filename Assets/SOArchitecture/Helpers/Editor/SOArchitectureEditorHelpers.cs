using UnityEditor;
using UnityEngine;

namespace SOArchitecture
{
    public static class SOArchitectureEditorHelpers
    {
        public static void DrawDescription(SerializedProperty descriptionProperty, ref bool showing)
        {
            showing = EditorGUILayout.Foldout(showing, "Description");
            if (showing)
                EditorGUILayout.PropertyField(descriptionProperty, GUIContent.none);
        }
    }
}
