using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace SOArchitecture
{
    public class SOGerenerator : EditorWindow
    {
        private static string _className;
        private static TextAsset _textAsset;

        private bool _variable;
        private bool _gameEvent;
        
        private int _type;

        private string _filePath;
        
        [MenuItem("Window/SO Generator")]
        public static void ShowWindow()
        {
            var window = GetWindow<SOGerenerator>("SO Generator");
            window.minSize = new Vector2(406f, 250f);
        }

        private void OnGUI()
        {
            _variable = GUILayout.Toggle(_variable, "Variable");

            EditorGUI.BeginDisabledGroup(_variable);
            if (_variable)
                _gameEvent = true;
            _gameEvent = GUILayout.Toggle(_gameEvent, "Game Event");
            EditorGUI.EndDisabledGroup();
            
            GUILayout.Space(10);
            _type = GUILayout.Toolbar(_type, new[] {"Asset", "String"});
            switch (_type)
            {
                case 0:
                    _textAsset = (TextAsset) EditorGUILayout.ObjectField(
                        "Text file:",
                        _textAsset, 
                        typeof(TextAsset), 
                        false);
                    
                    _className = _textAsset == null ? "none" : _textAsset.name;
                    break;
                case 1:
                    _className = EditorGUILayout.TextField("Text:", _className);
                    break;
            }

            GUILayout.Space(10);
            EditorGUILayout.BeginHorizontal();
            
            _filePath = EditorGUILayout.TextField("path:", _filePath);
            if (GUILayout.Button("Select folder"))
            {
                _filePath = string.Empty;
                _filePath = EditorUtility.OpenFolderPanel("Select folder", Application.dataPath, _className);
                GUI.FocusControl(null);
            }
            EditorGUILayout.EndHorizontal();

            if (GUILayout.Button("Create"))
            {
                if (!Directory.Exists(_filePath) || _filePath == string.Empty)
                    throw new Exception("The path does not exist or is null");

                if (_variable)
                {
                    CreateGameEvent(_className, _filePath);
                    CreateVariable(_className, _filePath);
                }
                else if (_gameEvent)
                {
                    CreateGameEvent(_className, _filePath);
                }
            }
        }
        
        private static void CreateGameEvent(string className, string path)
        {
            var pathToFile = string.Concat(path, "/GameEvents/");
            var gameEventFilePath = string.Concat(pathToFile, className, "GameEvent.cs");

            if (!Directory.Exists(pathToFile))
                Directory.CreateDirectory(pathToFile);

            if (!File.Exists(gameEventFilePath))
                File.Create(gameEventFilePath).Dispose();

            using (var outfile = new StreamWriter(gameEventFilePath))
            {
                outfile.WriteLine("using SOArchitecture;");
                outfile.WriteLine("using UnityEngine;");
                outfile.WriteLine(string.Concat(
                    "\n[CreateAssetMenu(menuName = ", '"', "SOArchitecture/GameEvents/", _className, '"', ", fileName = ", '"', "New", _className, "GameEvent", '"', ")]"));
                outfile.WriteLine(string.Concat("public class ", _className, "GameEvent : GameEventBase<", _className, "> { }"));
            }
            AssetDatabase.Refresh();
        }

        private static void CreateVariable(string className, string path)
        {
            
        }
    }
}
