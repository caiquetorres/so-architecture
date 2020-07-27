using SOArchitecture;
using UnityEngine;

[CreateAssetMenu] public class ScriptableObjectTest : ScriptableObject
{
    [SerializeField] private int age;
    [SerializeField] private new string name;
    [ShowVariableValue, SerializeField] private DirectionVariable directionVariable;
}
