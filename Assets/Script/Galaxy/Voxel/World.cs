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

    private Chunk[,] m_Chunks = new Chunk[10,10];

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
                            for (int x = 0; x < 10; ++x)
                            {
                                for (int z = 0; z < 10; ++z)
                                {
                                    m_Chunks[x,z] = new Chunk(x, z);
                                    m_Chunks[x,z].Start();
                                }
                            }                            

                            m_RanOnce = true;
                        }
                        for (int i = 0; i < m_Chunks.GetLength(0); ++i)
                        {
                            for (int j = 0; j < m_Chunks.GetLength(1); ++j)
                            {
                                m_Chunks[i, j].Update();
                            }
                        }                        
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
        
        for (int i = 0; i < m_Chunks.GetLength(0); ++i)
        {
            for (int j = 0; j < m_Chunks.GetLength(1); ++j)
            {
                if (m_Chunks[i, j] != null)
                {
                    m_Chunks[i, j].OnUnityUpdate();
                }
            }
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
