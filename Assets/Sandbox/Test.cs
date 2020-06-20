using Malee.List;
using UnityEngine;

namespace Game.Tests
{
    public class Test : MonoBehaviour
    {
        [Reorderable, SerializeField] private IntReordableList listTest;
    }
    
    [System.Serializable] public class IntReordableList : ReorderableArray<int> { }
}
