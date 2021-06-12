using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    #region Inspect
    [SerializeField] private PlayerCharacter playerA;
    [SerializeField] private PlayerCharacter playerB;
    #endregion

    private bool playerAHasReachedGoal = false;
    private bool playerBHasReachedGoal = false;
    private bool shouldSwitchCharacters = false;

    private void Start()
    {
        playerA.IsActivated = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartCurrentLevel();
        }
    }

    private void LateUpdate()
    {
        if (shouldSwitchCharacters)
        {
            playerA.IsActivated = !playerA.IsActivated;
            playerB.IsActivated = !playerB.IsActivated;

            shouldSwitchCharacters = false;
        }
    }

    #region Winning Levels
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
    #endregion

    #region Player Characters
    public void SwitchActiveCharacter()
    {
        shouldSwitchCharacters = true;
    }

    public void TryToLinkObjects()
    {
        var targetPlayerA = playerA.GetFocussedLinkable();
        var targetPlayerB = playerB.GetFocussedLinkable();

        if(targetPlayerA == null || targetPlayerB == null)
        {
            return;
        }

        targetPlayerA.LinkWith(targetPlayerB);
        targetPlayerB.LinkWith(targetPlayerA);
    }
    #endregion

    private void RestartCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }
}
