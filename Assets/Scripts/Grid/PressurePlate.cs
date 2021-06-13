using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    #region Inspector
    [SerializeField] private WalkOnTrigger walkOn;
    [SerializeField] private WalkOffTrigger walkOff;
    [SerializeField] private float triggerDelay = 0.1f;
    #endregion

    private bool walkOnTriggered = false;
    private bool walkOffTriggered = false;

    private IEnumerator CheckTriggers()
    {
        yield return new WaitForSeconds(triggerDelay);

        if(walkOnTriggered && walkOffTriggered)
        {
            // Do nothing
        }
        else if (walkOnTriggered)
        {
            walkOn.onEnter?.Invoke();
        }
        else if (walkOffTriggered)
        {
            walkOff.onLeave?.Invoke();
        }

        walkOnTriggered = false;
        walkOffTriggered = false;
    }

    public void TriggerWalkOn()
    {
        walkOnTriggered = true;
        StartCoroutine(CheckTriggers());
    }

    public void TriggerWalkOff()
    {
        walkOffTriggered = true;
        StartCoroutine(CheckTriggers());
    }
}
