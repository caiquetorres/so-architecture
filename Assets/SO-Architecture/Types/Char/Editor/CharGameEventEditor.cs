using System.IO;
using UnityEditor;
using SOArchitecture;

[CustomEditor(typeof(CharGameEvent))]
public class CharGameEventEditor : GameEventEditorBase<char, CharGameEvent>
{
    protected override void CreateInterface(string name)
    {
        base.CreateInterface(name);
        using (var outfile = new StreamWriter(NewInterfaceFilePath))
        {
            outfile.WriteLine(string.Concat("public interface I", name));
            outfile.WriteLine("{");
            outfile.WriteLine(string.Concat("    void ", name, "(char value);"));
            outfile.WriteLine("}");
        }

        AssetDatabase.Refresh();
    }
}
