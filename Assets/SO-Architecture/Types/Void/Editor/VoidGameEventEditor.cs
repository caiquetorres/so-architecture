using System.IO;
using UnityEditor;
using SOArchitecture;

[CustomEditor(typeof(VoidGameEvent))]
public class VoidGameEventEditor : GameEventEditorBase<VoidGameEvent>
{
    protected override void CreateInterface(string name)
    {
        base.CreateInterface(name);
        using (var outfile = new StreamWriter(NewInterfaceFilePath))
        {
            outfile.WriteLine(string.Concat("public interface I", name));
            outfile.WriteLine("{");
            outfile.WriteLine(string.Concat("    void ", name, "();"));
            outfile.WriteLine("}");
        }

        AssetDatabase.Refresh();
    }
}
