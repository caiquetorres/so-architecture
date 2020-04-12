using System.IO;
using UnityEditor;
using SOArchitecture;
using UnityEngine;

[CustomEditor(typeof(Vector2GameEvent))]
public class Vector2GameEventEditor : GameEventEditorBase<Vector2, Vector2GameEvent>
{
    protected override void CreateInterface(string name)
    {
        base.CreateInterface(name);
        using (var outfile = new StreamWriter(NewInterfaceFilePath))
        {
            outfile.WriteLine(string.Concat("public interface I", name));
            outfile.WriteLine("{");
            outfile.WriteLine(string.Concat("    void ", name, "(Vector2 value);"));
            outfile.WriteLine("}");
        }

        AssetDatabase.Refresh();
    }
}
