﻿//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : Subject {

    ////////////////////////////////
    ///			Constants		 ///
    ////////////////////////////////

    ////////////////////////////////
    ///			Statics			 ///
    ////////////////////////////////
    public static CameraManager instance;
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
    private Camera m_CurrentCamera;
    //properties
    public Camera CurrentCamera
    {
        //stub
        set
        {
            if (m_CurrentCamera != value)
            {
                m_CurrentCamera = value;
                Notify(m_CurrentCamera);
            }            
        }
        get
        {
            if (m_CurrentCamera == null)
            {
                Init();
            }
            return m_CurrentCamera;
        }
    }

    #region Unity API
    protected void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            m_CurrentCamera = Camera.main;
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    #endregion

    #region Public API
    public void Init()
    {
        m_CurrentCamera = Camera.main;
    }
    #endregion

    #region Protect
    #endregion

    #region Private
    #endregion
}
