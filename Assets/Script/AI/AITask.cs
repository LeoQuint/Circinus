//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AITask {

    public enum TaskType
    {
        /// <summary>
        /// Parameters:
        /// "target:, object
        /// "callback", string (requires target)
        /// </summary>
        None = 0,
        Wait = 1,
        GoTo = 2,
       

        FireFight = 20,
        Repair = 30,

        Pilot = 100,
        Shield = 200,
        Weapons = 300,
        Engine = 400,

        Fight = 10000
    }

    ////////////////////////////////
    ///			Public			 ///
    ////////////////////////////////

    public TaskType m_Type;    
    public Dictionary<string,object> m_Parameters;
    //Callbacks
    public Action<Hashtable> m_OnTaskDone;
    ////////////////////////////////
    ///			Protected		 ///
    ////////////////////////////////
    protected long m_Id;
    ////////////////////////////////
    ///			Private			 ///
    ////////////////////////////////

    #region Properties
    public long ID
    {
        get { return m_Id; }
    }
    #endregion

    #region Public API
    //constructors
    public AITask()
    {
        m_Id = UniqueIdManager.Instance.GetID();
    }

    public AITask (TaskType type, Dictionary<string, object> task)
    {
        m_Id = UniqueIdManager.Instance.GetID();
        m_Type = type;
        m_Parameters = task;
    }
    #endregion
}
