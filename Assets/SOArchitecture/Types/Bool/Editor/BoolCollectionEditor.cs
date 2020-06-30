using UnityEngine;
using UnityEditor;
using SOArchitecture;

[CustomEditor(typeof(BoolCollection))]
public class BoolCollectionEditor : CollectionEditorBase
{
	protected override string Name => "Bool";
}
