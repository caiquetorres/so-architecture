using System;
using System.Collections.Generic;
using PackageManager.Admin.Classes;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

namespace PackageManager.Admin.Scripts
{
    [CreateAssetMenu(menuName = "API/PackageManager", fileName = "New Package Manager")]
    public class PackageManagerAdmin : ScriptableObject
    {
        private const string ContentType = "Content-Type";

        private const string PackageFormat = ".unitypackage";
        private const string ContentTypeJson = "application/json";
        private const string ContentTypeMultipart = "multipart/form-data";

        private string _fileName;

        [SerializeField] private string url;
        [SerializeField] private string port;

        [SerializeField] private string username;
        [SerializeField] private string password;

        [SerializeField] private new string name;
        [SerializeField] private string version;
        [SerializeField] private string path;
        [Multiline, SerializeField] private string description;

        public void Create()
        {
            Login(handler =>
            {
                switch (handler.state)
                {
                    case "failure":
                        throw new Exception("Username or password incorrect");
                    case "error":
                        throw new Exception("error");
                    default:
                        CreatePackage(handler.token);
                        break;
                }
            });
        }

        private void Login(Action<UserLoginResponseHandler> callback)
        {
            var webRequest = new UnityWebRequest(string.Concat(url, ':', port, "/users/login/"), "POST");
            webRequest.SetRequestHeader(ContentType, ContentTypeJson);
            var json = JsonUtility.ToJson(new UserLoginRequestHandler
            {
                username = username,
                password = password
            });

            webRequest.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(json))
            {
                contentType = ContentType
            };
            webRequest.downloadHandler = new DownloadHandlerBuffer();

            var request = webRequest.SendWebRequest();
            request.completed += delegate
            {
                callback(JsonUtility.FromJson<UserLoginResponseHandler>(webRequest.downloadHandler.text));
            };
        }

        public void CreatePackage(string token)
        {
            _fileName = string.Concat("Assets/", name, '@', version, PackageFormat);
            AssetDatabase.ExportPackage(
                path,
                _fileName,
                ExportPackageOptions.IncludeDependencies | ExportPackageOptions.Recurse);
            AssetDatabase.Refresh();

            var formData = new List<IMultipartFormSection>
            {
                new MultipartFormDataSection(string.Concat("name=", name, "&version=", version)),
                new MultipartFormDataSection("file", _fileName),
                new MultipartFormDataSection(string.Concat("description=", description))
            };

            var webRequest = UnityWebRequest.Post(string.Concat(url, ':', port, "/unityPackage"), formData);

            webRequest.SetRequestHeader("Authorization", token);
            webRequest.SetRequestHeader(ContentType, ContentTypeMultipart);
            
            webRequest.SendWebRequest();
        }
    }
}
