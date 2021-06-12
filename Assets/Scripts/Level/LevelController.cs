using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    private bool playerAHasReachedGoal = false;
    private bool playerBHasReachedGoal = false;

    public void PlayerAEnteredGoal()
    {
        playerAHasReachedGoal = true;
        if (playerBHasReachedGoal)
        {
            WinLevel();
        }
    }

    public void PlayerALeftGoal()
    {
        playerAHasReachedGoal = false;
    }

    public void PlayerBEnteredGoal()
    {
        playerBHasReachedGoal = true;
        if (playerAHasReachedGoal)
        {
            WinLevel();
        }
    }

    public void PlayerBLeftGoal()
    {
        playerBHasReachedGoal = false;
    }

    private void WinLevel()
    {
        // TODO
        Debug.Log("You Win!");
    }
}
