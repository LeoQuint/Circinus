//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainLoopable : ILoopable
{

    private static MainLoopable instance;


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
    private List<ILoopable> m_RegisteredLoopes = new List<ILoopable>();
    //Properties

    public static MainLoopable Instance
    {
        get { return instance; }
    }

    #region Unity API
    public void Start()
    {
        for (int i = 0; i < m_RegisteredLoopes.Count; ++i)
        {
            m_RegisteredLoopes[i].Start();
        }
    }

    public void Update()
    {
        for (int i = 0; i < m_RegisteredLoopes.Count; ++i)
        {
            m_RegisteredLoopes[i].Update();
        }
    }

    public void OnApplicationQuit()
    {
        //throw new NotImplementedException();
    }
    #endregion

    #region Public API
    public static void Initialize()
    {
        instance = new MainLoopable();
        Logger.Initialize();
    }

    public void RegisterLoop(ILoopable loop)
    {
        m_RegisteredLoopes.Add(loop);
    }

    public void UnregisterLoop(ILoopable loop)
    {
        m_RegisteredLoopes.Remove(loop);
    }


    #endregion

    #region Protect
    #endregion

    #region Private
    #endregion

}
