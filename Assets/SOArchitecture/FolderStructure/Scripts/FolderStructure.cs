#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace SOArchitecture
{
    [CreateAssetMenu(menuName = "SOArchitecture/FolderStructure", fileName = "New Folder Structure")]
    public class FolderStructure : ScriptableObject
    {
        private const string DefaultNewFolderStructureLocation = "Assets";
        private const string AssetDatabaseSearchString = "t:FolderStructure";
        private const string DefaultNewFolderStructureName = "FolderStructure.asset";
        
        private static FolderStructure _instance;

        private static FolderStructure Instance
        {
            get
            {
                if (_instance == null)
                    _instance = GetInstance();
                
                return _instance;
            }
        }

        [SerializeField] private List<string> baseFolders = new List<string>
        {
            "3rdParty",
            "__Game__",
            "Scenes",
            "Sandbox",
        };
        [SerializeField] private List<string> entityFolders = new List<string>
        {
            "__Shared__",
            "Prefabs",
            "ScriptableObjects",
            "Scripts",
        };

        #region Instanciate new FolderStructure
        
        private static FolderStructure GetInstance()
        {
            var instance = FindInstanceInProject();
            return instance == null ? CreateInstance() : instance;
        }

        private static FolderStructure FindInstanceInProject()
        {
            var folderStructureGUIDs = AssetDatabase.FindAssets(AssetDatabaseSearchString);

            if (folderStructureGUIDs.Length == 0)
                return null;
            
            if (folderStructureGUIDs.Length > 1)
            {
                throw new Exception("Found more than one instance of FolderStructure" +
                                 $"\nTo find all instances, type {AssetDatabaseSearchString} into the project view search bar");
            }

            var folderStructurePath = AssetDatabase.GUIDToAssetPath(folderStructureGUIDs[0]);
            return AssetDatabase.LoadAssetAtPath<FolderStructure>(folderStructurePath);
        }

        private static FolderStructure CreateInstance()
        {
            var newFolderStructure = ScriptableObject.CreateInstance<FolderStructure>();

            AssetDatabase.CreateAsset(newFolderStructure, Path.Combine(DefaultNewFolderStructureLocation, DefaultNewFolderStructureName));
            AssetDatabase.SaveAssets();
            
            return newFolderStructure;
        }
        
        #endregion
        
        #region Folder creation
        
        [MenuItem("Assets/Create/Create Base Folders")]
        public static void CreateBaseFolders()
        {
            Debug.Log(Instance.baseFolders);
            for (var i = Instance.baseFolders.Count - 1; i >= 0; i--)
            {
                var name = Instance.baseFolders[i];
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
            for (var i = Instance.entityFolders.Count - 1; i >= 0; i--)
            {
                var name = Instance.entityFolders[i];
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
