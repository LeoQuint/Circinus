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
        Wait = 1,
        GoTo = 2,

        Pilot = 10,

        FireFight = 20,
        Repair = 30,
        
        Shield = 100,
        Weapons = 150,

        Fight = 200
    }

    ////////////////////////////////
    ///			Public			 ///
    ////////////////////////////////

    public TaskType m_Type;    
    public Dictionary<string,object> m_Task;
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
    public long Id
    {
        get { return m_Id; }
    }
    #endregion

    #region Public API
    //constructors
    public AITask()
    {
        m_Id = UniqueIdManager.instance.GetID();
    }

    public AITask (TaskType type, Dictionary<string, object> task)
    {
        m_Id = UniqueIdManager.instance.GetID();
        m_Type = type;
        m_Task = task;      
    }
    #endregion
}
