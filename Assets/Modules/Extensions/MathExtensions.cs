//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathExtensions {

    public static Vector3 RotatePoint(this Vector3 toRotate, Vector3 rotateAround, float angleInRad)
    {

        float sin = Mathf.Sin(angleInRad);
        float cos = Mathf.Cos(angleInRad);

        // Translate point back to origin
        toRotate.x -= rotateAround.x;
        toRotate.y -= rotateAround.y;

        // Rotate point
        float xnew = toRotate.x * cos - toRotate.y * sin;
        float ynew = toRotate.x * sin + toRotate.y * cos;

        // Translate point back
        Vector3 newPoint = new Vector3(xnew + rotateAround.x, ynew + rotateAround.y);
        return newPoint;
    }

    public static Vector3 GetOffset(this Vector3 a, Vector2 b)
    {
        return new Vector3(b.x - a.x, b.y - a.y, a.z);
    }

    public static Vector2Int ToInt(this Vector2 vector)
    {
        return new Vector2Int((int)vector.x, (int)vector.y);
    } 
}
