using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAnimation : MonoBehaviour
{
    public Animator animator;
    public float animationSpeedMin = 0.75f;
    public float animationSpeedMax = 1.25f;

    public void Start()
    {
        animator.SetFloat("AnimationSpeed", Random.Range(animationSpeedMin, animationSpeedMax));
        animator.SetFloat("AnimationOffset", Random.Range(0f, 1f));
    }
}
