using UnityEditor;
using UnityEngine;
using PackageInfo = PackageManager.Client.Scripts.PackageInfo;

namespace PackageManager.Client.Editor
{
    [CustomPropertyDrawer(typeof(PackageInfo))]
    public class PackageInfoEditor : PropertyDrawer
    {
        private const float Padding = 4f;
        private const string NameProperty = "name";
        private const string VersionProperty = "version";

        private float _lastWidth;
        private float _packageNameWidth;
        private float _packageVersionWidth;

        private Rect _rectPackageName;
        private Rect _rectPackageVersion;
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            _lastWidth = GUILayoutUtility.GetLastRect().width;
            _packageNameWidth = position.width * 0.7f;
            _packageVersionWidth = position.width * 0.3f;
            
            _rectPackageName = new Rect
            {
                x = position.x,
                y = position.y,
                width = _packageNameWidth,
                height = EditorGUIUtility.singleLineHeight,
            };
            
            _rectPackageVersion = new Rect
            {
                x = position.x + _packageNameWidth + Padding,
                y = position.y,
                width = _packageVersionWidth - Padding,
                height = EditorGUIUtility.singleLineHeight,
            };

            EditorGUI.BeginProperty(position, label, property);
            {
                EditorGUI.PropertyField(
                    _rectPackageName, property.FindPropertyRelative(NameProperty), new GUIContent(label));
                EditorGUI.PropertyField(
                    _rectPackageVersion, property.FindPropertyRelative(VersionProperty), GUIContent.none);
            }
            EditorGUI.EndProperty();
        }
    }
}
