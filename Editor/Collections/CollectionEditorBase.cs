using SOArchitecture.Editor.Helpers;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace SOArchitecture.Editor.Collections
{
    public abstract class CollectionEditorBase : UnityEditor.Editor
    {
        private const string HelpMessage = "If you don't want to raise an event just keep the field as None";
        
        private const string ItemsProperty = "items";
        private const string ArraySizeProperty = "Array.size";
        private const string DescriptionProperty = "description";
        private const string OnChangedCollectionGameEventProperty = "onChangedCollection";

        protected abstract string Name { get; }
        
        private bool _isShowingDescription; 

        private SerializedProperty _size;
        
        private SerializedProperty _items;
        private SerializedProperty _element;
        private SerializedProperty _gameEvent;
        private SerializedProperty _descriptionProperty;
        
        private ReorderableList _reorderableList;

        private void OnEnable()
        {
            _items = serializedObject.FindProperty(ItemsProperty);
            _size = _items.FindPropertyRelative(ArraySizeProperty);
            _descriptionProperty = serializedObject.FindProperty(DescriptionProperty);
            _gameEvent = serializedObject.FindProperty(OnChangedCollectionGameEventProperty);

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
            EditorGUI.BeginDisabledGroup(true);
            {
                EditorGUILayout.ObjectField(
                    "Script:", 
                    MonoScript.FromScriptableObject((ScriptableObject) target), serializedObject.GetType(), 
                    false);
            }
            EditorGUI.EndDisabledGroup();
            
            GUILayout.Space(10);
            
            serializedObject.Update();

            EditorGUILayout.HelpBox(HelpMessage, MessageType.Info);
            EditorGUILayout.PropertyField(_gameEvent);
            GUILayout.Space(10);
            EditorGUILayout.PropertyField(_size);

            _reorderableList.DoLayoutList();
            
            GUILayout.Space(10);
            SOArchitectureEditorHelpers.DrawDescription(_descriptionProperty, ref _isShowingDescription);
            
            serializedObject.ApplyModifiedProperties();
        }
    }
}
