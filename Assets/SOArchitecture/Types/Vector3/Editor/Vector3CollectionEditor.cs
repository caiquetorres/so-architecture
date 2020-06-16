using UnityEditor;
using SOArchitecture;

[CustomEditor(typeof(Vector3Collection))]
public class Vector3CollectionEditor : CollectionEditorBase
{
	protected override string Name => "Vector3";
}
