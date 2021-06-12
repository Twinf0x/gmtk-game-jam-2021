using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum TriggerActivatedBy { All, BothPlayers, OnlyPlayerA, OnlyPlayerB }

public class WalkOnTrigger : MonoBehaviour
{
    [SerializeField] private TriggerActivatedBy activatedBy;
    [SerializeField] private UnityEvent onEnter;

    public void OnTriggerEnter(Collider other)
    {
        if (!CanTrigger(other.gameObject.tag))
        {
            return;
        }

        onEnter?.Invoke();
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
