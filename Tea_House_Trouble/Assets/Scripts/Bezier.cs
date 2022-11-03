using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Bezier
{
    public static Vector3 GetPoint(Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4, float t)
    {
        //return Vector3.Lerp(Vector3.Lerp(p1, p2, t), Vector3.Lerp(p2, p3, t), t);

        t = Mathf.Clamp01(t);
        float oneMinusT = 1f - t;
        return
            oneMinusT * oneMinusT * oneMinusT * p1 + 3f * oneMinusT * oneMinusT * t * p2
            + 3f * oneMinusT * t * t * p3 + t * t * t * p4;
    }

    public static Vector3 GetFirstDerivative(Vector3 p1, Vector3 p2, Vector3 p3, Vector3 p4, float t)
    {
        t = Mathf.Clamp01(t);
        float oneMinusT = 1f - t;
        return
            3f * oneMinusT * oneMinusT * (p2 - p1) + 6f * oneMinusT * t * (p3 - p1)
            + 3f * t * t * (p4 - p3);
    }
}