using UnityEditor;
using SOArchitecture;

[CanEditMultipleObjects]
[CustomEditor(typeof(CollectionInt))]
public class CollectionIntEditorBase : CollectionEditorBase
{
    protected override string Name => "Int";
}
