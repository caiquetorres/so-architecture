using System.IO;
using UnityEditor;
using UnityEngine;

namespace SOArchitecture.Editor.Helpers
{
    public static class SOArchitectureEditorHelpers
    {
        /// <summary>
        /// Static method that draw in the inspector the description of the ScriptableObject
        /// </summary>
        /// <param name="descriptionProperty">Parameter that indicates the property that will be drawn</param>
        /// <param name="showing">Parameter that indicates if the property is being drawing in the inspector</param>
        public static void DrawDescription(SerializedProperty descriptionProperty, ref bool showing)
        {
            showing = EditorGUILayout.Foldout(showing, "Description");
            if (showing)
                EditorGUILayout.PropertyField(descriptionProperty, GUIContent.none);
        }

        /// <summary>
        /// Static method that create a .cs file with an interface
        /// </summary>
        /// <param name="name">Parameter that indicates the Game Event name</param>
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

        /// <summary>
        /// Static method that create a .cs file with an interface
        /// </summary>
        /// <param name="name">Parameter that indicates the Game Event name</param>
        /// <typeparam name="TValue">Parameter that indicates the type of the Game Event</typeparam>
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
