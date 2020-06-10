#if UNITY_EDITOR

using System;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEditorInternal;
using System.Collections.Generic;

namespace SOArchitecture
{
    public class SOGerenerator : EditorWindow
    {
        private static string _className;
        private static TextAsset _textAsset;

        private bool _collection;
        private bool _variable;
        private bool _gameEvent;
        
        private int _type;

        private string _filePath;

        private SerializedObject _serializedObject;
        
        private SerializedProperty _element;
        private SerializedProperty _list;
        
        private ReorderableList _reorderableList;
        
        [SerializeField] private List<string> references = new List<string>
        {
            "UnityEngine"
        };
        
        [MenuItem("Window/SO Generator")] public static void ShowWindow()
        {
            var window = GetWindow<SOGerenerator>("SO Generator");
            window.minSize = new Vector2(406f, 250f);
        }

        private void OnEnable()
        {
            _serializedObject = new SerializedObject(this);
            _list = _serializedObject.FindProperty("references");
            _reorderableList = new ReorderableList(_serializedObject, _list, true, true, true, true)
            {
                drawElementCallback = (rect, index, isActive, isFocused) =>
                {
                    rect.height = EditorGUIUtility.singleLineHeight;
                    _element = _reorderableList.serializedProperty.GetArrayElementAtIndex(index);
                    EditorGUI.PropertyField(rect, _element);
                },
                drawHeaderCallback = rect => { EditorGUI.LabelField(rect, "Namespaces"); }
            };
        }

