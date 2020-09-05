using System;
using UnityEngine;

public abstract class MathUtil
{
    public static bool InRangeInclusive(float min, float max, float val)
        => val >= min && val <= max;

    public static bool InRangeInclusive(int min, int max, int val)
        => val >= min && val <= max;

    public static bool InRange(float min, float max, float val)
        => val > min && val < max;

    public static bool InRange(int min, int max, int val)
        => val > min && val < max;

    public static float Normalize(float value, float min, float max)
        => min == 0 ? (value / max) : (value - min) / (max - min);

    public static float ManhattanDistance(float x1, float y1, float x2, float y2)
    {
        return Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
    }
}
