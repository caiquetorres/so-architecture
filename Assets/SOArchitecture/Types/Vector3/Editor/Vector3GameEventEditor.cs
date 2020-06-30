using UnityEngine;
using System.IO;
using UnityEditor;
using SOArchitecture;

[CustomEditor(typeof(Vector3GameEvent))]
public class Vector3GameEventEditor : GameEventEditorBase<Vector3, Vector3GameEvent> { }
