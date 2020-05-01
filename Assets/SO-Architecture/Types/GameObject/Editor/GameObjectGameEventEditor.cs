using System.IO;
using UnityEditor;
using SOArchitecture;
using UnityEngine;

[CustomEditor(typeof(GameObjectGameEvent))]
public class GameObjectGameEventEditor : GameEventEditorBase<GameObject, GameObjectGameEvent>
{
    protected override void CreateInterface(string name)
    {
        base.CreateInterface(name);
        using (var outfile = new StreamWriter(NewInterfaceFilePath))
        {
            outfile.WriteLine(string.Concat("public interface I", name));
            outfile.WriteLine("{");
            outfile.WriteLine(string.Concat("    void ", name, "(GameObject value);"));
            outfile.WriteLine("}");
        }

        AssetDatabase.Refresh();
    }
}
