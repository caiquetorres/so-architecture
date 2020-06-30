using UnityEngine;
using System.IO;
using UnityEditor;
using SOArchitecture;

[CustomEditor(typeof(CharGameEvent))]
public class CharGameEventEditor : GameEventEditorBase<char, CharGameEvent> { }
