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
    private ShipComponent m_InteractingComponent = null;

    #region Unity API
    private void Start()
    {
        m_Navigator.Init();
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
    public override void Init()
    {
        base.Init();
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
                if (newTask != null)
                {
                    OnTaskReceived(newTask);
                }
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
        IDamageable target = task.m_Parameters["target"] as IDamageable;
        m_Navigator.SetDestination(target.WorldPosition(), OnDestinationReached);
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
                StartPilotTask();
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

    private void StartPilotTask()
    {
        console.logStatus("Piloting starts");
        TaskUpdate += UpdatePilotingTask;
        m_InteractingComponent = m_CurrentTask.m_Parameters["target"] as PilotingStation;
        m_InteractingComponent.Interact(this);
    }

    private void UpdatePilotingTask(float deltaTime)
    {
        //stubb
    }

    private void OnTaskCompleted()
    {
        console.logStatus("OnTaskCompleted");
        AITaskManager.Instance.OnTaskDone(m_CurrentTask);
        m_CurrentTask = null;
        TaskUpdate = null;
        AITask newTask = AITaskManager.Instance.CheckForTask();
        if (newTask != null)
        {
            OnTaskReceived(newTask);
        }
        else
        {
            m_State = AIState.Idle;
            m_Navigator.Wander();
        }
    }

    private void OnTaskInterrupt(AITask replacementTask = null)
    {
        console.logStatus("OnTaskInterrupt");

        if (m_InteractingComponent != null)
        {
            m_InteractingComponent.Disengage(this);
        }
        //return the task back.
        AITaskManager.Instance.AddTask(m_CurrentTask);
        m_CurrentTask = null;
        TaskUpdate = null;
        if (replacementTask != null)
        {
            OnTaskReceived(replacementTask);
        }
        else
        {
            AITask newTask = AITaskManager.Instance.CheckForTask();
            if (newTask != null)
            {
                OnTaskReceived(newTask);
            }
            else
            {
                m_State = AIState.Idle;
                m_Navigator.Wander();
            }
        }       
    }
    #endregion
}
