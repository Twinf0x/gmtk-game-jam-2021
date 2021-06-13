using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TriggerType { None, WalkOn, WalkOff }

public class PressurePlate : MonoBehaviour
{
    #region Inspector
    [SerializeField] private WalkOnTrigger walkOn;
    [SerializeField] private WalkOffTrigger walkOff;
    [SerializeField] private float ignoreTime = 0.1f;
    #endregion

    private float timer = 0f;
    private TriggerType lastTrigger = TriggerType.None;

    private void LateUpdate()
    {
        timer -= Time.deltaTime;
    }

    public void TriggerWalkOn()
    {
        if(timer > 0f)
        {
            timer = 0f;
            return;
        }

        if(lastTrigger == TriggerType.WalkOn)
        {
            timer = ignoreTime;
            return;
        }

        walkOn.onEnter?.Invoke();
        timer = ignoreTime;
        lastTrigger = TriggerType.WalkOn;
    }

    public void TriggerWalkOff()
    {
        if (timer > 0f)
        {
            timer = 0f;
            return;
        }

        if(lastTrigger == TriggerType.WalkOff)
        {
            timer = ignoreTime;
            return;
        }

        walkOff.onLeave?.Invoke();
        timer = ignoreTime;
        lastTrigger = TriggerType.WalkOff;
    }
}
