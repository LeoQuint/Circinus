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

    ///enum
    ///
    public enum AIState
    {
        Idle,
        Tasking
    }

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

    protected Action<float> TaskUpdate;
    ////////////////////////////////
    ///			Private			 ///
    ////////////////////////////////
    private AIState m_State = AIState.Tasking;
    //cached targets
    private IDamageable m_RepairTarget = null;

    #region Unity API
    private void Start()
    {
        //Temp location for Init
        Init();
    }

    protected void Update()
    {
        if (TaskUpdate != null)
        {
            TaskUpdate(Time.deltaTime);
        }
    }
    #endregion

    #region Public API
    public void Init()
    {
        AITaskManager.Instance.Register(this);
        m_State = AIState.Idle;
        m_Navigator.Wander();
    }

    public void OnNotify(params object[] notice)
    {
        if (notice[0] is AITaskManager)
        {
            TaskType task = (TaskType)notice[1];
            //check if task overule current task
            //TODO : above
            if (m_CurrentTask == null)
            {
                AITask newTask = AITaskManager.Instance.GetTask(task);
                OnTaskReceived(newTask);
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
        m_State = AIState.Tasking;
        m_CurrentTask = task;
        console.log("New task received: " + task.m_Type);
        IDamageable toRepair = task.m_Parameters["target"] as IDamageable;

        m_Navigator.SetDestination(toRepair.Transform(), OnDestinationReached);
    }

    protected void OnDestinationReached()
    {
        StartTask();
    }

    protected void StartTask()
    {
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
                StartRepairTask();
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
    #endregion

    #region Private
    private void SetDestination()
    {

    }

    private void StartRepairTask()
    {
        console.logStatus("Repair starts");
        TaskUpdate += UpdateRepairTask;
        m_RepairTarget = m_CurrentTask.m_Parameters["target"] as IDamageable;       
    }

    private void UpdateRepairTask(float deltaTime)
    {
        if (m_RepairTarget != null && m_RepairTarget.CanRepair())
        {
            m_RepairTarget.Repair(deltaTime * m_RepairPerSeconds);
        }
        else
        {
            OnTaskCompleted();
        }
    }

    private void OnTaskCompleted()
    {
        console.logStatus("OnTaskCompleted");
        AITaskManager.Instance.OnTaskDone(m_CurrentTask);
        m_CurrentTask = null;
        TaskUpdate = null;
        m_State = AIState.Idle;
        m_Navigator.Wander();
    }   
    #endregion
}
