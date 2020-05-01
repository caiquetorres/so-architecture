using UnityEditor;
using SOArchitecture;

[CustomEditor(typeof(GameObjectCollection))]
public class GameObjectCollectionEditor : CollectionEditorBase
{
	protected override string Name => "GameObject";
}
