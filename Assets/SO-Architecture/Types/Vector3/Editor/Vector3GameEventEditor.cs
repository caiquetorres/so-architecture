using System.IO;
using UnityEditor;
using UnityEngine;
using SOArchitecture;

[CustomEditor(typeof(Vector3GameEvent))]
public class Vector3GameEventEditor : GameEventEditorBase<Vector3, Vector3GameEvent>
{
    protected override void CreateInterface(string name)
    {
        base.CreateInterface(name);
        using (var outfile = new StreamWriter(NewInterfaceFilePath))
        {
            outfile.WriteLine(string.Concat("public interface I", name));
            outfile.WriteLine("{");
            outfile.WriteLine(string.Concat("    void ", name, "(Vector3 value);"));
            outfile.WriteLine("}");
        }

        AssetDatabase.Refresh();
    }
}
