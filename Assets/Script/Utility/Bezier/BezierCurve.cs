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
    protected int m_NumberOfBezierInPath = 1;
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

    public int NumberOfBezier
    {
        get { return m_NumberOfBezierInPath;  }
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
    /// <summary>
    /// Add an extra curve.
    /// </summary>
    public void Extend()
    {
        _Points.Add(new Vector3(_Points[_Points.Count - 1].x + 1f, _Points[_Points.Count - 1].y + 1f, _Points[_Points.Count - 1].z));
        _Points.Add(new Vector3(_Points[_Points.Count - 1].x + 1f, _Points[_Points.Count - 1].y + 1f, _Points[_Points.Count - 1].z));
        _Points.Add(new Vector3(_Points[_Points.Count - 1].x + 1f, _Points[_Points.Count - 1].y + 1f, _Points[_Points.Count - 1].z));
        ++m_NumberOfBezierInPath;
    }
    /// <summary>
    /// Remove a curve
    /// </summary>
    public void Shorten()
    {
        if (m_NumberOfBezierInPath > 1)
        {
            _Points.RemoveAt(_Points.Count- 1);
            _Points.RemoveAt(_Points.Count - 1);
            _Points.RemoveAt(_Points.Count - 1);
            --m_NumberOfBezierInPath;
        }
    }
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
        List<Vector3> newPositions = new List<Vector3>(curve.Points);

        int curveFrom = 0;
        int curveTo = 1;

        for (int i = 0; i < curve.NumberOfBezier; ++i)
        {
            Handles.DrawBezier(curve.Points[0 + (i * 3)], curve.Points[3 + (i * 3)], curve.Points[1 + (i * 3)], curve.Points[2 + (i * 3)], curve.CurveColor, null, curve.Width);
            Handles.DrawLine(curve.Points[curveFrom], curve.Points[curveTo]);
            curveFrom += 2;
            curveTo += 2;
            Handles.DrawLine(curve.Points[curveFrom], curve.Points[curveTo]);
            ++curveFrom;
            ++curveTo;
        }

        EditorGUI.BeginChangeCheck();

        for (int i = 0; i < curve.Points.Count; ++i)
        {
            newPositions[i] = Handles.FreeMoveHandle(curve.Points[i], Quaternion.identity, curve.HandleSize, Vector3.zero, Handles.RectangleHandleCap);
        }
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Change Bezier curve.");           
            for (int i = 0; i < curve.Points.Count; ++i)
            {
                curve.Points[i] = newPositions[i];
            }
        }
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        BezierCurve curve = target as BezierCurve;
        if (GUILayout.Button("Extend"))
        {
            curve.Extend();
        }
        if (GUILayout.Button("Shorten"))
        {
            curve.Shorten();
        }
    }
}