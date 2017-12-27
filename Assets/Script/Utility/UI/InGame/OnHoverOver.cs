//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Collider))]
public class OnHoverOver : MonoBehaviour {

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
    protected Action m_OnMouseEnter;
    protected Action m_OnMouseOver;
    protected Action m_OnMouseExit;
    ////////////////////////////////
    ///			Private			 ///
    ////////////////////////////////


    ///Properties
    public Action OnMouseEnterDelegate
    {
        get { return m_OnMouseEnter; }
        set { m_OnMouseEnter = value; }
    }

    public Action OnMouseOverDelegate
    {
        get { return m_OnMouseOver; }
        set { m_OnMouseOver = value; }
    }

    public Action OnMouseExitDelegate
    {
        get { return m_OnMouseExit; }
        set { m_OnMouseExit = value; }
    }

    #region Unity API
    protected void OnMouseEnter()
    {
        if (m_OnMouseEnter != null)
        {
            m_OnMouseEnter();
        }
    }
    protected void OnMouseOver()
    {
        if (m_OnMouseOver != null)
        {
            m_OnMouseOver();
        }
    }
    protected void OnMouseExit()
    {
        if (m_OnMouseExit != null)
        {
            m_OnMouseExit();
        }
    }
    #endregion

    #region Public API
    #endregion

    #region Protect
    #endregion

    #region Private
    #endregion
}
