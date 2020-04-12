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
                AssetDatabase.Refresh();
            }
        }
        
        private static void CreateGameEvent(string className, string path)
        {
            var pathToFile = string.Concat(path, "/", className, "/");
            var gameEventFilePath = string.Concat(pathToFile, className, "GameEvent.cs");

            if (!Directory.Exists(pathToFile))
                Directory.CreateDirectory(pathToFile);

            if (!File.Exists(gameEventFilePath))
                File.Create(gameEventFilePath).Dispose();
            using (var outfile = new StreamWriter(gameEventFilePath))
            {
                outfile.WriteLine("using UnityEngine;");
                outfile.WriteLine("using SOArchitecture;");
                outfile.WriteLine(string.Concat(
                    "\n[CreateAssetMenu(menuName = ", '"', "SOArchitecture/GameEvents/", _className, '"', ", fileName = ", '"', "New", _className, "GameEvent", '"', ")]"));
                outfile.WriteLine(string.Concat("public class ", _className, "GameEvent : GameEventBase<", _className, "> { }"));
            }

            var pathToEditor = string.Concat(pathToFile, "Editor/");
            var editorFilePath = string.Concat(pathToEditor, className, "GameEventEditor.cs");
            
            if (!Directory.Exists(pathToEditor))
                Directory.CreateDirectory(pathToEditor);
            
            if (!File.Exists(editorFilePath))
                File.Create(editorFilePath).Dispose();

            using (var outfile = new StreamWriter(editorFilePath))
            {
                outfile.WriteLine("using System.IO;\nusing UnityEditor;\nusing SOArchitecture;\n");
                outfile.WriteLine(string.Concat("[CustomEditor(typeof(", className, "GameEvent))]"));
                outfile.WriteLine(string.Concat("public class ", className, "GameEventEditor : GameEventEditorBase<", className, ", ", className, "GameEvent>\n{"));
                outfile.WriteLine("    protected override void CreateInterface(string name)");
                outfile.WriteLine("    {");
                outfile.WriteLine("        base.CreateInterface(name);");
                outfile.WriteLine("        using (var outfile = new StreamWriter(NewInterfaceFilePath))");
                outfile.WriteLine("        {");
                outfile.WriteLine(string.Concat("            outfile.WriteLine(string.Concat(", '"', "public interface I", '"', ", name));"));
                outfile.WriteLine(string.Concat("            outfile.WriteLine(", '"', '{', '"', ");"));
                outfile.WriteLine(string.Concat("            outfile.WriteLine(string.Concat(", '"', "    void ", '"', ", name, ", '"', '(', className, " value);", '"', "));"));
                outfile.WriteLine(string.Concat("            outfile.WriteLine(", '"', '}', '"', ");"));
                outfile.WriteLine("        }\n\n        AssetDatabase.Refresh();\n    }\n}");
            }

            var unityEventFilePath = string.Concat(pathToFile, className,"UnityEvent.cs");
            if (!File.Exists(unityEventFilePath))
                File.Create(unityEventFilePath).Dispose();
            using (var outfile = new StreamWriter(unityEventFilePath))
            {
                outfile.WriteLine("using UnityEngine.Events;\n");
                outfile.WriteLine("[System.Serializable]");
                outfile.WriteLine(string.Concat("public class ", className, "UnityEvent : UnityEvent<", className, "> { }"));
            }
            
            var gameEventListenerFilePath = string.Concat(pathToFile, className, "GameEventListener.cs");
            if (!File.Exists(gameEventListenerFilePath))
                File.Create(gameEventListenerFilePath).Dispose();
            using (var outfile = new StreamWriter(gameEventListenerFilePath))
            {
                outfile.WriteLine("using SOArchitecture;");
                outfile.WriteLine(string.Concat("public class ", className, "GameEventListener : GameEventListenerBase<", className, ", ", className, "GameEvent, ", className, "UnityEvent> { }"));
            }
        }

        private static void CreateVariable(string className, string path)
        {
            var pathToFile = string.Concat(path, "/", className, "/");
            var variableFilePath = string.Concat(pathToFile, className, "Variable.cs");

            if (!Directory.Exists(pathToFile))
                Directory.CreateDirectory(pathToFile);

            if (!File.Exists(variableFilePath))
                File.Create(variableFilePath).Dispose();
            
            using (var outfile = new StreamWriter(variableFilePath))
            {
                outfile.WriteLine("using UnityEngine;");
                outfile.WriteLine("using SOArchitecture;\n");
                outfile.WriteLine(string.Concat(
                    "[CreateAssetMenu(menuName = ", '"', "SOArchitecture/Variables/", _className, '"', ", fileName = ", '"', "New", _className, "Variable", '"', ")]"));
                outfile.WriteLine(string.Concat("public class ", className, "Variable : VariableBase<", className, ", ", className, "GameEvent> { }"));
            }
        }
    }
}
