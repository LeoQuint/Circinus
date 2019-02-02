//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
///Descriptions:
///Base class for purely visual elements that can animate.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AnimatedVisuals : MonoBehaviour {

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
    protected Animator m_Animator;
    ////////////////////////////////
    ///			Private			 ///
    ////////////////////////////////
   
    #region Unity API
    protected virtual void Awake()
    {
        //stub
    }

    protected virtual void Update()
    {
       
    }
    #endregion

    #region Public API
    public void SetFloat(string name, float val)
    {
        if (m_Animator != null)
        {
            m_Animator.SetFloat(name, val);
        }
        else
        {
            Debug.LogError("The animator on " + gameObject.name + " is NULL.");
        }
    }

    public void SetInteger(string name, int val)
    {
        if (m_Animator != null)
        {
            m_Animator.SetInteger(name, val);
        }
        else
        {
            Debug.LogError("The animator on " + gameObject.name + " is NULL.");
        }
    }

    public void SetBool(string name, bool val)
    {
        if (m_Animator != null)
        {
            m_Animator.SetBool(name, val);
        }
        else
        {
            Debug.LogError("The animator on " + gameObject.name + " is NULL.");
        }
    }

    public void Play(string stateName)
    {
        m_Animator.Play(stateName);        
    }
    #endregion

    #region Protect
    #endregion

    #region Private
    #endregion
}
