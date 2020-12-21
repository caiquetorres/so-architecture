using SOArchitecture.Editor.Helpers;
using SOArchitecture.Runtime.GameEvents.Scripts;
using UnityEditor;
using UnityEngine;

namespace SOArchitecture.Editor.GameEvents
{
    public abstract class GameEventEditorBase<TGameEvent> : UnityEditor.Editor
        where TGameEvent : GameEventBase
    {
        private const string DescriptionProperty = "description";
        
        private bool _isShowingDescription;
        
        private TGameEvent _gameEvent;

        private SerializedProperty _descriptionProperty;

        private void OnEnable()
        {
            _gameEvent = target as TGameEvent;
            
            _descriptionProperty = serializedObject.FindProperty(DescriptionProperty);
        }

        public override void OnInspectorGUI()
        {
            var buttonStyle = new GUIStyle(GUI.skin.button)
            {
                alignment = TextAnchor.MiddleCenter,
                fixedWidth = 200f,
                margin = new RectOffset(0, 0, 10, 20),
            };
            
            serializedObject.Update();
            
            EditorGUI.BeginDisabledGroup(true);
            {
                EditorGUILayout.ObjectField(
                    "Script:", 
                    MonoScript.FromScriptableObject((ScriptableObject) target), serializedObject.GetType(), 
                    false);
            }
            EditorGUI.EndDisabledGroup();
            
            GUILayout.Space(10);
            EditorGUI.BeginDisabledGroup(Application.isPlaying);
            {
                EditorGUILayout.HelpBox("Do not create game events with white spaces", MessageType.Warning);

                if (GUILayout.Button("Create interface", buttonStyle))
                    SOArchitectureEditorHelpers.CreateInterface(_gameEvent.name);
            }
            EditorGUI.EndDisabledGroup();
            
            EditorGUI.BeginDisabledGroup(!Application.isPlaying);
            {
                GUILayout.Label("Press the Raise button to simulate the event in the game");
                if (GUILayout.Button("Raise"))
                    _gameEvent.Raise();
            }
            EditorGUI.EndDisabledGroup();

            GUILayout.Space(10);
            SOArchitectureEditorHelpers.DrawDescription(_descriptionProperty, ref _isShowingDescription);

            serializedObject.ApplyModifiedProperties();
        }
    }
    
    public abstract class GameEventEditorBase<TValue, TGameEvent> : UnityEditor.Editor
        where TGameEvent : GameEventBase<TValue>
    {
        private const string DescriptionProperty = "description";
        private const string SimulateValueProperty = "simulateValue";
        
        private bool _isShowingDescription;
        
        private TGameEvent _gameEvent;

        private SerializedProperty _descriptionProperty;
        private SerializedProperty _simulateValueProperty;

        private void OnEnable()
        {
            _gameEvent = target as TGameEvent;
            
            _descriptionProperty = serializedObject.FindProperty(DescriptionProperty);
            _simulateValueProperty = serializedObject.FindProperty(SimulateValueProperty);
        }

        public override void OnInspectorGUI()
        {
            var buttonStyle = new GUIStyle(GUI.skin.button)
            {
                alignment = TextAnchor.MiddleCenter,
                fixedWidth = 200f,
                margin = new RectOffset(0,0,10,20),
            };
            
            var boldText = new GUIStyle(GUI.skin.label)
            {
                fontStyle = FontStyle.Bold
            };
            
            serializedObject.Update();
            
            EditorGUI.BeginDisabledGroup(true);
            {
                EditorGUILayout.ObjectField(
                    "Script:", 
                    MonoScript.FromScriptableObject((ScriptableObject) target), serializedObject.GetType(), 
                    false);
            }
            EditorGUI.EndDisabledGroup();
            
            GUILayout.Space(10);
            EditorGUI.BeginDisabledGroup(Application.isPlaying);
            {
                EditorGUILayout.HelpBox("Do not create game events with white spaces", MessageType.Warning);
            
                if (GUILayout.Button("Create interface", buttonStyle))
                    SOArchitectureEditorHelpers.CreateInterface<TValue>(_gameEvent.name);
            }
            EditorGUI.EndDisabledGroup();

            EditorGUI.BeginDisabledGroup(!Application.isPlaying);
            {
                GUILayout.Label("Value: " + _gameEvent.value, boldText);
                if (_simulateValueProperty != null)
                {
                    EditorGUILayout.PropertyField(_simulateValueProperty);
                    GUILayout.Space(10);
                }
                GUILayout.Label("Press the Raise button to simulate the event in the game");
                if (GUILayout.Button("Raise"))
                    _gameEvent.Raise(_gameEvent.simulateValue);
            }
            EditorGUI.EndDisabledGroup();
            
            GUILayout.Space(10);
            SOArchitectureEditorHelpers.DrawDescription(_descriptionProperty, ref _isShowingDescription); 
            
            serializedObject.ApplyModifiedProperties();
        }
    }
}
