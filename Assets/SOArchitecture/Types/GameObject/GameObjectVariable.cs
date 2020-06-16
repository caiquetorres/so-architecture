using UnityEngine;
using SOArchitecture;

[CreateAssetMenu(menuName = "SOArchitecture/Variables/GameObject", fileName = "NewGameObjectVariable")]
public class GameObjectVariable : VariableBase<GameObject, GameObjectGameEvent> { }
