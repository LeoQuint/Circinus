//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalaxyViewShip : MonoBehaviour {

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
    private float m_OrbitSpeed = 100f;
    ////////////////////////////////
    ///			Public			 ///
    ////////////////////////////////

    ////////////////////////////////
    ///			Protected		 ///
    ////////////////////////////////
    [SerializeField][InEditorReadOnly]
    protected Transform m_TargetLocation;
    [SerializeField][InEditorReadOnly]
    protected Transform m_OrbitLocation;
    ////////////////////////////////
    ///			Private			 ///
    ////////////////////////////////
    private float m_LerpStartTime;
    private float m_WarpSpeed = 100f;
    private float m_WarpDuration = 2f;

    #region Unity API
    private void Update()
    {
        if (m_TargetLocation != null)
        {
            LerpTo();
        }
        if (m_OrbitLocation != null)
        {
            transform.RotateAround(m_OrbitLocation.position, Vector3.up, m_OrbitSpeed * Time.deltaTime);
        }
    }
    #endregion

    #region Public API
    public void AssignLocation(Transform target)
    {
        m_OrbitLocation = null;
        m_TargetLocation = target;
        m_LerpStartTime = Time.time + Mathf.Epsilon;
    }
    #endregion

    #region Protect
    #endregion

    #region Private
    private void LerpTo()
    {
        float ratio = (Time.time - m_LerpStartTime) / m_WarpDuration;
        transform.position = Vector3.Lerp(transform.position, m_TargetLocation.position, ratio);
    }
    #endregion
}
