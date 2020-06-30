using UnityEngine;
using UnityEditor;
using SOArchitecture;

[CustomEditor(typeof(IntCollection))]
public class IntCollectionEditor : CollectionEditorBase
{
	protected override string Name => "Int";
}
