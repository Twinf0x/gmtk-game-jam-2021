using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkableObject : MovableObject
{
    private List<LinkableObject> linkedObjects = new List<LinkableObject>();
    private bool gotMovementOrderThisFrame = false;

    private void LateUpdate()
    {
        gotMovementOrderThisFrame = false;
    }

    public void LinkWith(LinkableObject other)
    {
        linkedObjects.Add(other);
    }

    public void GetAllLinkedObjects(ref List<LinkableObject> allLinkedObjects)
    {
        allLinkedObjects.Add(this);

        foreach(var obj in linkedObjects)
        {
            if (!allLinkedObjects.Contains(obj))
            {
                obj.GetAllLinkedObjects(ref allLinkedObjects);
            }
        }
    }

    public override bool CanMoveInDirection(GridDirection direction)
    {
        if (gotMovementOrderThisFrame)
        {
            return true;
        }

        return CanMoveInDirection(direction, false);
    }

    public bool CanMoveInDirection(GridDirection direction, bool checkOnlySelf)
    {
        List<LinkableObject> allLinkedObjects = new List<LinkableObject>();
        GetAllLinkedObjects(ref allLinkedObjects);

        if (checkOnlySelf)
        {
            var targetPosition = Grid.GetWorldPosition(gridPosition, direction);
            var neighbour = Grid.GetMovableObjectFromWorldPosition(targetPosition);
            if (neighbour is LinkableObject && allLinkedObjects.Contains((LinkableObject)neighbour))
            {
                return true;
            }

            return CanMoveInDirection(direction, allLinkedObjects);
        }

        foreach (var linkedObj in allLinkedObjects)
        {
            if(!linkedObj.CanMoveInDirection(direction, true))
            {
                return false;
            }
        }

        return true;
    }

    public override void Move(GridDirection direction)
    {
        Move(direction, false);
    }

    public void Move(GridDirection direction, bool onlySelf)
    {
        if (gotMovementOrderThisFrame)
        {
            return;
        }

        if (onlySelf)
        {
            gotMovementOrderThisFrame = true;
            base.Move(direction);
            return;
        }

        List<LinkableObject> allLinkedObjects = new List<LinkableObject>();
        GetAllLinkedObjects(ref allLinkedObjects);

        foreach (var linkedObj in allLinkedObjects)
        {
            linkedObj.Move(direction, true);
        }
    }
}
