using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coroutiner : MonoBehaviour, ICoroutiner
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
