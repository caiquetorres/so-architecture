using UnityEngine;
using System.IO;
using UnityEditor;
using SOArchitecture;

[CustomEditor(typeof(IntGameEvent))]
public class IntGameEventEditor : GameEventEditorBase<int, IntGameEvent> { }
