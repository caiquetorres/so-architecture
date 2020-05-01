using System.IO;
using UnityEditor;
using SOArchitecture;
using UnityEngine;

[CustomEditor(typeof(TransformGameEvent))]
public class TransformGameEventEditor : GameEventEditorBase<Transform, TransformGameEvent>
{
    protected override void CreateInterface(string name)
    {
        base.CreateInterface(name);
        using (var outfile = new StreamWriter(NewInterfaceFilePath))
        {
            outfile.WriteLine(string.Concat("public interface I", name));
            outfile.WriteLine("{");
            outfile.WriteLine(string.Concat("    void ", name, "(Transform value);"));
            outfile.WriteLine("}");
        }

        AssetDatabase.Refresh();
    }
}
