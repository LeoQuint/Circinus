//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
#define DEBUG_TEST
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Diagnostics;

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
    private bool _UseLocalMatrix = true;
    [SerializeField]
    [InEditorReadOnly]
    private List<Vector3> m_Path = new List<Vector3>();
    [SerializeField]
    [InEditorReadOnly]
    protected int m_NumberOfBezierInPath = 1;
    [SerializeField]
    [InEditorReadOnly]
    private float m_TotalLength = -1f;
    [SerializeField]
    [InEditorReadOnly]
    private List<float> m_Lengths = new List<float>();
#if DEBUG_TEST
    Stopwatch m_StopWatch;
#endif
    ////////////////////////////////
    ///			Public			 ///
    ////////////////////////////////
    [HideInInspector]
    public bool IsLockedDisplay = false;
    ////////////////////////////////
    ///			Protected		 ///
    ////////////////////////////////

    ////////////////////////////////
    ///			Private			 ///
    ////////////////////////////////
    private bool _PreviewOn = false;
    ///proterties
    ///
    public bool UseLocalMatrix
    {
        get { return _UseLocalMatrix; }
    }

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
        set { m_NumberOfBezierInPath = value; }
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

    public bool ForceSmoothPath
    {
        get { return _ForceSmoothPath; }
    }

    public bool PreviewOn
    {
        get { return _PreviewOn; }
        set
        {
            if (_PreviewOn != value)
            {
                SceneView.RepaintAll();
            }
            _PreviewOn = value;
        }
    }
    #region Unity API
#if DEBUG_TEST
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            List<float> testList = new List<float>();
            for (int i = 0; i < 1000f; ++i)
            {
                testList.Add(Random.Range(0f,1f));
            }
            m_StopWatch = new Stopwatch();
            m_StopWatch.Start();
            for (int t = 0; t < 1000; ++t)
            {            
                for (int i = 0; i < testList.Count; ++i)
                {
                    GetPointOnPath(testList[i]);
                }
            }
            console.logStatus(m_StopWatch.Elapsed);
            m_StopWatch.Stop();
            m_StopWatch.Reset();
            m_StopWatch.Start();
            for (int t = 0; t < 1000; ++t)
            {
                for (int i = 0; i < testList.Count; ++i)
                {
                    GetPointOnPathStrict(testList[i]);
                }
            }
            console.logStatus(m_StopWatch.Elapsed);
            m_StopWatch.Stop();
        }
    }
