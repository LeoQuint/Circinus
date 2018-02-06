//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode]
public class BezierCurve : MonoBehaviour {

    ////////////////////////////////
    ///			Constants		 ///
    ////////////////////////////////

    ////////////////////////////////
    ///			Statics			 ///
    ////////////////////////////////

    ////////////////////////////////
    ///	  Serialized In Editor	 ///
    ////////////////////////////////
    [SerializeField]
    protected Color _CurveColor = Color.red;
    [SerializeField]
    protected List<Vector3> _Points = new List<Vector3>() { Vector3.zero, Vector3.up , new Vector3(1f,1f,0f) , Vector3.right };
    [SerializeField]
    protected float _Width = 5f;
    [SerializeField]
    private float _HandleSize = 0.5f;
    ////////////////////////////////
    ///			Public			 ///
    ////////////////////////////////

    ////////////////////////////////
    ///			Protected		 ///
    ////////////////////////////////

    ////////////////////////////////
    ///			Private			 ///
    ////////////////////////////////

    ///proterties
    public List<Vector3> Points
    {
        get { return _Points;  }
        set { _Points = value; }
    }

    public float Width
    {
        get { return _Width; }
    }

    public Color CurveColor
    {
        get { return _CurveColor; }
    }

    public float HandleSize
    {
        get { return _HandleSize; }
    }
    #region Unity API
    protected void Update()
    {
        
    }    

    protected void OnDrawGizmosSelected()
    {
        
    }
    #endregion

    #region Public API
    #endregion

    #region Protect
    [ContextMenu("PreCalculatePoints")]
    protected void PreCalculatePoints()
    {
        //todo precalculate x points for performance.
    }
    #endregion

    #region Private
    Vector3 GetPointOnCurve(float t)
    {
        float u = 1f - t;
        float t2 = t * t;
        float u2 = u * u;
        float u3 = u2 * u;
        float t3 = t2 * t;

        Vector3 point = (u3) * _Points[0] + (3f * u2 * t) * _Points[1] + (3f * u * t2) * _Points[2] + (t3) * _Points[3];
        return point;
    }
    #endregion
}

[CustomEditor(typeof(BezierCurve))]
public class EditorBezierDrawer : Editor
{
    protected void OnSceneGUI()
    {
        BezierCurve curve = target as BezierCurve;
       
        Handles.DrawBezier(curve.Points[0], curve.Points[3], curve.Points[1], curve.Points[2], curve.CurveColor, null, curve.Width);

        EditorGUI.BeginChangeCheck();
        Vector3 newTargetPosition1 = Handles.FreeMoveHandle(curve.Points[0], Quaternion.identity, curve.HandleSize, Vector3.zero, Handles.RectangleHandleCap);
        Vector3 newTargetPosition2 = Handles.FreeMoveHandle(curve.Points[1], Quaternion.identity, curve.HandleSize, Vector3.zero, Handles.RectangleHandleCap);
        Vector3 newTargetPosition3 = Handles.FreeMoveHandle(curve.Points[2], Quaternion.identity, curve.HandleSize, Vector3.zero, Handles.RectangleHandleCap);
        Vector3 newTargetPosition4 = Handles.FreeMoveHandle(curve.Points[3], Quaternion.identity, curve.HandleSize, Vector3.zero, Handles.RectangleHandleCap);

        Handles.DrawLine(curve.Points[0], curve.Points[1]);
        Handles.DrawLine(curve.Points[2], curve.Points[3]);

        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Change Bezier curve.");
            curve.Points[0] = newTargetPosition1;
            curve.Points[1] = newTargetPosition2;
            curve.Points[2] = newTargetPosition3;
            curve.Points[3] = newTargetPosition4;
        }
    }
}