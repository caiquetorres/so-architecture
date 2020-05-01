using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace SOArchitecture
{
    public abstract class CollectionEditorBase : Editor
    {
        protected abstract string Name { get; }

        private SerializedProperty _size;
        
        private SerializedProperty _items;
        private SerializedProperty _gameEvent;
        
        private SerializedProperty _element;
        
        private ReorderableList _reorderableList;

        private void OnEnable()
        {
            _gameEvent = serializedObject.FindProperty("onChangedCollection");
            _items = serializedObject.FindProperty("items");
            _size = _items.FindPropertyRelative("Array.size");

            _reorderableList = new ReorderableList(
                serializedObject, _items, true, true, true, true)
            {
                drawElementCallback = (rect, index, isActive, isFocused) =>
                {
                    rect.height = EditorGUIUtility.singleLineHeight;
                    _element = _reorderableList.serializedProperty.GetArrayElementAtIndex(index);
                    EditorGUI.PropertyField(rect, _element);
                },
                drawHeaderCallback = rect => { EditorGUI.LabelField(rect, Name); }
            };
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.HelpBox("If you don't want to raise an event just keep the field as None", MessageType.Info);
            EditorGUILayout.PropertyField(_gameEvent);
            GUILayout.Space(10);
            EditorGUILayout.PropertyField(_size);

            _reorderableList.DoLayoutList();
            serializedObject.ApplyModifiedProperties();
        }
    }
}
