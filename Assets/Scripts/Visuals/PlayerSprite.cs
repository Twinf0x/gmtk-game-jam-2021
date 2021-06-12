using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprite : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void SetSprite(GridDirection direction)
    {
        animator.SetInteger("Direction", (int)direction);
    }
}