        private void OnGUI()
        {
            _collection = GUILayout.Toggle(_collection, "Collection");
            _variable = GUILayout.Toggle(_variable, "Variable");

            EditorGUI.BeginDisabledGroup(_variable);
                if (_variable)
                    _gameEvent = true;
                _gameEvent = GUILayout.Toggle(_gameEvent, "Game Event");
            EditorGUI.EndDisabledGroup();
            
            GUILayout.Space(10);
            
            _reorderableList.DoLayoutList();
            
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
                    
                    _className = _textAsset == null ? "" : _textAsset.name;
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
                EditorUtility.SetDirty(this);
                if (_className == string.Empty && _textAsset == null)
                    throw new Exception("You must write some name first");
                
                if (!Directory.Exists(_filePath) || _filePath == string.Empty)
                    throw new Exception("The path does not exist or is null");

                if (_collection)
                {
                    CreateCollection(_className, _filePath);
                }
                
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

        private void CreateCollection(string className, string path)
        {
            var pathToFile = string.Concat(path, "/", className.ToTitle(), "SO/");
            var collectionFilePath = string.Concat(pathToFile, className.ToTitle(), "Collection.cs");
            
            if (!Directory.Exists(pathToFile))
                Directory.CreateDirectory(pathToFile);

            if (!File.Exists(collectionFilePath))
                File.Create(collectionFilePath).Dispose();

            using (var outfile = new StreamWriter(collectionFilePath))
            {
                references.ForEach(reference =>
                {
                    outfile.WriteLine(string.Concat("using ", reference, ";"));
                });
                
                outfile.WriteLine("using SOArchitecture;\n");
                outfile.WriteLine(string.Concat(
                    "[CreateAssetMenu(menuName = ", '"', "SOArchitecture/Collection/", className, '"', ", fileName = ", '"', "New", className.ToTitle(), "Collection", '"', ")]"));
                outfile.WriteLine(string.Concat("public class ", className.ToTitle(), "Collection : Collection<", className, "> { }"));
            }
            
            var pathToEditor = string.Concat(pathToFile, "Editor/");
            var editorFilePath = string.Concat(pathToEditor, className.ToTitle(), "CollectionEditor.cs");
            
            if (!Directory.Exists(pathToEditor))
                Directory.CreateDirectory(pathToEditor);
            
            if (!File.Exists(editorFilePath))
                File.Create(editorFilePath).Dispose();

            using (var outfile = new StreamWriter(editorFilePath))
            {
                references.ForEach(reference =>
                {
                    outfile.WriteLine(string.Concat("using ", reference, ";"));
                });
                
                outfile.WriteLine("using UnityEditor;\nusing SOArchitecture;\n");
                outfile.WriteLine(string.Concat("[CustomEditor(typeof(", className.ToTitle(), "Collection))]"));
                outfile.WriteLine(string.Concat("public class ", className.ToTitle(),
                    "CollectionEditor : CollectionEditorBase"));
                outfile.WriteLine(string.Concat("{\n", "\tprotected override string Name => ", '"', className.ToTitle(), '"', ";\n}"));
            }
        }
        
        private void CreateVariable(string className, string path)
        {
            var pathToFile = string.Concat(path, "/", className.ToTitle(), "SO/");
            var variableFilePath = string.Concat(pathToFile, className.ToTitle(), "Variable.cs");

            if (!Directory.Exists(pathToFile))
                Directory.CreateDirectory(pathToFile);

            if (!File.Exists(variableFilePath))
                File.Create(variableFilePath).Dispose();
            
            using (var outfile = new StreamWriter(variableFilePath))
            {
                references.ForEach(reference =>
                {
                    outfile.WriteLine(string.Concat("using ", reference, ";"));
                });
                
                outfile.WriteLine("using SOArchitecture;\n");
                outfile.WriteLine(string.Concat(
                    "[CreateAssetMenu(menuName = ", '"', "SOArchitecture/Variables/", className, '"', ", fileName = ", '"', "New", className.ToTitle(), "Variable", '"', ")]"));
                outfile.WriteLine(string.Concat("public class ", className.ToTitle(), "Variable : VariableBase<", className, ", ", className.ToTitle(), "GameEvent> { }"));
            }

            var pathToEditor = string.Concat(pathToFile, "Editor/");
            var editorFilePath = string.Concat(pathToEditor, className.ToTitle(), "VariableEditor.cs");
            
            if (!Directory.Exists(pathToEditor))
                Directory.CreateDirectory(pathToEditor);
            
            if (!File.Exists(editorFilePath))
                File.Create(editorFilePath).Dispose();
            
            using (var outfile = new StreamWriter(editorFilePath))
            {
                outfile.WriteLine("using UnityEditor;\nusing SOArchitecture;\n");
                outfile.WriteLine(string.Concat(
                    "[CanEditMultipleObjects, CustomEditor(typeof(", 
                    className.ToTitle(), 
                    "Variable))]"));
                outfile.WriteLine(string.Concat(
                    "public class ",
                    className.ToTitle(), 
                    "VariableEditor : VariableEditorBase { }"));
            }
        }
        
        private void CreateGameEvent(string className, string path)
        {
            var pathToFile = string.Concat(path, "/", className.ToTitle(), "SO/");
            var gameEventFilePath = string.Concat(pathToFile, className.ToTitle(), "GameEvent.cs");

            if (!Directory.Exists(pathToFile))
                Directory.CreateDirectory(pathToFile);

            if (!File.Exists(gameEventFilePath))
                File.Create(gameEventFilePath).Dispose();
            using (var outfile = new StreamWriter(gameEventFilePath))
            {
                references.ForEach(reference =>
                {
                    outfile.WriteLine(string.Concat("using ", reference, ";"));
                });
                
                outfile.WriteLine("using SOArchitecture;");
                outfile.WriteLine(string.Concat(
                    "\n[CreateAssetMenu(menuName = ", '"', "SOArchitecture/GameEvents/", className, '"', ", fileName = ", '"', "New", className.ToTitle(), "GameEvent", '"', ")]"));
                outfile.WriteLine(string.Concat("public class ", className.ToTitle(), "GameEvent : GameEventBase<", className, "> { }"));
            }

            var pathToEditor = string.Concat(pathToFile, "Editor/");
            var editorFilePath = string.Concat(pathToEditor, className.ToTitle(), "GameEventEditor.cs");
            
            if (!Directory.Exists(pathToEditor))
                Directory.CreateDirectory(pathToEditor);
            
            if (!File.Exists(editorFilePath))
                File.Create(editorFilePath).Dispose();

            using (var outfile = new StreamWriter(editorFilePath))
            {
                references.ForEach(reference =>
                {
                    outfile.WriteLine(string.Concat("using ", reference, ";"));
                });
                
                outfile.WriteLine("using System.IO;\nusing UnityEditor;\nusing SOArchitecture;\n");
                outfile.WriteLine(string.Concat("[CustomEditor(typeof(", className.ToTitle(), "GameEvent))]"));
                outfile.WriteLine(string.Concat("public class ", className.ToTitle(), "GameEventEditor : GameEventEditorBase<", className, ", ", className.ToTitle(), "GameEvent> { }"));
            }

            var unityEventFilePath = string.Concat(pathToFile, className.ToTitle(),"UnityEvent.cs");
            if (!File.Exists(unityEventFilePath))
                File.Create(unityEventFilePath).Dispose();
            using (var outfile = new StreamWriter(unityEventFilePath))
            {
                references.ForEach(reference =>
                {
                    outfile.WriteLine(string.Concat("using ", reference, ";"));
                });
                
                outfile.WriteLine("using UnityEngine.Events;\n");
                outfile.WriteLine("[System.Serializable]");
                outfile.WriteLine(string.Concat("public class ", className.ToTitle(), "UnityEvent : UnityEvent<", className, "> { }"));
            }
            
            var gameEventListenerFilePath = string.Concat(pathToFile, className.ToTitle(), "GameEventListener.cs");
            if (!File.Exists(gameEventListenerFilePath))
                File.Create(gameEventListenerFilePath).Dispose();
            using (var outfile = new StreamWriter(gameEventListenerFilePath))
            {
                references.ForEach(reference =>
                {
                    outfile.WriteLine(string.Concat("using ", reference, ";"));
                });
                
                outfile.WriteLine("using SOArchitecture;");
                outfile.WriteLine(string.Concat("public class ", className.ToTitle(), "GameEventListener : GameEventListenerBase<", className, ", ", className.ToTitle(), "GameEvent, ", className.ToTitle(), "UnityEvent> { }"));
            }
        }
    }
    
    public static class StringExtender
    {
        public static string ToTitle(this string value)
        {
            return string.Concat(value[0].ToString().ToUpper(), value.Substring(1, value.Length - 1));
        }
    }
}

#endif
