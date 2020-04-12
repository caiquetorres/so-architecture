using System.IO;
using UnityEditor;
using SOArchitecture;

[CustomEditor(typeof(DoubleGameEvent))]
public class DoubleGameEventEditor : GameEventEditorBase<double, DoubleGameEvent>
{
    protected override void CreateInterface(string name)
    {
        base.CreateInterface(name);
        using (var outfile = new StreamWriter(NewInterfaceFilePath))
        {
            outfile.WriteLine(string.Concat("public interface I", name));
            outfile.WriteLine("{");
            outfile.WriteLine(string.Concat("    void ", name, "(double value);"));
            outfile.WriteLine("}");
        }

        AssetDatabase.Refresh();
    }
}
