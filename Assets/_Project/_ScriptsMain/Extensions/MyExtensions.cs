using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MyExtensions
{
    private const float DISTANCE_RAY = 100f;

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

    public static RaycastHit2D GetMouseRaycast(this RaycastHit2D raycastHit, Camera camera, LayerMask layerMask)
    {
        Ray ray = GetMouseRay(camera);
        return Physics2D.GetRayIntersection(ray, DISTANCE_RAY, layerMask);
    }

    public static RaycastHit2D[] GetMouseRaycasts(this RaycastHit2D raycastHit, Camera camera)
    {
        Ray ray = GetMouseRay(camera);
        return Physics2D.GetRayIntersectionAll(ray);
    }

    public static bool TryGetComoponentInMouseReycast<T>(this RaycastHit2D raycastHit, out T component)
    {
        Ray ray = GetMouseRay(Camera.main);
        RaycastHit2D[] raycastHits2D = raycastHit.GetMouseRaycasts(Camera.main);

        foreach (RaycastHit2D raycast in raycastHits2D)
        {
            if (raycast.collider.TryGetComponent(out T t))
            {
                component = t;
                return true;
            }
        }

        component = default;
        return false;
    }

    public static bool TryGetComoponentInMouseReycast<T>(this RaycastHit raycastHit, out T component, LayerMask layerMask)
    {
        Ray ray = GetMouseRay(Camera.main);
        if (Physics.Raycast(ray, out raycastHit, layerMask))
        {
            if (raycastHit.collider.TryGetComponent(out T t))
            {
                component = t;
                return true;
            }
        }

        component = default;
        return false;
    }

    public static bool TryGetComoponentInMouseReycast<T>(this RaycastHit raycastHit, out T component)
    {
        Ray ray = GetMouseRay(Camera.main);
        if (Physics.Raycast(ray, out raycastHit))
        {
            if (raycastHit.collider.TryGetComponent(out T t))
            {
                component = t;
                return true;
            }
        }

        component = default;
        return false;
    }

    private static Ray GetMouseRay(Camera camera)
    {
        return camera.ScreenPointToRay(Input.mousePosition);
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

}

public enum Direction
{
    Up,
    Down,
    Left,
    Right
}
