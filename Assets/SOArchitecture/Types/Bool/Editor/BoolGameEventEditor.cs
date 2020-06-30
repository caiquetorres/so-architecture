using UnityEngine;
using System.IO;
using UnityEditor;
using SOArchitecture;

[CustomEditor(typeof(BoolGameEvent))]
public class BoolGameEventEditor : GameEventEditorBase<bool, BoolGameEvent> { }
