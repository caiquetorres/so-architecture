using Game;
using UnityEngine;
using UnityEditor;
using SOArchitecture;

[CustomEditor(typeof(DirectionCollection))]
public class DirectionCollectionEditor : CollectionEditorBase
{
	protected override string Name => "Direction";
}
