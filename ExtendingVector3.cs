
using UnityEngine;
public static class ExtendingVector3
{
    public static bool IsGreaterOrEqual(this Vector3 local, Vector3 other)
    {
        if (local.x >= other.x && local.y >= other.y && local.z >= other.z)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool IsLesserOrEqual(this Vector3 local, Vector3 other)
    {
        if (local.x <= other.x && local.y <= other.y && local.z <= other.z)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}