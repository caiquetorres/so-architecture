using UnityEditor;
using UnityEngine;
using UnityEditorInternal;

namespace SOArchitecture
{
    public abstract class CollectionEditorBase : Editor
    {
        private const string HelpMessage = "If you don't want to raise an event just keep the field as None";
        
        private const string ItemsProperty = "items";
        private const string ArraySizeProperty = "Array.size";
        private const string OnChangedCollectionGameEventProperty = "onChangedCollection";
        
        protected abstract string Name { get; }

        private SerializedProperty _size;
        
        private SerializedProperty _items;
        private SerializedProperty _gameEvent;
        private SerializedProperty _element;
        
        private ReorderableList _reorderableList;

        private void OnEnable()
        {
            _gameEvent = serializedObject.FindProperty(OnChangedCollectionGameEventProperty);
            _items = serializedObject.FindProperty(ItemsProperty);
            _size = _items.FindPropertyRelative(ArraySizeProperty);

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

            EditorGUILayout.HelpBox(HelpMessage, MessageType.Info);
            EditorGUILayout.PropertyField(_gameEvent);
            GUILayout.Space(10);
            EditorGUILayout.PropertyField(_size);

            _reorderableList.DoLayoutList();
            serializedObject.ApplyModifiedProperties();
        }
    }
}
