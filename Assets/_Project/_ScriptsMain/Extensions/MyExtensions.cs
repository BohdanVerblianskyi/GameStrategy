using System.Collections.Generic;
using UnityEngine;

public static class MyExtensions
{
    public static Direction GetDirection(this Vector2 direction)
    {
        if (Vector2.Angle(direction, Vector2.up) <= 45.0)
        {
            return Direction.Up;
        }
        else if (Vector3.Angle(direction, Vector2.right) <= 45.0)
        {
            return Direction.Right;
        }
        else if (Vector3.Angle(direction, Vector2.down) <= 45.0)
        {
            return Direction.Down;
        }
        else
        {
            return Direction.Left;
        }
    }

    public static List<Vector2> GetPositionList(this List<Transform> transforms)
    {
        List<Vector2> listVector2 = new List<Vector2>();

        foreach (Transform transform  in transforms)
        {
            listVector2.Add(transform.position);
        }
        return listVector2;
    }

    public static Vector2 GetClosestPosition(this List<Vector2> EndPositions, Vector2 StartPosition)
    {
        float minDistance = float.MaxValue;
        Vector2 closerPosition = EndPositions[0];

        foreach (Vector2 position in EndPositions)
        {
           float distance = Vector2.Distance(position, StartPosition);
            if (distance < minDistance)
            {
                minDistance = distance;
                closerPosition = position;
            }
        }

        return closerPosition;
    }

    public static Vector2 ToWorldPosition(this Vector2 screenPosition, Camera camera)
    {
        return camera.ScreenToWorldPoint(screenPosition);
    }

    public static bool TryGetComponentFromRaycast<T>(this Ray ray, out  T component, LayerMask layerMask)
    {
        if (Physics.Raycast(ray, out RaycastHit raycastHit,layerMask))
        {
            return raycastHit.collider.TryGetComponent(out component);
        }
        component = default;
        return false;
    }

    public static bool IsRaycastCollisionInLayer(this Ray ray, LayerMask layerMask)
    {
        return Physics.Raycast(ray,layerMask);
    }
    
    public static bool TryGetComponentFromRaycast<T>(this Ray ray, out T component)
    {
        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            return raycastHit.collider.TryGetComponent(out component);
        }
        component = default;
        return false;
    }

}

public enum Direction
{
    Up,
    Down,
    Left,
    Right
}
