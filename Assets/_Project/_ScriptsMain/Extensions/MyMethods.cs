using UnityEngine;

namespace _Project._ScriptsMain.Extensions
{
    public static class MyMethods
    {
        public static void BoxStretching(Vector3 startPosition, Vector3 endPosition, out Vector3 center,
            out Vector3 scale)
        {
            center = (startPosition + endPosition) / 2;
            scale = new Vector2(
                Mathf.Abs(startPosition.x - endPosition.x),
                Mathf.Abs(startPosition.y - endPosition.y));
        }
    }
}