﻿//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TaskType = AITask.TaskType;

public class AITaskManager : Subject {

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
    protected Dictionary<TaskType, List<AITask>> m_PendingTasks = new Dictionary<TaskType, List<AITask>>();
    protected HashSet<long> m_CompletedTasks = new HashSet<long>();
    ////////////////////////////////
    ///			Private			 ///
    ////////////////////////////////

    #region Unity API
    protected void OnDestroy()
    {
        ClearAllTasks();
        m_PendingTasks = null;
    }

    #endregion

    #region Public API
    public void AddTask(AITask task)
    {
        console.logInfo("Creating task " + task.m_Type);
        if(!m_PendingTasks.ContainsKey(task.m_Type))
        {
            m_PendingTasks.Add(task.m_Type, new List<AITask>());
        }
        m_PendingTasks[task.m_Type].Add(task);

        Notify(this, task.m_Type);
    }

    public AITask GetTask(TaskType type, int id)
    {       
        if (m_PendingTasks.ContainsKey(type) && m_PendingTasks[type].Count > 0)
        {
            AITask task = null;
            foreach (AITask t in m_PendingTasks[type])
            {
                if (t.ID == id)
                {
                    task = t;
                }
            }

            if (task != null)
            {
                m_PendingTasks[type].Remove(task);
            }

            console.logInfo("Getting task " + task.m_Type + " #" + task.ID);
            return task;
        }
        else
        {
            return null;
        }
    }

    public AITask GetTask(TaskType type)
    {
        if (m_PendingTasks.ContainsKey(type) && m_PendingTasks[type].Count > 0)
        {
            AITask task = m_PendingTasks[type][0];
            m_PendingTasks[type].Remove(task);
            return task;
        }
        else
        {
            return null;
        }
    }    

    public void OnTaskDone(AITask task)
    {
        m_CompletedTasks.Add(task.ID);
        RemoveTask(task.m_Type, task.ID);
    }    

    public AITask CheckForTask(/*TODO: Get list of priorities*/)
    {
        return GetHighestPriorityTask();
    }

    public void ClearAllTasks()
    {
        m_PendingTasks.Clear();
    }

    #endregion

    #region Protect
    protected void RemoveTask(TaskType type, long id)
    {
        if (m_PendingTasks.ContainsKey(type) && m_PendingTasks[type].Count > 0)
        {
            AITask task = null;
            foreach (AITask t in m_PendingTasks[type])
            {
                if (t.ID == id)
                {
                    task = t;
                }
            }

            if (task != null)
            {
                console.logInfo("Removing task " + task.m_Type + " #" + task.ID);
                m_PendingTasks[type].Remove(task);
            }
        }
    }
    #endregion

    #region Private
    private AITask GetHighestPriorityTask(/*TODO: Get list of priorities*/)
    {
        AITask highestPriority = null;
        foreach (KeyValuePair<TaskType, List<AITask>> pair in m_PendingTasks)
        {
            if (pair.Value != null && pair.Value.Count > 0)
            {
                return GetTask(pair.Key);
            }
        }
        return highestPriority;
    }
    #endregion
}
