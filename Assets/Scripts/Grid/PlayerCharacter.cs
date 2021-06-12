using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MovableObject
{
    #region Inspector
    [SerializeField] private MovableObject otherCharacter;
    #endregion

    internal new void Update()
    {
        if(Grid.IsEverythingReady())
        {
            CheckMovementInput();
        }

        base.Update();
    }

    internal void CheckMovementInput()
    {
        if (Input.GetAxisRaw("Horizontal") > 0f)
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
        otherCharacter.Move(direction);
        base.Move(direction);
    }
}
