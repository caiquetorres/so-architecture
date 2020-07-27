using SOArchitecture;
using UnityEngine;

[CreateAssetMenu] public class ScriptableObjectTest : ScriptableObject
{
    [SerializeField] private int age;
    [ReadOnly, SerializeField] private new string name;
    [ShowVariableValue, SerializeField] private DirectionVariable directionVariable;
}
