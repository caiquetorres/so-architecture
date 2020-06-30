using UnityEngine;
using System.IO;
using UnityEditor;
using SOArchitecture;

[CustomEditor(typeof(GameObjectGameEvent))]
public class GameObjectGameEventEditor : GameEventEditorBase<GameObject, GameObjectGameEvent> { }
