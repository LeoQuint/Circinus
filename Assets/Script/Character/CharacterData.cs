//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TaskType = AITask.TaskType;

[CreateAssetMenu(fileName = "NewCharacter", menuName = "Data/Character", order = 3)]
public class CharacterData : MonoBehaviour {

    public struct sTaskPriority
    {
        public TaskType Type;
        public int Priority;

        public sTaskPriority(TaskType type, int priority)
        {
            Type = type;
            Priority = priority;
        }
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
    public Dictionary<int, List<TaskType>> m_TaskPriorities = new Dictionary<int, List<TaskType>>();
    public List<sTaskPriority> m_Priorities = new List<sTaskPriority>();
    ////////////////////////////////
    ///			Private			 ///
    ////////////////////////////////

    #region Unity API
    #endregion

    #region Public API
    public bool IsHigherPriority(TaskType newTask, TaskType currentTask)
    {

        return false;
    }

    public void SetPriority(TaskType type)
    {

    }
    #endregion

    #region Protect
    #endregion

    #region Private
    #endregion
}
