#if UNITY_EDITOR

using System.Collections.Generic;
using System.IO;
using PackageManager.Client.Classes;
using UnityEngine;
using UnityEngine.Networking;

namespace PackageManager.Client.Scripts
{
    [CreateAssetMenu(menuName = "Package Manager", fileName = "New Package Manager")]
    public class PackageManagerClient : ScriptableObject
    {
        private const string ContentType = "Content-Type";
        
        private const string PackageFormat = ".unitypackage";
        private const string ContentTypeJson = "application/json";
        
        private UnityWebRequest _webRequest;
        private DownloadHandlerFile _downloadHandler;
        private UnityWebRequestAsyncOperation _request;

        [SerializeField] public bool updateWhenInitialize;
        [SerializeField] private bool showPackageChanges;

        [SerializeField] private string url;
        [SerializeField] private string folderPath;

        [SerializeField] private List<PackageInfo> packages;

        public void Apply()
        {
            var path = Path.Combine(Application.dataPath, folderPath);
            for (var i = packages.Count - 1; i >= 0; i--)
            {
                var fileName = string.Concat(packages[i].name, PackageFormat);
                var filePath = string.Concat(path, fileName);
                
                _webRequest = UnityWebRequest.Get(url);
                _webRequest.SetRequestHeader(ContentType, ContentTypeJson);
                var json = JsonUtility.ToJson(
                    new RequestContentHandler(packages[i].name, packages[i].version));
                Debug.Log(json);                
                if (json == null)
                    return;

                _webRequest.uploadHandler = 
                new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(json))
                {
                    contentType = ContentTypeJson
                };

                var request = _webRequest.SendWebRequest();
                request.completed += delegate
                {
                    
                };
            }
        }
    }
}

#endif
