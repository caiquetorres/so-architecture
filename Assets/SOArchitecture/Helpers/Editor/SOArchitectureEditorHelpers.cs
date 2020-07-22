using System.IO;
using UnityEditor;
using UnityEngine;

namespace SOArchitecture
{
    public static class SOArchitectureEditorHelpers
    {
        public static void DrawDescription(SerializedProperty descriptionProperty, ref bool showing)
        {
            showing = EditorGUILayout.Foldout(showing, "Description");
            if (showing)
                EditorGUILayout.PropertyField(descriptionProperty, GUIContent.none);
        }

        public static void CreateInterface(string name)
        {
            var fullPath = AssetDatabase.GetAssetPath(Selection.activeObject);
            var fileName = string.Concat("I", name, ".cs");

            var directoryPath = string.Concat(fullPath.Substring(0, fullPath.Length - name.Length - 6), "Interfaces/");
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            var newInterfaceFilePath = string.Concat(directoryPath, fileName);
            
            if (File.Exists(newInterfaceFilePath))
                File.Delete(newInterfaceFilePath);
            
            using (var outfile = new StreamWriter(newInterfaceFilePath))
            {
                outfile.WriteLine(string.Concat("public interface I", name));
                outfile.WriteLine("{");
                outfile.WriteLine(string.Concat("    void ", name, "();"));
                outfile.WriteLine("}");
            }

            AssetDatabase.Refresh();
        }

        public static void CreateInterface<TValue>(string name)
        {
            var fullPath = AssetDatabase.GetAssetPath(Selection.activeObject);
            var fileName = string.Concat("I", name, ".cs");

            var directoryPath = string.Concat(fullPath.Substring(0, fullPath.Length - name.Length - 6), "Interfaces/");
            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            var newInterfaceFilePath = string.Concat(directoryPath, fileName);
            
            if (File.Exists(newInterfaceFilePath))
                File.Delete(newInterfaceFilePath);

            using (var outfile = new StreamWriter(newInterfaceFilePath))
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
