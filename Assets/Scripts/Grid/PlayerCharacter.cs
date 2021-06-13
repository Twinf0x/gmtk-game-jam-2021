using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MovableObject
{
    public static string TAG_PLAYER_A = "PlayerA";
    public static string TAG_PLAYER_B = "PlayerB";

    #region Inspector
    [Header("Settings")]
    [SerializeField] private GridDirection startOrientation = GridDirection.Up;
    [Header("References")]
    [SerializeField] private LevelController levelController;
    [SerializeField] private PlayerSprite playerSprite;
    [SerializeField] public Transform linkMarker;
    [Header("Audio")]
    [SerializeField] private AudioSource sfxFootsteps;
    [SerializeField] public AudioSource sfxSpell;
    #endregion

    public bool IsActivated { get; set; }

    internal new void Awake()
    {
        base.Awake();
        linkMarker.parent = null;
        linkMarker.position = Grid.GetWorldPosition(gridPosition, startOrientation);
        playerSprite.SetSprite(startOrientation);
    }

    internal new void Update()
    {
        if(IsActivated && Grid.IsEverythingReady())
        {
            CheckInput();
        }

        base.Update();
    }

    internal void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            levelController.TryToLinkObjects();
        }
        else if (Input.GetKeyDown(KeyCode.Tab))
        {
            levelController.SwitchActiveCharacter();
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
        playerSprite.SetSprite(direction);
        sfxFootsteps.Play();

        base.Move(direction);

        linkMarker.position = Grid.GetWorldPosition(gridPosition, direction);
    }

    public LinkableObject GetFocussedLinkable()
    {
        var targetObject = Grid.GetMovableObjectFromWorldPosition(linkMarker.position);
        if (targetObject == null || !(targetObject is LinkableObject))
        {
            return null;
        }
        else
        {
            return (LinkableObject)targetObject;
        }
    }
}
