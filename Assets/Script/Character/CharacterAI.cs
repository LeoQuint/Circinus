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
        Tasking,
        Selected
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
    private Vector2Int m_TargetPosition = Vector2Int.zero;
    private IDamageable m_RepairTarget = null;
    private ShipComponent m_InteractingComponent = null;
    private BurningController m_BurningController = null;

    #region Unity API
    protected void Update()
    {
        if (TaskUpdate != null)
        {
            TaskUpdate(Time.deltaTime);
        }
    }
    #endregion

    #region Public API
    public override void Init(Ship ship)
    {
        base.Init(ship);
        ship.TaskManager.Register(this);
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
                AITask newTask = m_Ship.TaskManager.GetTask(task);
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
        m_TargetPosition = (Vector2Int)task.m_Parameters["position"];
        m_Navigator.SetDestination(m_TargetPosition, OnDestinationReached);
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
                StartFireFightingTask();
                break;
            case TaskType.Repair:
                StartRepairTask();
                break;
            case TaskType.Shield:
                StartShieldTask();
                break;
            case TaskType.Weapons:
                StartWeaponTask();
                break;
            case TaskType.Fight:
                StartCombatTask();
                break;
            default:
                break;
        }
    }

    protected override void OnOrderGiven(Tile tile, Vector2 innerPosition)
    {
        base.OnOrderGiven(tile, innerPosition);
        OnTaskInterrupt();
    }
    #endregion

    #region Private
    private void SetDestination()
    {

    }

    private void StartFireFightingTask()
    {
        console.logStatus("Firefighting starts");
        TaskUpdate += UpdateFireFightingTask;
        m_BurningController = m_CurrentTask.m_Parameters["controller"] as BurningController;
    }

    private void UpdateFireFightingTask(float deltaTime)
    {
        bool hasExtinghuisedFire = m_BurningController.RemoveFireRank(m_TargetPosition, m_FireFightingPerSeconds * deltaTime);
        if (hasExtinghuisedFire)
        {
            Debug.Log("Fire task done");
            OnTaskCompleted();
        }
    }

    private void StartShieldTask()
    {
        console.logStatus("Shield starts");
    }

    private void UpdateShieldTask(float deltaTime)
    {

    }

    private void StartWeaponTask()
    {
        console.logStatus("Weapon starts");
        TaskUpdate += UpdateWeaponTask;
        m_InteractingComponent = m_CurrentTask.m_Parameters["target"] as WeaponStation;
        m_InteractingComponent.Interact(this);
    }

    private void UpdateWeaponTask(float deltaTime)
    {

    }

    private void StartCombatTask()
    {
        console.logStatus("Combat starts");
    }

    private void UpdateCombatTask()
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
        m_Ship.TaskManager.OnTaskDone(m_CurrentTask);
        m_CurrentTask = null;
        TaskUpdate = null;
        m_State = AIState.Idle;
        m_BurningController = null;

        AITask newTask = m_Ship.TaskManager.CheckForTask();
        if (newTask != null)
        {
            Debug.Log("Starting a new task");
            OnTaskReceived(newTask);
        }
        else
        {
            Debug.Log("No new task to start.");
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
        if (m_CurrentTask != null)
        {
            m_Ship.TaskManager.AddTask(m_CurrentTask);
            m_CurrentTask = null;
            TaskUpdate = null;
            if (replacementTask != null)
            {
                OnTaskReceived(replacementTask);
            }
            else
            {
                AITask newTask = m_Ship.TaskManager.CheckForTask();
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
    }
    #endregion
}
