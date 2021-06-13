using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum TriggerActivatedBy { All, BothPlayers, OnlyPlayerA, OnlyPlayerB }

public class WalkOnTrigger : MonoBehaviour
{
    [Header("Optional")]
    [SerializeField] private PressurePlate pressurePlate;
    [Header("Settings")]
    [SerializeField] private TriggerActivatedBy activatedBy;
    [SerializeField] public UnityEvent onEnter;

    public void OnTriggerEnter(Collider other)
    {
        if (!CanTrigger(other.gameObject.tag))
        {
            return;
        }

        if (pressurePlate != null)
        {
            pressurePlate.TriggerWalkOn();
        }
        else
        {
            onEnter?.Invoke();
        }
    }

    private bool CanTrigger(string tag)
    {
        switch (activatedBy)
        {
            case TriggerActivatedBy.All:
                return true;
            case TriggerActivatedBy.BothPlayers:
                return tag == PlayerCharacter.TAG_PLAYER_A || tag == PlayerCharacter.TAG_PLAYER_B;
            case TriggerActivatedBy.OnlyPlayerA:
                return tag == PlayerCharacter.TAG_PLAYER_A;
            case TriggerActivatedBy.OnlyPlayerB:
                return tag == PlayerCharacter.TAG_PLAYER_B;
            default:
                return false;
        }
    }
}
