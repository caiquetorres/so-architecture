using UnityEngine;
using UnityEditor;
using SOArchitecture;

[CustomEditor(typeof(StringCollection))]
public class StringCollectionEditor : CollectionEditorBase
{
	protected override string Name => "String";
}
