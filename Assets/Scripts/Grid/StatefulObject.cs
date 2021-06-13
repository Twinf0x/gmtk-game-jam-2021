using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatefulObject : MonoBehaviour
{
    #region Inspector
    [SerializeField] private bool isDeadly = false;
    [SerializeField] private GameObject blockingState;
    [SerializeField] private GameObject nonBlockingState;
    [Header("Audio")]
    [SerializeField] private AudioSource blockingSFX;
    [SerializeField] private AudioSource nonBlockingSFX;
    [Header("References - Scene")]
    [SerializeField] private LevelController levelController;
    #endregion

    public void SetBlocking()
    {
        blockingState.SetActive(true);
        nonBlockingState.SetActive(false);
        blockingSFX.Play();

        if (isDeadly)
        {
            var movable = Grid.GetMovableObjectFromWorldPosition(transform.position);
            if (movable != null)
            {
                levelController.LoseLevel();
            }
        }
    }

    public void SetNonBlocking()
    {
        blockingState.SetActive(false);
        nonBlockingState.SetActive(true);
        nonBlockingSFX.Play();
    }
}
