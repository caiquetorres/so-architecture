using SOArchitecture.Editor.GameEvents;
using SOArchitecture.Runtime.Types.Void;
using UnityEditor;

namespace SOArchitecture.Editor.Types.Void
{
    [CustomEditor(typeof(VoidGameEvent))]
    public class VoidGameEventEditor : GameEventEditorBase<VoidGameEvent> { }
}
