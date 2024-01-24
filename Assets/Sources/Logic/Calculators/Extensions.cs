using UnityEngine;

public static class Extensions
{
    public static void GetBounds(this Transform transform,out Vector3 minBound,out Vector3 maxBound)
    {
        Vector3 min = Vector3.one * float.MaxValue;
        Vector3 max = Vector3.one * float.MinValue;

        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            min = Vector3.Min(min, child.localPosition);
            max = Vector3.Max(max, child.localPosition);
        }

        minBound = min;
        maxBound = max;
    }
}
