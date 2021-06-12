using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprite : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private Camera targetCamera;

    private void Awake()
    {
        targetCamera = Camera.main;
    }

    private void Update()
    {
        Vector3 targetPosition = targetCamera.transform.position;
        targetPosition.y = transform.position.y;
        transform.LookAt(targetPosition, Vector3.up);
    }

    public void SetSprite(GridDirection direction)
    {
        animator.SetInteger("Direction", (int)direction);
    }
}
