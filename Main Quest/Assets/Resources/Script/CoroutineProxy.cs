using UnityEngine;
using System.Collections;

public class CoroutineProxy : MonoBehaviour
{
    public static CoroutineProxy Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RunCoroutine(IEnumerator coroutine)
    {
        StartCoroutine(coroutine);
    }
}