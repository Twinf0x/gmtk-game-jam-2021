using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GridDirection { Up, Down, Left, Right }

public static class GridDirectionExtensions
{
    public static Vector2 ToVector2(this GridDirection direction)
    {
        switch (direction)
        {
            case GridDirection.Up:
                return Vector2.up;
            case GridDirection.Down:
                return Vector2.down;
            case GridDirection.Left:
                return Vector2.left;
            case GridDirection.Right:
                return Vector2.right;
            default:
                return Vector2.zero;
        }
    }
}

public class Grid : MonoBehaviour
{
    #region Singleton
    private static Grid instance;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    #endregion

    #region Inspector
    [SerializeField] private float cellSize = 3f;
    [SerializeField] private LayerMask obstacleLayers;
    [SerializeField] private LayerMask movableObjectLayers;
    #endregion

    private List<MovableObject> movableObjects = new List<MovableObject>();

    #region Static Access
    public static float CellSize { get { return instance.cellSize; } }
    public static LayerMask ObstacleLayers { get { return instance.obstacleLayers; } }
    public static LayerMask MovableObjectLayers { get { return instance.movableObjectLayers; } }

    public static Vector3 GetWorldPosition(Vector2 gridPosition)
    {
        return new Vector3(gridPosition.x * CellSize, 0f, gridPosition.y * CellSize);
    }

    public static Vector3 GetWorldPosition(Vector2 gridPosition, GridDirection direction)
    {
        return GetWorldPosition(gridPosition + direction.ToVector2());
    }

    public static Vector2 GetClosestGridPosition(Vector3 worldPosition)
    {
        float x = Mathf.Round(worldPosition.x / CellSize);
        float y = Mathf.Round(worldPosition.z / CellSize);

        return new Vector2(x, y);
    }

    public static void AddMovableObject(MovableObject obj)
    {
        instance.movableObjects.Add(obj);
    }

    public static MovableObject GetMovableObjectFromWorldPosition(Vector3 worldPosition)
    {
        var movableObjects = Physics.OverlapSphere(worldPosition, 0.5f, Grid.MovableObjectLayers);
        if (movableObjects.Length <= 0)
        {
            // If not we're good to go
            return null;
        }
        else
        {
            // If there's something, check if it can be pushed
            var targetObject = movableObjects[0].GetComponent<MovableObject>();
            // This should never be the case
            if (targetObject == null)
            {
                Debug.LogError("Something's on the movable object layer, but has no MovableObject script attached to it", targetObject);
                return null;
            }

            return targetObject;
        }
    }

    public static bool IsEverythingReady()
    {
        foreach(var obj in instance.movableObjects)
        {
            if (!obj.IsReadyToMove())
            {
                return false;
            }
        }

        return true;
    }
    #endregion
}
