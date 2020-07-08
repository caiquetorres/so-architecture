using System.IO;
using UnityEditor;
using UnityEngine;

namespace SOArchitecture
{
    public abstract class GameEventEditorBase<TGameEvent> : Editor
        where TGameEvent : GameEventBase
    {
        private bool _isShowingDescription;
        
        protected string NewInterfaceFilePath;
        private TGameEvent _gameEvent;

        private SerializedProperty _descriptionProperty;

        private void OnEnable()
        {
            _gameEvent = target as TGameEvent;
            
            _descriptionProperty = serializedObject.FindProperty("description");
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
                    CreateInterface(_gameEvent.name);
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
        
        protected virtual void CreateInterface(string name)
        {
            var fullPath = AssetDatabase.GetAssetPath(Selection.activeObject);
            var fileName = string.Concat("I", name, ".cs");

            var directoryPath = string.Concat(fullPath.Substring(0, fullPath.Length - name.Length - 6), "Interfaces/");
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            NewInterfaceFilePath = string.Concat(directoryPath, fileName);
            
            if (File.Exists(NewInterfaceFilePath))
                File.Delete(NewInterfaceFilePath);
        }
    }
    
    public abstract class GameEventEditorBase<TValue, TGameEvent> : Editor
        where TGameEvent : GameEventBase<TValue>
    {
        private bool _isShowingDescription;
        
        protected string NewInterfaceFilePath;
        private TGameEvent _gameEvent;

        private SerializedProperty _descriptionProperty;
        private SerializedProperty _simulateValueProperty;

        private void OnEnable()
        {
            _gameEvent = target as TGameEvent;
            
            _descriptionProperty = serializedObject.FindProperty("description");
            _simulateValueProperty = serializedObject.FindProperty("simulateValue");
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
                    CreateInterface(_gameEvent.name);
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

        protected virtual void CreateInterface(string name)
        {
            var fullPath = AssetDatabase.GetAssetPath(Selection.activeObject);
            var fileName = string.Concat("I", name, ".cs");

            var directoryPath = string.Concat(fullPath.Substring(0, fullPath.Length - name.Length - 6), "Interfaces/");
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            NewInterfaceFilePath = string.Concat(directoryPath, fileName);
            
            if (File.Exists(NewInterfaceFilePath))
                File.Delete(NewInterfaceFilePath);

            using (var outfile = new StreamWriter(NewInterfaceFilePath))
            {
                outfile.WriteLine(string.Concat("public interface I", name));
                outfile.WriteLine("{");
                outfile.WriteLine(string.Concat("    void ", name, "(", typeof(TValue), " value);"));
                outfile.WriteLine("}\n");
            }

            AssetDatabase.Refresh();
        }
    }
}
