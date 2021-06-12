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
        var targetPosition = Grid.GetWorldPosition(gridPosition, direction);

        // Check if there is an obstacle
        var movementBlocked = Physics.CheckSphere(targetPosition, Grid.CheckSize, Grid.ObstacleLayers);
        if (movementBlocked)
        {
            return false;
        }

        // Check if we would have to push sth out of the way
        var movableObject = Grid.GetMovableObjectFromWorldPosition(targetPosition);
        if (movableObject == null)
        {
            // If not we're good to go
            return true;
        }
        else
        {
            return movableObject.CanMoveInDirection(direction);
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

        var movableObject = Grid.GetMovableObjectFromWorldPosition(desiredPosition);
        if(movableObject != null)
        {
            movableObject.Move(direction);
        }
    }
}
