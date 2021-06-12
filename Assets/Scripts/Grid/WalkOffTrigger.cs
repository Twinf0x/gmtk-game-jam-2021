using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WalkOffTrigger : MonoBehaviour
{
    [SerializeField] private TriggerActivatedBy activatedBy;
    [SerializeField] private UnityEvent onLeave;

    public void OnTriggerExit(Collider other)
    {
        if (!CanTrigger(other.gameObject.tag))
        {
            return;
        }

        onLeave?.Invoke();
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
