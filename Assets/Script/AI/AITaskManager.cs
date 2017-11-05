//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITaskManager : Subject {

    ////////////////////////////////
    ///			Constants		 ///
    ////////////////////////////////

    ////////////////////////////////
    ///			Statics			 ///
    ////////////////////////////////

    public static AITaskManager instance;

    ////////////////////////////////
    ///	  Serialized In Editor	 ///
    ////////////////////////////////

    ////////////////////////////////
    ///			Public			 ///
    ////////////////////////////////

    ////////////////////////////////
    ///			Protected		 ///
    ////////////////////////////////

    protected Dictionary<AITask.TaskType, List<AITask>> m_PendingTasks = new Dictionary<AITask.TaskType, List<AITask>>();

    ////////////////////////////////
    ///			Private			 ///
    ////////////////////////////////


    #region Unity API
    protected void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }        
    }

    protected void Update()
    {

    }

    protected void OnDestroy()
    {
        ClearAllTasks();
        m_PendingTasks = null;
    }

    #endregion

    #region Public API
    public void AddTask(AITask task)
    {
        //TODO: Validate task

        //TODO: notify observer of a new task.
        Notify(task.m_Type);

        m_PendingTasks[task.m_Type].Add(task);
    }

    public void RemoveTask(int taskID/*TODO: Task id system*/)
    {

    }

    public AITask CheckForTask(/*TODO: Get list of priorities*/)
    {
        GetHighestPriorityTask();

        return new AITask();
    }

    public void ClearAllTasks()
    {
        m_PendingTasks.Clear();
    }

    #endregion

    #region Protect



    #endregion

    #region Private

    private AITask GetHighestPriorityTask(/*TODO: Get list of priorities*/)
    {
        return new AITask();
    }



    #endregion
}
