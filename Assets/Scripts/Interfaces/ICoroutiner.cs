using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICoroutiner
{
    Coroutine StartCoroutine(IEnumerator routine);
}
