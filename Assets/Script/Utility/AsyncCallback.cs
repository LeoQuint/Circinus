//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using System;
using System.Timers;
using UnityEngine;

public class AsyncCallback:  MonoBehaviour {

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

    private Timer m_TimeOutTimer;
    private Action m_OnTimeOut;

    public void Connect(Action OnConnect, Action OnTimeOut, double time )
    {
        if (m_TimeOutTimer != null)
        {
            m_TimeOutTimer.Stop();
            m_TimeOutTimer.Dispose();
            m_TimeOutTimer = null;
        }
        if (m_OnTimeOut != null)
        {
            m_OnTimeOut = null;
        }
        m_TimeOutTimer = new Timer(time);
        m_OnTimeOut += OnTimeOut;
        m_TimeOutTimer.Elapsed += OnTimeOutCallback;
        m_TimeOutTimer.Start();
    }

    private void OnTimeOutCallback(object source, ElapsedEventArgs e)
    {
        m_TimeOutTimer.Stop();
        m_TimeOutTimer.Dispose();
        m_TimeOutTimer = null;
        if (m_OnTimeOut != null)
        {
            m_OnTimeOut();
            m_OnTimeOut = null;
        }
    }

    #region Unity API
    #endregion

    #region Public API
    #endregion

    #region Protect
    #endregion

    #region Private
    #endregion
}
