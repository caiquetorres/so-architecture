using UnityEngine;
using SOArchitecture;
using System.Collections;

public class Player : MonoBehaviour
{
    [SerializeField] private GameEventFloat gameEventFloat;
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(2f);
        gameEventFloat.Raise(3.4f);
    }

    public void ShowValue(float value)
    {
        print(value);
    }
}
