using UnityEngine;
using System.IO;
using UnityEditor;
using SOArchitecture;

[CustomEditor(typeof(TransformGameEvent))]
public class TransformGameEventEditor : GameEventEditorBase<Transform, TransformGameEvent> { }
