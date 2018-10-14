//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

using TaskType = AITask.TaskType;

public class CharacterAI : Character, Observer {

    ////////////////////////////////
    ///			Constants		 ///
    ////////////////////////////////

    ////////////////////////////////
    ///			Statics			 ///
    ////////////////////////////////

    ////////////////////////////////
    ///	  Serialized In Editor	 ///
    ////////////////////////////////
    [SerializeField]
    protected Navigator2D m_Navigator;
    ////////////////////////////////
    ///			Public			 ///
    ////////////////////////////////

    ////////////////////////////////
    ///			Protected		 ///
    ////////////////////////////////
    protected AITask m_CurrentTask;
    //Internal list of priority so this character.
    protected Dictionary<int, List<TaskType>> m_TaskPriorityList = new Dictionary<int, List<TaskType>>();
    
    ////////////////////////////////
    ///			Private			 ///
    ////////////////////////////////

    #region Unity API
    private void Start()
    {
        //Temp location for Init
        Init();
    }
    #endregion

    #region Public API
    public void Init()
    {
        AITaskManager.Instance.Register(this);
    }

    public void OnNotify(params object[] notice)
    {
        if (notice[0] is AITaskManager)
        {
            TaskType task = (TaskType)notice[1];
            //check if task overule current task
            //TODO : above

            AITask newTask = AITaskManager.Instance.GetTask(task);
            OnTaskReceived(newTask);
        }
    }

    public void GetTask()
    {
        m_CurrentTask = AITaskManager.Instance.CheckForTask();

        if (m_CurrentTask != null)
        {
            SetDestination();
            switch (m_CurrentTask.m_Type)
            {
                case TaskType.Wait:
                    break;
                case TaskType.GoTo:
                    break;
                case TaskType.Pilot:
                    break;
                case TaskType.FireFight:
                    break;
                case TaskType.Repair:
                    SetRepairTask();
                    break;
                case TaskType.Shield:
                    break;
                case TaskType.Weapons:
                    break;
                case TaskType.Fight:
                    break;
                default:
                    break;
            }
        }
    }
    #endregion

    #region Protect
    protected void LoadData()
    {
        //if save load save

        //else

    }

    protected void OnTaskReceived(AITask task)
    {
        m_CurrentTask = task;
        console.log("New task received: " + task.m_Type);

        IDamageable toRepair = task.m_Parameters["target"] as IDamageable;

        m_Navigator.SetDestination(toRepair.Transform());
    }
    #endregion

    #region Private
    private void SetDestination()
    {

    }

    private void SetRepairTask()
    {
        
    }

    private void OnTaskCompleted()
    {

    }   
    #endregion
}
