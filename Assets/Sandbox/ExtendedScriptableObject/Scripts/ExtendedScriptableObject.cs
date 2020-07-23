using UnityEditor;
using UnityEngine;

namespace SOArchitecture
{
    public class ExtendedScriptableObject : PropertyAttribute { }

    [CustomPropertyDrawer(typeof(ExtendedScriptableObject))]
    public class ExtendedScriptableObjectDrawer : PropertyDrawer
    {
        
    }
}
