//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

public class CharacterAI : MonoBehaviour {

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
    protected AITask m_CurrentTask;
    protected AITask m_GoToTask;

    protected object m_Target;
    ////////////////////////////////
    ///			Private			 ///
    ////////////////////////////////
    private Action m_TaskCallback;


    #region Unity API
    #endregion

    #region Public API
    public void GetTask()
    {
        m_CurrentTask = AITaskManager.instance.CheckForTask();

        if (m_CurrentTask != null)
        {
            SetDestination();
            switch (m_CurrentTask.m_Type)
            {
                case AITask.TaskType.Wait:
                    break;
                case AITask.TaskType.GoTo:
                    break;
                case AITask.TaskType.Pilot:
                    break;
                case AITask.TaskType.FireFight:
                    break;
                case AITask.TaskType.Repair:
                    SetRepairTask();
                    break;
                case AITask.TaskType.Shield:
                    break;
                case AITask.TaskType.Weapons:
                    break;
                case AITask.TaskType.Fight:
                    break;
                default:
                    break;
            }
        }
    }
    #endregion

    #region Protect
    #endregion

    #region Private
    private void SetDestination()
    {

    }

    private void SetRepairTask()
    {
        m_Target = m_CurrentTask.m_Task["target"];
       // m_TaskCallback += m_Target.GetType().GetMethod(m_CurrentTask.m_Task["callback"] as string)
     
    }

    private void OnTaskCompleted()
    {

    }
    #endregion
}
