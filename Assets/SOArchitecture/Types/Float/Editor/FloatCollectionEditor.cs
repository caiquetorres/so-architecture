using UnityEngine;
using UnityEditor;
using SOArchitecture;

[CustomEditor(typeof(FloatCollection))]
public class FloatCollectionEditor : CollectionEditorBase
{
	protected override string Name => "Float";
}
