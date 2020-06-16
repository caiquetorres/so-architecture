using UnityEngine;

[CreateAssetMenu(menuName = "API/PackageManager", fileName = "New Package Manager")]
public class PackageManagerController : ScriptableObject
{
    [SerializeField] private string _username;
    [SerializeField] private string _password;

    [SerializeField] private string _name;
    [SerializeField] private string _version;
    [SerializeField] private string _path;
    [Multiline, SerializeField] private string _description;

    public void Create()
    {
        
    }
}
