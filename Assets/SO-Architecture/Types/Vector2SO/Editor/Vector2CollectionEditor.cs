using UnityEditor;
using SOArchitecture;

[CustomEditor(typeof(Vector2Collection))]
public class Vector2CollectionEditor : CollectionEditorBase
{
	protected override string Name => "Vector2";
}