#endif
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

    public List<Vector3> InsertControlPoint(Vector3 screenPoint)
    {
        PreCalculatePoints();
        screenPoint.z = 0;
        List<Vector3> path = new List<Vector3>(_Points);
        Vector4 results = GetNearestPoint(screenPoint);        
        Vector3 newControlPoint = new Vector3(results.x, results.y, results.z);
        float ratio = GetRatio(newControlPoint);
        path.Insert((int)results.w, newControlPoint);//control point
        if (ratio < 0.975f)
        {
            Vector3 point2 = GetPointOnPathStrict(ratio + 0.02f);
            Vector3 tangent1 = newControlPoint - point2;
            tangent1 = new Vector3(tangent1.y, -tangent1.x, tangent1.z);
            path.Insert((int)results.w, newControlPoint + (tangent1.normalized * HandleSize * 5f));
            path.Insert((int)results.w + 2, newControlPoint + (-tangent1.normalized * HandleSize * 5f));
        }
        else
        {
            Vector3 point2 = GetPointOnPathStrict(ratio - 0.02f);
            Vector3 tangent1 = newControlPoint - point2;
            tangent1 = new Vector3(tangent1.y, -tangent1.x, tangent1.z);
            path.Insert((int)results.w, newControlPoint +( tangent1.normalized * HandleSize * 5f));
            path.Insert((int)results.w + 2, newControlPoint +( - tangent1.normalized * HandleSize * 5f));
        }
        return path;
    }

    public List<Vector3> RemoveControlPoint(Vector3 screenPoint)
    {
        PreCalculatePoints();
        screenPoint.z = 0;
        List<Vector3> path = new List<Vector3>(_Points);
        int nearestControlPoint = GetNearestControlPoint(screenPoint);
        if (nearestControlPoint == -1 || path.Count <= 4)
        {
            return path;
        }

        if (nearestControlPoint == 0)
        {
            path.RemoveAt(0);
            path.RemoveAt(0);
            path.RemoveAt(0);
        }
        else if (nearestControlPoint + 1 == path.Count)
        {
            path.RemoveAt(path.Count - 1);
            path.RemoveAt(path.Count - 1);
            path.RemoveAt(path.Count - 1);
        }
        else
        {
            path.RemoveAt(nearestControlPoint - 1);
            path.RemoveAt(nearestControlPoint - 1);
            path.RemoveAt(nearestControlPoint - 1);
        }

        return path;
    }
    /// <summary>
    /// Remove a curve
    /// </summary>
    public void Shorten()
    {
        if (m_NumberOfBezierInPath > 1)
        {
            _Points.RemoveAt(_Points.Count - 1);
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
            for (int j = 0; j < POINTS_PER_BEZIER; ++j)
            {
                m_Path.Add(GetPointOnCurve(((float)j / (float)POINTS_PER_BEZIER), i));
                if (i + 1 == m_NumberOfBezierInPath && j + 1 == POINTS_PER_BEZIER)//Add an extra pts to cap the curve.
                {
                    m_Path.Add(GetPointOnCurve(((float)(j+1) / (float)POINTS_PER_BEZIER), i));
                }                
            }
            //Calculate the length of each bezier.
            float length = CalculateBezierLength(i);
            m_Lengths.Add(length);
            m_TotalLength += length;
        }        
    }
    /// <summary>
    /// Vector4 (w) is used to add the point index where to insert new controls.
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public Vector4 GetNearestPoint(Vector3 position)
    {
        float nearest = float.MaxValue;
        int index = -1;
        int pointIndex = 0;
        for (int i = 0; i < m_NumberOfBezierInPath; ++i)
        {
            for (int j = 0; j < POINTS_PER_BEZIER; ++j)
            {
                Vector3 point = m_Path[(i * POINTS_PER_BEZIER) + j];
                if (_UseLocalMatrix)
                {
                    point = transform.TransformPoint(point); ;
                }
                float distance = Vector3.Distance(position, point);
                if (distance < nearest)
                {
                    nearest = distance;
                    index =( i * POINTS_PER_BEZIER) +j;
                    pointIndex = i * 3;
                }
            }            
        }
        if (index != -1)
        {
            return new Vector4( m_Path[index].x, m_Path[index].y, m_Path[index].z, pointIndex + 2);
        }
        return Vector4.one;

    }

    public int GetNearestControlPoint(Vector3 position)
    {
        float nearest = float.MaxValue;
        int index = -1;
        for (int i = 0; i < _Points.Count; ++i)
        {
            if (i % 3 == 0)//control point
            {                
                float distance = Vector3.Distance(position, _Points[i]);
                if (distance < nearest)
                {
                    nearest = distance;
                    index = i;
                }
            }
        }
       return index;
    }

    public float GetRatio(Vector3 position)
    {
        float nearest = float.MaxValue;
        float ratio = -1;
        for (int i = 0; i < m_NumberOfBezierInPath; ++i)
        {
            for (int j = 0; j < POINTS_PER_BEZIER; ++j)
            {
                float distance = Vector3.Distance(position, m_Path[(i * POINTS_PER_BEZIER) + j]);
                if (distance < nearest)
                {
                    nearest = distance;
                    ratio = ((float)((i * POINTS_PER_BEZIER) + j)/(float)(m_NumberOfBezierInPath * POINTS_PER_BEZIER));
                }
            }
        }
        if (ratio != -1)
        {
            return ratio;
        }
        return 0f;
    }

    public Vector3 GetNormalOnPath(float ratio)
    {
        Vector3 point1 = GetPointOnPath(ratio);
        Vector3 point2 = Vector3.zero;
        if (ratio < 0.975f)
        {
            point2 = GetPointOnPath(ratio + 0.025f);
        }
        else
        {
            point2 = GetPointOnPath(ratio - 0.025f);
        }

        Vector3 direction = point1 - point2;

        return new Vector3(direction.y, -direction.x, direction.z);
    }
    /// <summary>
    /// Get the nearest point on path
    /// </summary>
    /// <param name="ratio"></param>
    /// <returns></returns>
    public Vector3 GetPointOnPath(float ratio)
    {
        float targetDistance = m_TotalLength * ratio;
        float currentDistance = 0f;
        for (int i = 0; i < m_NumberOfBezierInPath; ++i)
        {
            if (currentDistance + m_Lengths[i] >= targetDistance)
            {
                return m_Path[(POINTS_PER_BEZIER * i) + (int)((targetDistance - currentDistance) / m_Lengths[i] * (float)POINTS_PER_BEZIER)];
            }
            currentDistance += m_Lengths[i];
        }

        return m_Path[m_Path.Count - 1];
    }
    /// <summary>
    /// Calculate the exact point on path
    /// </summary>
    /// <param name="ratio"></param>
    /// <returns></returns>
    public Vector3 GetPointOnPathStrict(float ratio)
    {
        Vector3 point = Vector3.zero;
        float targetDistance = m_TotalLength * ratio;
        float currentDistance = 0f;
        for (int i = 0; i < m_NumberOfBezierInPath; ++i)
        {
            if (currentDistance + m_Lengths[i] >= targetDistance)
            {
                float distanceOnCurve = targetDistance - currentDistance;
                return GetPointOnCurve(distanceOnCurve / m_Lengths[i], i);
            }
            currentDistance += m_Lengths[i];
        }

        return point;
    }
