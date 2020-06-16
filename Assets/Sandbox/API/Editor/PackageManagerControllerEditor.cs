using System;
using UnityEditor;
using UnityEngine;

[CanEditMultipleObjects, CustomEditor(typeof(PackageManagerController))]
public class PackageManagerControllerEditor : Editor
{
    private bool _showPackage = true;

    private PackageManagerController _controller;

    private SerializedProperty
        _usernameProperty,
        _passwordProperty,
        _packageNameProperty,
        _packageVersionProperty,
        _packagePathProperty,
        _packageDescriptionProperty;


    private void OnEnable()
    {
        _controller = (PackageManagerController) target;

        _usernameProperty = serializedObject.FindProperty("_username");
        _passwordProperty = serializedObject.FindProperty("_password");

        _packageNameProperty = serializedObject.FindProperty("_name");
        _packageVersionProperty = serializedObject.FindProperty("_version");
        _packagePathProperty = serializedObject.FindProperty("_path");
        _packageDescriptionProperty = serializedObject.FindProperty("_description");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

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
                _packagePathProperty.stringValue = EditorGUILayout.TextField("path:", _packagePathProperty.stringValue);
                if (GUILayout.Button("Select folder"))
                {
                    _packagePathProperty.stringValue = string.Empty;
                    _packagePathProperty.stringValue =
                        EditorUtility.OpenFolderPanel(
                            "Select folder", 
                            Application.dataPath, 
                            string.Empty);
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
            _controller.Create();
        }
    }
}