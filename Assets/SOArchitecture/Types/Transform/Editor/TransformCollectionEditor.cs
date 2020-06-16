using UnityEditor;
using SOArchitecture;

[CustomEditor(typeof(TransformCollection))]
public class TransformCollectionEditor : CollectionEditorBase
{
	protected override string Name => "Transform";
}
