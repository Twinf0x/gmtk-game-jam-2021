using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    private bool playerAHasReachedGoal = false;
    private bool playerBHasReachedGoal = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartCurrentLevel();
        }
    }

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

    private void RestartCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }
}
