using UnityEngine;
using SOArchitecture;

[CreateAssetMenu(menuName = "SOArchitecture/Variables/Transform", fileName = "NewTransformVariable")]
public class TransformVariable : VariableBase<Transform, TransformGameEvent> { }
