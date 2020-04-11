using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    [SerializeField] private FloatVariable floatVariable;
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(2f);
        floatVariable.Value = 2f;
    }

    public void ShowValue(float value)
    {
        print(value);
    }
}
