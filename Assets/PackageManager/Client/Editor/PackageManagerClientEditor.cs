using PackageManager.Client.Scripts;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace PackageManager.Client.Editor
{
    [CanEditMultipleObjects, CustomEditor(typeof(PackageManagerClient))]
    public class PackageManagerClientEditor : UnityEditor.Editor
    {
        private const string Apply = "Apply";
        private const string Packages = "Packages";
        private const string Script = "Script:";

        private PackageManagerClient _packageManagerClient;
        private ReorderableList _reorderableList;

        private SerializedProperty 
            _element,
            _arraySize,
            _updateWhenInitialize,
            _showPackageChangesProperty,
            _urlProperty,
            _folderPathProperty,
            _packagesSerializedPropertyProperty;

        private void OnEnable()
        {
            _packageManagerClient = (PackageManagerClient) target;

            _updateWhenInitialize = serializedObject.FindProperty("updateWhenInitialize");
            _showPackageChangesProperty = serializedObject.FindProperty("showPackageChanges");
            _urlProperty = serializedObject.FindProperty("url");
            _folderPathProperty = serializedObject.FindProperty("folderPath");

            _packagesSerializedPropertyProperty = serializedObject.FindProperty("packages");
            _arraySize = _packagesSerializedPropertyProperty.FindPropertyRelative("Array.size");

            _reorderableList = new ReorderableList(serializedObject, _packagesSerializedPropertyProperty,
                false, true, true, true)
            {
                drawElementCallback = (rect, index, active, focused) =>
                {
                    rect.height = EditorGUIUtility.singleLineHeight;
                    _element = _packagesSerializedPropertyProperty.GetArrayElementAtIndex(index);
                    EditorGUI.PropertyField(rect, _element);
                },
                drawHeaderCallback = rect => { EditorGUI.LabelField(rect, Packages); },
            };
        }

        public override void OnInspectorGUI()
        {
            EditorGUI.BeginDisabledGroup(true);
            {
                EditorGUILayout.ObjectField(
                    Script,
                    MonoScript.FromScriptableObject((ScriptableObject) target), typeof(PackageManagerClient),
                    false);
            }
            EditorGUI.EndDisabledGroup();

            serializedObject.Update();

            EditorGUILayout.PropertyField(_updateWhenInitialize);
            EditorGUILayout.PropertyField(_showPackageChangesProperty);

            GUILayout.Space(10);
            EditorGUILayout.PropertyField(_urlProperty);
            EditorGUILayout.PropertyField(_folderPathProperty);

            GUILayout.Space(10);
            EditorGUILayout.PropertyField(_arraySize);
            _reorderableList.DoLayoutList();

            serializedObject.ApplyModifiedProperties();

            var applyButtonStyle = new GUIStyle(GUI.skin.button)
            {
                fixedWidth = 100,
            };

            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            {
                if (GUILayout.Button(Apply, applyButtonStyle))
                    _packageManagerClient.Apply();
            }
            GUILayout.EndHorizontal();
        }
    }
}