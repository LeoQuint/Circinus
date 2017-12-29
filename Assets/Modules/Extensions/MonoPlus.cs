//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using UnityEngine;
using System.Threading;

public class MonoPlus : MonoBehaviour {

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
    protected bool m_HasInitializeThreads = false;
    
    ////////////////////////////////
    ///			Private			 ///
    ////////////////////////////////
    private Thread m_IndependentUpdateThread;
    private int m_UpdatePerSeconds = 60;
    private int m_UpdateSleepTime = 16;
    private float m_SecondsPerUpdates = 0.166667f;
    private bool m_IndependentUpdateRunning = false;

    /// <summary>
    /// Returns the sleep time in milliseconds
    /// </summary>
    public int UpdatesPerSeconds
    {
        get { return m_UpdateSleepTime; }
        set
        {
            if (value <= 0)
            {
                Debug.LogError("Cannot set Update to less than 1 per seconds. Use SecondsPerUpdates if the intention is > 1 sec per updates.");
                return;
            }
            m_UpdatePerSeconds = value;
            m_UpdateSleepTime = 1000 / m_UpdatePerSeconds;
        }
    }

    /// <summary>
    /// Returns the sleep time in milliseconds
    /// </summary>
    public int SecondsPerUpdates
    {
        get { return m_UpdateSleepTime; }
        set
        {
            if (value <= 0)
            {
                Debug.LogError("Cannot set Update 0 or negative values.");
                return;
            }
            m_SecondsPerUpdates = value;
            m_UpdateSleepTime = (int)(1000f * m_SecondsPerUpdates);
        }
    }

    #region Unity API
    /// <summary>
    /// Always call base.
    /// </summary>
    public virtual void Awake()
    {
        if (!m_HasInitializeThreads)
        {
            m_IndependentUpdateRunning = true;
            m_HasInitializeThreads = true;
            InitializeThreads();
        }
    }
    /// <summary>
    /// Always call base.
    /// </summary>
    public virtual void OnEnable()
    {
        if (!m_HasInitializeThreads)
        {
            m_IndependentUpdateRunning = true;
            m_HasInitializeThreads = true;
            InitializeThreads();
        }
    }
    /// <summary>
    /// Always call base.
    /// </summary>
    public virtual void OnDestroy()
    {
        m_IndependentUpdateRunning = false;
    }
    #endregion

    #region Public API
    /// <summary>
    /// Always call base.
    /// </summary>
    public virtual void IndependentUpdate()
    {        
        Thread.Sleep(m_UpdateSleepTime);
    }
    #endregion

    #region Protect
    #endregion

    #region Private
    private void InitializeThreads()
    {
        m_IndependentUpdateThread = new Thread(IndependentThread);
        m_IndependentUpdateThread.Start();
    }

    private void IndependentThread()
    {
        while (m_IndependentUpdateRunning)
        {
            IndependentUpdate();
        }
    }
    #endregion
}
