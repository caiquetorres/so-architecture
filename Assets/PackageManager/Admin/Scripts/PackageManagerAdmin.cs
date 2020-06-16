using UnityEngine;

namespace PackageManager.Admin.Scripts
{
    [CreateAssetMenu(menuName = "API/PackageManager", fileName = "New Package Manager")]
    public class PackageManagerAdmin : ScriptableObject
    {
        [SerializeField] private string username;
        [SerializeField] private string password;

        [SerializeField] private new string name;
        [SerializeField] private string version;
        [SerializeField] private string path;
        [Multiline, SerializeField] private string description;

        public void Create()
        {
            
        }
    }
}
