using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace SOArchitecture
{
    [CanEditMultipleObjects, CustomEditor(typeof(FolderStructure))]
    public class FolderStructureEditor : Editor
    {
        private const string BaseFolders = "BaseFolders";
        private const string EntityFolders = "EntityFolders";
        
        private FolderStructure _folderStructure;

        private ReorderableList _baseFolders;
        private ReorderableList _entityFolders;

        private SerializedProperty _element;
        private SerializedProperty _baseFolderLength;
        private SerializedProperty _entityFolderLength;
        private SerializedProperty _baseFoldersProperty;
        private SerializedProperty _entityFoldersProperty;

        #region Initialization
        
        private void OnEnable()
        {
            _folderStructure = (FolderStructure) target;
            
            _baseFoldersProperty = serializedObject.FindProperty("baseFolders");
            _entityFoldersProperty = serializedObject.FindProperty("entityFolders");

            _baseFolderLength = _baseFoldersProperty.FindPropertyRelative("Array.size");
            _entityFolderLength = _entityFoldersProperty.FindPropertyRelative("Array.size");

            _baseFolders = new ReorderableList(
                serializedObject,
                _baseFoldersProperty,
                true,
                true,
                true,
                true)
            {
                drawElementCallback = (rect, index, isActive, isFocused) =>
                {
                    rect.height = EditorGUIUtility.singleLineHeight;
                    _element = _baseFolders.serializedProperty.GetArrayElementAtIndex(index);
                    EditorGUI.PropertyField(rect, _element, new GUIContent(_element.stringValue));
                },
                drawHeaderCallback = rect => { EditorGUI.LabelField(rect, BaseFolders); }
            };
            _entityFolders = new ReorderableList(
                serializedObject,
                _entityFoldersProperty,
                true,
                true,
                true,
                true)
            {
                drawElementCallback = (rect, index, isActive, isFocused) =>
                {
                    rect.height = EditorGUIUtility.singleLineHeight;
                    _element = _entityFolders.serializedProperty.GetArrayElementAtIndex(index);
                    EditorGUI.PropertyField(rect, _element, new GUIContent(_element.stringValue));
                },
                drawHeaderCallback = rect => { EditorGUI.LabelField(rect, EntityFolders); }
            };
        }
        
        #endregion

        public override void OnInspectorGUI()
        {
            EditorGUI.BeginDisabledGroup(true);
            {
                EditorGUILayout.ObjectField(
                    "Script:", 
                    MonoScript.FromScriptableObject((ScriptableObject) target), serializedObject.GetType(), 
                    false);
            }
            EditorGUI.EndDisabledGroup();
            
            serializedObject.Update();

            EditorGUILayout.PropertyField(_baseFolderLength);
            _baseFolders.DoLayoutList();
            
            EditorGUILayout.PropertyField(_entityFolderLength);
            _entityFolders.DoLayoutList();

            serializedObject.ApplyModifiedProperties();
        }
    }
}