#endregion

#region Protect

#endregion

#region Private
    private Vector3 GetPointOnCurve(float ratio, int bezier = 0)
    {
        float u = 1f - ratio;
        float t2 = ratio * ratio;
        float u2 = u * u;
        float u3 = u2 * u;
        float t3 = t2 * ratio;

        Vector3 point = (u3) * _Points[0 + (bezier * 3)] +
                        (3f * u2 * ratio) * _Points[1 + (bezier * 3)] +
                        (3f * u * t2) * _Points[2 + (bezier * 3)] +
                        (t3) * _Points[3 + (bezier * 3)];
        return point;
    }

    private float CalculateBezierLength(int bezier = 0)
    {
        float length = 0f;
        for (int i = 0; i < POINTS_PER_BEZIER-1; ++i)
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
    private Color defaultColor = Color.grey;
    private Color previewColor = new Color(0.2f, 0.2f, 0.2f);
    public static List<BezierCurve> ActiveBezier;
    private BezierCurve curve;
    private bool doubleClickReset = true;
    private Color UNSELECTED_COLOR = Color.gray;
    private bool m_IsSelected = true;

    protected void Awake()
    {
        if (ActiveBezier == null)
        {
            ActiveBezier = new List<BezierCurve>();
        }
    }

    protected void OnEnable()
    {
        defaultColor = GUI.color;
    }

    public void AddToAlwaysRender()
    {
        //SceneView.onSceneGUIDelegate += HandleOnSceneFunc;
        if (SceneGUI.InEditorActiveBeziers != null && !SceneGUI.InEditorActiveBeziers.Contains(this))
        {
            curve.IsLockedDisplay = true;
            SceneGUI.InEditorActiveBeziers.Add(this);
        }
    }


    public void RemoveFromAlwaysRender()
    {
        //SceneView.onSceneGUIDelegate += HandleOnSceneFunc;

        if (SceneGUI.InEditorActiveBeziers != null)
        {
            curve.IsLockedDisplay = false;
            for (int i = 0; i < SceneGUI.InEditorActiveBeziers.Count; ++i)
            {
                if (SceneGUI.InEditorActiveBeziers[i].curve == curve)
                {
                    SceneGUI.InEditorActiveBeziers.RemoveAt(i);
                    return;
                }
            }
        }
    }

    public void OnSceneGUI()
    {
        curve = target as BezierCurve;
        m_IsSelected = Selection.activeGameObject == curve.gameObject;

        if (curve.UseLocalMatrix)
        {
            Handles.matrix = curve.transform.localToWorldMatrix;
        }

        if (ActiveBezier != null && !ActiveBezier.Contains(this.curve))
        {
            ActiveBezier.Add(curve);
        }
        List<Vector3> newPositions = new List<Vector3>(curve.Points);
        Event e = Event.current;
        if (e.clickCount < 2)
        {
            doubleClickReset = true;
        }
        if (m_IsSelected && (e.command || e.control) && e.shift && e.clickCount >= 2 && doubleClickReset && curve.Points.Count > 4)
        {
            doubleClickReset = false;
            e.clickCount = 0;
            //delete nearest bezier
            EditorGUI.BeginChangeCheck();
            //save the new bezier inserted to undo.       
            List<Vector3> newPath = curve.RemoveControlPoint(HandleUtility.GUIPointToWorldRay(Event.current.mousePosition).origin);
            EditorGUI.EndChangeCheck();

            Undo.RecordObject(target, "Removed Bezier Curve");
            curve.Points = newPath;
            curve.NumberOfBezier--;
            SceneView.RepaintAll();
            return;
        }
        else if (m_IsSelected && (e.command || e.control) && e.clickCount >= 2 && doubleClickReset)
        {
            doubleClickReset = false;
            e.clickCount = 0;
            EditorGUI.BeginChangeCheck();
            //save the new bezier inserted to undo.       
            List<Vector3> newPath = curve.InsertControlPoint(HandleUtility.GUIPointToWorldRay(Event.current.mousePosition).origin);
            EditorGUI.EndChangeCheck();

            Undo.RecordObject(target, "Added Bezier Curve");
            curve.Points = newPath;
            curve.NumberOfBezier++;
            SceneView.RepaintAll();
            return;
        }
        int curveFrom = 0;
        int curveTo = 1;

        for (int i = 0; i < curve.NumberOfBezier; ++i)
        {
            Handles.DrawBezier(curve.Points[0 + (i * 3)], curve.Points[3 + (i * 3)], curve.Points[1 + (i * 3)], curve.Points[2 + (i * 3)], m_IsSelected ? curve.CurveColor : UNSELECTED_COLOR, null, curve.Width);
            if (!curve.PreviewOn)
            {
                Handles.DrawLine(curve.Points[curveFrom], curve.Points[curveTo]);
                curveFrom += 2;
                curveTo += 2;
                Handles.DrawLine(curve.Points[curveFrom], curve.Points[curveTo]);
                ++curveFrom;
                ++curveTo;
            }
        }
        //No editing in preview mode
        if (!curve.PreviewOn)
        {
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
                    if (curve.ForceSmoothPath)
                    {
                        if (i % 3 == 1 && i > 1)//after the control point
                        {
                            newPositions[i] = Handles.FreeMoveHandle(curve.Points[i], Quaternion.identity, curve.HandleSize, Vector3.zero, Handles.RectangleHandleCap);
                            //Starting angle
                            tangentDirection = curve.Points[i]/*target*/ - curve.Points[i - 1]/*origin*/;
                            float initialAngle = Mathf.Atan2(tangentDirection.y, tangentDirection.x) - Mathf.Atan2(1f, 0f);
                            //End angle
                            tangentDirection = newPositions[i]/*target*/ - curve.Points[i - 1]/*origin*/;
                            float newAngle = Mathf.Atan2(tangentDirection.y, tangentDirection.x) - Mathf.Atan2(1f, 0f);

                            newPositions[i - 2] = newPositions[i - 2].RotatePoint(curve.Points[i - 1], (newAngle - initialAngle));
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
    }

    public override void OnInspectorGUI()
    {


        BezierCurve curve = target as BezierCurve;

        if (curve.PreviewOn)
        {
            GUI.color = previewColor;
        }
        DrawDefaultInspector();


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

        if (curve.PreviewOn)
        {
            GUI.color = Color.red;
        }
        if (GUILayout.Button(curve.PreviewOn ? "Preview Mode" : "Edit Mode"))
        {
            curve.PreviewOn = !curve.PreviewOn;
        }

        if (curve.IsLockedDisplay)
        {
            GUI.color = Color.red;
            if (GUILayout.Button("Unlock Display"))
            {
                RemoveFromAlwaysRender();
            }
        }
        else
        {
            if (GUILayout.Button("Lock Display"))
            {
                AddToAlwaysRender();
            }
        }

        if (curve.PreviewOn)
        {
            GUI.color = defaultColor;
        }
    }
}

public class SceneGUI : EditorWindow
{
    public static List<EditorBezierDrawer> InEditorActiveBeziers = new List<EditorBezierDrawer>();
    public static bool ShowLockedBezier = true;

    [MenuItem("Bezier/Toggle Always ON %g")]
    public static void ToggleBezier()
    {
        if (ShowLockedBezier)
        {
            ShowLockedBezier = false;
            SceneView.onSceneGUIDelegate -= OnScene;
        }
        else
        {
            SceneView.onSceneGUIDelegate += OnScene;
            ShowLockedBezier = true;
        }
        
    }

    private static void OnScene(SceneView sceneview)
    {

        for (int i = 0; i < InEditorActiveBeziers.Count; ++i)
        {
            InEditorActiveBeziers[i].OnSceneGUI();
        }
    }
}
