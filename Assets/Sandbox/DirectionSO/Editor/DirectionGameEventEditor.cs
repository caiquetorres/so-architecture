using Game;
using UnityEngine;
using System.IO;
using UnityEditor;
using SOArchitecture;

[CustomEditor(typeof(DirectionGameEvent))]
public class DirectionGameEventEditor : GameEventEditorBase<Direction, DirectionGameEvent> { }
