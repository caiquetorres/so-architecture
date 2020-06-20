#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

namespace SOArchitecture
{
    [CreateAssetMenu(menuName = "SOArchitecture/FolderStructure", fileName = "New Folder Structure")]
    public class FolderStructure : ScriptableObject
    {
        private static FolderStructure _instance;
        
        [SerializeField] private string[] baseFolders;
        [SerializeField] private string[] entityFolders;

        #region Initialization
        
        private void OnEnable()
        {
            if (_instance == null)
                _instance = this;
            else
            {
                Debug.LogError("Cannot create two instances of the FolderStructure");
                DestroyImmediate(this);
            }
        }
        
        #endregion

        #region Folder creation
        
        [MenuItem("Assets/Create/Create Base Folders")]
        public static void CreateBaseFolders()
        {
            for (var i = _instance.baseFolders.Length - 1; i >= 0; i--)
            {
                var name = _instance.baseFolders[i];
                if (AssetDatabase.IsValidFolder(string.Concat("Assets/", name))) 
                    continue;
                
                var guid = AssetDatabase.CreateFolder("Assets", name);
                AssetDatabase.GUIDToAssetPath(guid);
            }
        }

        [MenuItem("Assets/Create/Create Entity Folders")]
        public static void CreateEntityFolders()
        {
            var folderPath = AssetDatabase.GetAssetPath(Selection.activeObject);
            for (var i = _instance.entityFolders.Length - 1; i >= 0; i--)
            {
                var name = _instance.entityFolders[i];
                if (AssetDatabase.IsValidFolder(string.Concat(folderPath, name))) 
                    continue;
                
                var guid = AssetDatabase.CreateFolder(folderPath, name);
                AssetDatabase.GUIDToAssetPath(guid);
            }
        }
        
        #endregion
    }
}

#endif
