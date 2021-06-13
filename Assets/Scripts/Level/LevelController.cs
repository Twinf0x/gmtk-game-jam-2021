using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    #region Inspect
    [Header("References - Scene")]
    [SerializeField] private PlayerCharacter playerA;
    [SerializeField] private PlayerCharacter playerB;
    [SerializeField] private Transform activePlayerMarker;
    [SerializeField] private GameObject youWinCanvas;
    [Header("References - Assets")]
    [SerializeField] private LinkVFX linkPrefab;
    [SerializeField] private GameObject spellVFXPrefab;
    #endregion

    private bool playerAHasReachedGoal = false;
    private bool playerBHasReachedGoal = false;
    private bool shouldSwitchCharacters = false;

    private void Start()
    {
        playerA.IsActivated = true;
        activePlayerMarker.parent = playerA.transform;
        activePlayerMarker.position = playerA.transform.position;
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

            if (playerA.IsActivated)
            {
                activePlayerMarker.parent = playerA.transform;
                activePlayerMarker.position = playerA.transform.position;
            }
            else
            {
                activePlayerMarker.parent = playerB.transform;
                activePlayerMarker.position = playerB.transform.position;
            }

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
        youWinCanvas.SetActive(true);
    }

    public void GoToNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
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
        if(targetPlayerA == null)
        {
            Instantiate(spellVFXPrefab, playerA.linkMarker.position, Quaternion.identity);
        }
        else
        {
            Instantiate(spellVFXPrefab, playerA.linkMarker.position + (Vector3.up * 0.6f), Quaternion.identity);
        }
        var targetPlayerB = playerB.GetFocussedLinkable();
        if (targetPlayerB == null)
        {
            Instantiate(spellVFXPrefab, playerB.linkMarker.position, Quaternion.identity);
        }
        else
        {
            Instantiate(spellVFXPrefab, playerB.linkMarker.position + (Vector3.up * 0.6f), Quaternion.identity);
        }

        if (targetPlayerA == null || targetPlayerB == null || targetPlayerA == targetPlayerB)
        {
            return;
        }

        targetPlayerA.LinkWith(targetPlayerB);
        targetPlayerB.LinkWith(targetPlayerA);
        var vfx = Instantiate(linkPrefab);
        vfx.Initialize(targetPlayerA, targetPlayerB);
    }
    #endregion

    private void RestartCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }
}
