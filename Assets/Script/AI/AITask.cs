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
    public long m_Id;
    public Hashtable m_Task;

    ////////////////////////////////
    ///			Protected		 ///
    ////////////////////////////////

    ////////////////////////////////
    ///			Private			 ///
    ////////////////////////////////



    #region Unity API
    #endregion

    #region Public API
    //constructors
    public AITask()
    {
    }

    public AITask (TaskType type, Hashtable task)
    {
        m_Type = type;
        m_Id = UniqueIdManager.instance.GetID();
        m_Task = task;
    }
    #endregion

    #region Protect
    #endregion

    #region Private
    #endregion
}
