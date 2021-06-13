using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestructor : MonoBehaviour
{
    public float timeToLive = 2f;

    private void Awake()
    {
        StartCoroutine(SelfDestruction());
    }

    private IEnumerator SelfDestruction()
    {
        yield return new WaitForSeconds(timeToLive);

        Destroy(gameObject);
    }
}
