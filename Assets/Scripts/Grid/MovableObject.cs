using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObject : MonoBehaviour
{
    #region Inspector
    [SerializeField] internal float movementSpeed = 5f;
    #endregion
    internal Vector2 gridPosition;
    internal Vector3 desiredPosition;

    internal void Awake()
    {
        Grid.AddMovableObject(this);
        gridPosition = Grid.GetClosestGridPosition(transform.position);
        desiredPosition = transform.position;
    }

    internal void Update()
    {
        if(desiredPosition != transform.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, desiredPosition, movementSpeed * Time.deltaTime);
        }
    }

    public virtual bool IsReadyToMove()
    {
        return desiredPosition == transform.position;
    }

    public virtual bool CanMoveInDirection(GridDirection direction)
    {
        if (transform.position != desiredPosition)
        {
            return false;
        }

        var targetPosition = Grid.GetWorldPosition(gridPosition, direction);

        // Check if there is an obstacle
        var movementBlocked = Physics.CheckSphere(targetPosition, 0.5f, Grid.ObstacleLayers);
        if (movementBlocked)
        {
            return false;
        }

        // Check if we would have to push sth out of the way
        var movableObjects = Physics.OverlapSphere(targetPosition, 0.5f, Grid.MovableObjectLayers);
        if (movableObjects.Length <= 0)
        {
            // If not we're good to go
            return true;
        }
        else
        {
            // If there's something, check if it can be pushed
            var targetObject = movableObjects[0].GetComponent<MovableObject>();
            // This should never be the case
            if(targetObject == null)
            {
                Debug.LogError("Something's on the movable object layer, but has no MovableObject script attached to it", targetObject);
                return false;
            }

            return targetObject.CanMoveInDirection(direction);
        }
    }

    public virtual void Move(GridDirection direction)
    {
        if (!CanMoveInDirection(direction))
        {
            return;
        }

        desiredPosition = Grid.GetWorldPosition(gridPosition, direction);
        gridPosition += direction.ToVector2();
    }
}
