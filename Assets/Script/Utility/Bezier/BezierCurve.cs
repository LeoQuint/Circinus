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
    private const int POINTS_PER_BEZIER = 50;
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
    [SerializeField]
    private bool _ForceSmoothPath = true;
    [SerializeField]
    [InEditorReadOnly]
    private List<Vector3> m_Path = new List<Vector3>();
    [SerializeField]
    [InEditorReadOnly]
    private float m_TotalLength = -1f;
    [SerializeField]
    [InEditorReadOnly]
    private List<float> m_Lengths = new List<float>();
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

    public float Length
    {
        get
        {
            if (m_TotalLength == -1f)
            {
                console.logWarning("Bezier length not calculated. Use PreCalculatePoints to calculate.");
            }
            return m_TotalLength;
        }
    }
    #region Unity API
    #endregion

    #region Public API
    /// <summary>
    /// Add an extra curve.
    /// </summary>
    public void Extend()
    {
        _Points.Add( /*Rotate the first point so its handle is 180 degree from the opposite one.*/
            new Vector3(_Points[_Points.Count - 2].x, 
                        _Points[_Points.Count - 2].y, 
                        _Points[_Points.Count - 2].z)
                        .RotatePoint(_Points[_Points.Count - 1], Mathf.PI/*180Degrees*/));
        _Points.Add(new Vector3(_Points[_Points.Count - 1].x + 1f, _Points[_Points.Count - 1].y + 1f, _Points[_Points.Count - 1].z));
        _Points.Add(new Vector3(_Points[_Points.Count - 1].x + 1f, _Points[_Points.Count - 1].y + 1f, _Points[_Points.Count - 1].z));
        ++m_NumberOfBezierInPath;
        SceneView.RepaintAll();
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
            SceneView.RepaintAll();
        }
    }

    [ContextMenu("PreCalculatePoints")]
    public void PreCalculatePoints()
    {
        m_Path.Clear();
        m_Path.Capacity = m_NumberOfBezierInPath * POINTS_PER_BEZIER;
        m_Lengths.Clear();
        m_Lengths.Capacity = m_NumberOfBezierInPath;
        m_TotalLength = 0f;
        for (int i = 0; i < m_NumberOfBezierInPath; ++i)
        {
            for (int j = 0; j <= POINTS_PER_BEZIER; ++j)
            {
                m_Path.Add(GetPointOnCurve(((float)j / (float)POINTS_PER_BEZIER), i));
            }
            //Calculate the length of each bezier.
            float length = CalculateBezierLength(i);
            m_Lengths.Add(length);
            m_TotalLength += length;
        }        
    }
    #endregion

    #region Protect

    #endregion

    #region Private
    private Vector3 GetPointOnCurve(float t, int bezier = 0)
    {
        float u = 1f - t;
        float t2 = t * t;
        float u2 = u * u;
        float u3 = u2 * u;
        float t3 = t2 * t;

        Vector3 point = (u3) * _Points[0 + (bezier * 3)] +
                        (3f * u2 * t) * _Points[1 + (bezier * 3)] +
                        (3f * u * t2) * _Points[2 + (bezier * 3)] +
                        (t3) * _Points[3 + (bezier * 3)];
        return point;
    }

    private float CalculateBezierLength(int bezier = 0)
    {
        float length = 0f;
        for (int i = 0; i < POINTS_PER_BEZIER; ++i)
        {
            length += Vector3.Distance(m_Path[i + (bezier * POINTS_PER_BEZIER)], m_Path[i + 1 + (bezier * POINTS_PER_BEZIER)]);
        }
        return length;
    }
    #endregion
}

[CustomEditor(typeof(BezierCurve))]
public class EditorBezierDrawer : Editor
{
    private Vector3 controlPointDelta = Vector3.zero;
    private Vector3 tangentDirection = Vector3.zero;

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
            if (i % 3 == 0)//Control points
            {
                newPositions[i] = Handles.FreeMoveHandle(curve.Points[i], Quaternion.identity, curve.HandleSize, Vector3.zero, Handles.RectangleHandleCap);
                controlPointDelta = newPositions[i] - curve.Points[i];
                if (i > 0)
                {
                    newPositions[i - 1] += controlPointDelta;
                }
                if (newPositions.Count > i + 2)
                {
                    newPositions[i + 1] += controlPointDelta;
                    curve.Points[i + 1] = newPositions[i + 1];
                }
            }
            else//tangents
            {
                if (i % 3 == 1 && i > 1)//after the control point
                {
                    newPositions[i] = Handles.FreeMoveHandle(curve.Points[i], Quaternion.identity, curve.HandleSize, Vector3.zero, Handles.RectangleHandleCap);
                    //Starting angle
                    tangentDirection = curve.Points[i]/*target*/ - curve.Points[i - 1]/*origin*/;
                    float initialAngle = Mathf.Atan2(tangentDirection.y, tangentDirection.x) - Mathf.Atan2(1f, 0f);
                    //End angle
                    tangentDirection = newPositions[i]/*target*/ - curve.Points[i-1]/*origin*/;
                    float newAngle = Mathf.Atan2(tangentDirection.y, tangentDirection.x) - Mathf.Atan2(1f,0f);

                    newPositions[i - 2] = newPositions[i - 2].RotatePoint(curve.Points[i-1], (newAngle - initialAngle));
                }
                else if (i % 3 == 2 && i + 2 < newPositions.Count)//before the control point
                {
                    newPositions[i] = Handles.FreeMoveHandle(curve.Points[i], Quaternion.identity, curve.HandleSize, Vector3.zero, Handles.RectangleHandleCap);
                    //Starting angle
                    tangentDirection = curve.Points[i]/*target*/ - curve.Points[i + 1]/*origin*/;
                    float initialAngle = Mathf.Atan2(tangentDirection.y, tangentDirection.x) - Mathf.Atan2(1f, 0f);
                    //End angle
                    tangentDirection = newPositions[i]/*target*/ - curve.Points[i + 1]/*origin*/;
                    float newAngle = Mathf.Atan2(tangentDirection.y, tangentDirection.x) - Mathf.Atan2(1f, 0f);

                    newPositions[i + 2] = newPositions[i + 2].RotatePoint(curve.Points[i + 1], (newAngle - initialAngle));
                }
                else
                {
                    newPositions[i] = Handles.FreeMoveHandle(curve.Points[i], Quaternion.identity, curve.HandleSize, Vector3.zero, Handles.RectangleHandleCap);
                }               
            }            
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
        if (GUILayout.Button("Pre Calculate"))
        {
            curve.PreCalculatePoints();
        }
    }    
}