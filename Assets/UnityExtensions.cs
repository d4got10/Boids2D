using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VectorExtensions
{
    public static Vector2 Rotate(this Vector2 vector, float angleInDegrees)
    {
        angleInDegrees *= Mathf.Deg2Rad;

        float prevAngle = Mathf.Atan2(vector.y, vector.x);
        float magnitude = vector.magnitude;
        return new Vector2(magnitude * Mathf.Cos(prevAngle + angleInDegrees), 
                           magnitude * Mathf.Sin(prevAngle + angleInDegrees));
    }
}
