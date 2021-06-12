using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatefulObject : MonoBehaviour
{
    #region Inspector
    [SerializeField] private GameObject blockingState;
    [SerializeField] private GameObject nonBlockingState;
    #endregion

    public void SetBlocking()
    {
        blockingState.SetActive(true);
        nonBlockingState.SetActive(false);
    }

    public void SetNonBlocking()
    {
        blockingState.SetActive(false);
        nonBlockingState.SetActive(true);
    }
}
