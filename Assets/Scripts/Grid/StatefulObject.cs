using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatefulObject : MonoBehaviour
{
    #region Inspector
    [SerializeField] private GameObject blockingState;
    [SerializeField] private GameObject nonBlockingState;
    [Header("Audio")]
    [SerializeField] private AudioSource blockingSFX;
    [SerializeField] private AudioSource nonBlockingSFX;
    #endregion

    public void SetBlocking()
    {
        blockingState.SetActive(true);
        nonBlockingState.SetActive(false);
        blockingSFX.Play();
    }

    public void SetNonBlocking()
    {
        blockingState.SetActive(false);
        nonBlockingState.SetActive(true);
        nonBlockingSFX.Play();
    }
}
