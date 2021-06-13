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
    [SerializeField] private GameObject youLoseCanvas;
    [Header("References - Assets")]
    [SerializeField] private LinkVFX linkPrefab;
    [SerializeField] private GameObject spellVFXPrefab;
    [Header("Audio")]
    [SerializeField] private AudioSource sfxWin;
    [SerializeField] private AudioSource sfxLose;
    [SerializeField] private AudioSource sfxSwitch;
    #endregion

    private bool playerAHasReachedGoal = false;
    private bool playerBHasReachedGoal = false;
    private bool shouldSwitchCharacters = false;

    private void Start()
    {
        playerA.IsActivated = true;
        activePlayerMarker.parent = playerA.transform;
        activePlayerMarker.position = playerA.transform.position;

        Time.timeScale = 1f;
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
        sfxWin.Play();
        Time.timeScale = 0f;
        youWinCanvas.SetActive(true);
    }

    public void LoseLevel()
    {
        sfxLose.Play();
        Time.timeScale = 0f;
        youLoseCanvas.SetActive(true);
    }

    public void GoToNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
    }
    #endregion

    #region Player Characters
    public void SwitchActiveCharacter()
    {
        sfxSwitch.Play();
        shouldSwitchCharacters = true;
    }

    public void TryToLinkObjects()
    {
        playerA.sfxSpell.Play();
        playerB.sfxSpell.Play();

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

    public void RestartCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
