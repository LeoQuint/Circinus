//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class AreaDrawer : MonoBehaviour {

    ////////////////////////////////
    ///			Constants		 ///
    ////////////////////////////////

    ////////////////////////////////
    ///			Statics			 ///
    ////////////////////////////////

    ////////////////////////////////
    ///	  Serialized In Editor	 ///
    ////////////////////////////////

    ////////////////////////////////
    ///			Public			 ///
    ////////////////////////////////

    ////////////////////////////////
    ///			Protected		 ///
    ////////////////////////////////

    ////////////////////////////////
    ///			Private			 ///
    ////////////////////////////////
    private LineRenderer m_LineRenderer;
    private List<Vector3> m_Points;

    /// <summary>
    /// Properties
    /// </summary>
    public List<Vector3> Points
    {
        get { return m_Points; }
        set
        {
            m_Points = value;
            if (m_Points != null && m_Points.Count > 1)
            {
                Draw();
            }
        }
    }

    #region Unity API
    #endregion

    #region Public API
    public void Init()
    {
        m_LineRenderer = GetComponent<LineRenderer>();
    }

    public void Draw()
    {
        m_LineRenderer.positionCount = m_Points.Count;
        m_LineRenderer.SetPositions(m_Points.ToArray());
    }
    #endregion

    #region Protect
    #endregion

    #region Private
    #endregion
}
