//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UniqueIdManager : MonoBehaviour {

    ////////////////////////////////
    ///			Constants		 ///
    ////////////////////////////////

    ////////////////////////////////
    ///			Statics			 ///
    ////////////////////////////////
    public static UniqueIdManager instance;
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
    private long m_LastID;


    #region Unity API
    protected void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            m_LastID = DateTime.Now.Ticks;
            DontDestroyOnLoad(gameObject);
        }
    }
    #endregion

    #region Public API

    public long GetID()
    {
        ++m_LastID;
        return m_LastID;
    }

    #endregion

    #region Protect
    #endregion

    #region Private
    #endregion
}
