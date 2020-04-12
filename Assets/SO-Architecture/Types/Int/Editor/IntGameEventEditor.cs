using System.IO;
using UnityEditor;
using SOArchitecture;

[CustomEditor(typeof(IntGameEvent))]
public class IntGameEventEditor : GameEventEditorBase<int, IntGameEvent>
{
    protected override void CreateInterface(string name)
    {
        base.CreateInterface(name);
        using (var outfile = new StreamWriter(NewInterfaceFilePath))
        {
            outfile.WriteLine(string.Concat("public interface I", name));
            outfile.WriteLine("{");
            outfile.WriteLine(string.Concat("    void ", name, "(int value);"));
            outfile.WriteLine("}");
        }

        AssetDatabase.Refresh();
    }
}
