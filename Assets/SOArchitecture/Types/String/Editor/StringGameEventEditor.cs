using UnityEngine;
using System.IO;
using UnityEditor;
using SOArchitecture;

[CustomEditor(typeof(StringGameEvent))]
public class StringGameEventEditor : GameEventEditorBase<string, StringGameEvent> { }
