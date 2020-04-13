using System.IO;
using UnityEditor;
using UnityEngine;

namespace SOArchitecture
{
    public abstract class GameEventEditorBase<TValue, TGameEvent> : Editor
        where TGameEvent : GameEventBase<TValue>
    {
        protected string NewInterfaceFilePath;
        private TGameEvent _gameEvent;

        private void OnEnable()
        {
            _gameEvent = target as TGameEvent;
        }

        public override void OnInspectorGUI()
        {
            EditorGUI.BeginDisabledGroup(Application.isPlaying);
            
            EditorGUILayout.HelpBox("Do not create game events with white spaces", MessageType.Warning);
            
            var buttonStyle = new GUIStyle(GUI.skin.button)
            {
                alignment = TextAnchor.MiddleCenter,
                fixedWidth = 200f,
                margin = new RectOffset(0,0,10,20),
            };

            if (GUILayout.Button("Create interface", buttonStyle))
            {
                CreateInterface(_gameEvent.name);
            }
            EditorGUI.EndDisabledGroup();
            
            var rightAlignment = new GUIStyle(GUI.skin.label)
            {
                alignment = TextAnchor.LowerRight
            };

            var boldText = new GUIStyle(GUI.skin.label)
            {
                fontStyle = FontStyle.Bold
            };

            EditorGUI.BeginDisabledGroup(!Application.isPlaying);

            GUILayout.Label("Value: " + _gameEvent.value, boldText);
            
            var so = new SerializedObject(_gameEvent);
            var sp = so.FindProperty("simulateValue");
            if (sp != null)
            {
                EditorGUILayout.PropertyField(sp);
                so.ApplyModifiedProperties();
                GUILayout.Space(10);
            }

            GUILayout.Label("Press the Raise button to simulate the event in the game");
            if (GUILayout.Button("Raise"))
            {
                _gameEvent.Raise(_gameEvent.simulateValue);
            }
            
            EditorGUI.EndDisabledGroup();
        }

        protected virtual void CreateInterface(string name)
        {
            var fullPath = AssetDatabase.GetAssetPath(Selection.activeObject);
            var fileName = string.Concat("I", name, ".cs");

            var directoryPath = string.Concat(fullPath.Substring(0, fullPath.Length - name.Length - 6), "Interfaces/");
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            NewInterfaceFilePath = string.Concat(directoryPath, fileName);
            
            if (File.Exists(NewInterfaceFilePath))
            {
                File.Delete(NewInterfaceFilePath);
            }
        }
    }
}
