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
        Wait,
        GoTo,

        Pilot,

        FireFight,
        Repair,
        
        Shield,
        Weapons,

        Fight
    }

    ////////////////////////////////
    ///			Public			 ///
    ////////////////////////////////

    public TaskType m_Type;    
    public Hashtable m_Task;
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

    public AITask (TaskType type, Hashtable task)
    {
        m_Id = UniqueIdManager.instance.GetID();
        m_Type = type;
        m_Task = task;      
    }
    #endregion
}
