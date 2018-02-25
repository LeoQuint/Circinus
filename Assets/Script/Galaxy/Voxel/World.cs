//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class World : ILoopable
{

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

    
    private static World instance = new World();
    private Thread m_WorldThread;
    private bool m_IsRunning = false;

    private bool m_RanOnce = false;

    private Chunk m_FirstChunk;

    #region Unity API
    public void OnApplicationQuit()
    {
        m_IsRunning = false;
        Logger.Log("Stopping world thread.");
    }

    public void OnDestroy()
    {
        m_IsRunning = false;
    }

    public void Start()
    {
        m_IsRunning = true;
        m_WorldThread = new Thread(
            () => {
                Logger.Log("Init world thread.");
                while (m_IsRunning)
                {
                    try
                    {
                        if (!m_RanOnce)
                        {
                            m_FirstChunk = new Chunk(0, 0);
                            ((ITickable)m_FirstChunk).Start();

                            m_RanOnce = true;
                        }
                         ((ITickable)m_FirstChunk).Update();
                    }
                    catch (System.Exception e)
                    {
                        Logger.Log(e);
                    }
                }

                Logger.MainLog.Update();
            });
        m_WorldThread.Start();
    }

    public void Update()
    {
        if (m_FirstChunk != null)
        {
            ((ITickable)m_FirstChunk).OnUnityUpdate();
        }
    }
    #endregion

    #region Public API
    public static void Initialize()
    {
        MainLoopable.Instance.RegisterLoop(instance);
    }

    
    #endregion

    #region Protect
    #endregion

    #region Private
    #endregion

}
