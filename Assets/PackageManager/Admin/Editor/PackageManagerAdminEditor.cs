using UnityEditor;
using PackageManager.Admin.Scripts;
using UnityEngine;

namespace PackageManager.Admin.Editor
{
    [CanEditMultipleObjects, CustomEditor(typeof(PackageManagerAdmin))]
    public class PackageManagerAdminEditor : UnityEditor.Editor
    {
        private bool _showPackage = true;

        private PackageManagerAdmin _packageManager;

        private SerializedProperty
            _urlProperty,
            _portProperty,
            _usernameProperty,
            _passwordProperty,
            _packageNameProperty,
            _packageVersionProperty,
            _packagePathProperty,
            _packageDescriptionProperty;


        private void OnEnable()
        {
            _packageManager = (PackageManagerAdmin) target;

            _urlProperty = serializedObject.FindProperty("url");
            _portProperty = serializedObject.FindProperty("port");

            _usernameProperty = serializedObject.FindProperty("username");
            _passwordProperty = serializedObject.FindProperty("password");

            _packageNameProperty = serializedObject.FindProperty("name");
            _packageVersionProperty = serializedObject.FindProperty("version");
            _packagePathProperty = serializedObject.FindProperty("path");
            _packageDescriptionProperty = serializedObject.FindProperty("description");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            
            EditorGUILayout.PropertyField(_urlProperty);
            EditorGUILayout.PropertyField(_portProperty);
            
            GUILayout.Space(10);
            EditorGUILayout.PropertyField(_usernameProperty);
            _passwordProperty.stringValue = EditorGUILayout.PasswordField("Password", _passwordProperty.stringValue);
            
            GUILayout.Space(10);
            _showPackage = EditorGUILayout.Foldout(_showPackage, "Package");
            if (_showPackage)
            {
                EditorGUILayout.PropertyField(_packageNameProperty);
                EditorGUILayout.PropertyField(_packageVersionProperty);
            
                GUILayout.BeginHorizontal();
                {
                    _packagePathProperty.stringValue =
                        EditorGUILayout.TextField("path:", _packagePathProperty.stringValue);
                    if (GUILayout.Button("Select folder"))
                    {
                        _packagePathProperty.stringValue = string.Empty;
                        _packagePathProperty.stringValue =
                            EditorUtility.OpenFolderPanel(
                                "Select folder",
                                Application.dataPath,
                                string.Empty);
                        _packagePathProperty.stringValue = _packagePathProperty.stringValue.Replace(Application.dataPath, "Assets");
                        GUI.FocusControl(null);
                    }
                }
                GUILayout.EndHorizontal();
            
                EditorGUILayout.PropertyField(_packageDescriptionProperty);
            }
            
            serializedObject.ApplyModifiedProperties();
            
            var buttonStyle = new GUIStyle(GUI.skin.button)
            {
                fixedWidth = 100f
            };
            
            if (GUILayout.Button("Create", buttonStyle))
            {
                _packageManager.Create();
            }
        }
    }
}
