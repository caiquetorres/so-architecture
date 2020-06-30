using UnityEngine;
using UnityEditor;
using SOArchitecture;

[CustomEditor(typeof(CharCollection))]
public class CharCollectionEditor : CollectionEditorBase
{
	protected override string Name => "Char";
}
