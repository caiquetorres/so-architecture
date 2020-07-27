#if UNITY_EDITOR

using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace SOArchitecture
{
    [CreateAssetMenu(menuName = "SOArchitecture/Settings", fileName = "New SOSettings")]
    public class SOSettings : ScriptableObject
    {
        private const string DefaultNewSettingsLocation = "Assets";
        private const string AssetDatabaseSearchString = "t:SOSettings";
        private const string DefaultNewSettingsName = "SOSettings.asset";

        private static SOSettings _instance;

        private static SOSettings Instance
        {
            get
            {
                if (_instance == null)
                    _instance = GetInstance();

                return _instance;
            }
        }
        
        #region Asset creation
        
        private static SOSettings GetInstance()
        {
            var instance = FindInstanceInProject();
            return instance == null ? CreateInstance() : instance;
        }

        private static SOSettings FindInstanceInProject()
        {
            var settingsGUIDs = AssetDatabase.FindAssets(AssetDatabaseSearchString);

            if (settingsGUIDs.Length == 0)
                return null;
            
            if (settingsGUIDs.Length > 1)
            {
                throw new Exception("Found more than one instance of SOSettings" +
                                    $"\nTo find all instances, type {AssetDatabaseSearchString} into the project view search bar");
            }

            var settingsPath = AssetDatabase.GUIDToAssetPath(settingsGUIDs[0]);
            return AssetDatabase.LoadAssetAtPath<SOSettings>(settingsPath);
        }

        private static SOSettings CreateInstance()
        {
            var newSettings = ScriptableObject.CreateInstance<SOSettings>();
            
            AssetDatabase.CreateAsset(newSettings, Path.Combine(DefaultNewSettingsLocation, DefaultNewSettingsName));
            AssetDatabase.SaveAssets();

            return newSettings;
        }
        
        #endregion
    }
}

#endif
