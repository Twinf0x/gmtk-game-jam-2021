using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MovableObject
{
    #region Inspector
    [Header("Settings")]
    [SerializeField] private GridDirection startOrientation = GridDirection.Up;
    [Header("References")]
    [SerializeField] private MovableObject otherCharacter;
    [SerializeField] private PlayerSprite[] playerSprites;
    [SerializeField] private Transform linkMarkerSelf;
    [SerializeField] private Transform linkMarkerOther;
    #endregion

    internal new void Awake()
    {
        base.Awake();
        otherCharacter.transform.parent = null;
        linkMarkerSelf.parent = null;
        linkMarkerOther.parent = null;
        linkMarkerSelf.position = Grid.GetWorldPosition(gridPosition, startOrientation);
        linkMarkerOther.position = Grid.GetWorldPosition(otherCharacter.gridPosition, startOrientation);
    }

    internal new void Update()
    {
        if(Grid.IsEverythingReady())
        {
            CheckInput();
        }

        base.Update();
    }

    internal void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TryToLinkObjects();
        }
        else if (Input.GetAxisRaw("Horizontal") > 0f)
        {
            Move(GridDirection.Right);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0f)
        {
            Move(GridDirection.Left);
        }
        else if (Input.GetAxisRaw("Vertical") > 0f)
        {
            Move(GridDirection.Up);
        }
        else if (Input.GetAxisRaw("Vertical") < 0f)
        {
            Move(GridDirection.Down);
        }
    }

    public override void Move(GridDirection direction)
    {
        foreach(var sprite in playerSprites)
        {
            sprite.SetSprite(direction);
        }

        otherCharacter.Move(direction);
        base.Move(direction);

        linkMarkerSelf.position = Grid.GetWorldPosition(gridPosition, direction);
        linkMarkerOther.position = Grid.GetWorldPosition(otherCharacter.gridPosition, direction);
    }

    public void TryToLinkObjects()
    {
        var targetObjectSelf = Grid.GetMovableObjectFromWorldPosition(linkMarkerSelf.position);
        if(targetObjectSelf == null || !(targetObjectSelf is LinkableObject))
        {
            return;
        }

        var targetObjectOther = Grid.GetMovableObjectFromWorldPosition(linkMarkerOther.position);
        if(targetObjectOther == null || !(targetObjectOther is LinkableObject))
        {
            return;
        }

        ((LinkableObject)targetObjectSelf).LinkWith((LinkableObject)targetObjectOther);
        ((LinkableObject)targetObjectOther).LinkWith((LinkableObject)targetObjectSelf);
    }
}
