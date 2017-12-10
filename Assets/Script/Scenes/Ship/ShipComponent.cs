//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipComponent : MonoBehaviour {

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
    protected long m_Id;
    ////////////////////////////////
    ///			Private			 ///
    ////////////////////////////////
    private int m_Health;
    private int m_MaxHealth;
    
    private bool m_TaskSentRepair;

    #region Properties
    public int Health
    {
        get { return m_Health; }
    }

    public long Id
    {
        get { return m_Id; }
    }
    #endregion

    #region Unity API

    private void Update()
    {
        ///TODO: Remove Debug
        ///
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ReceiveDamage(10);
        }
    }


    #endregion

    #region Public API
    public void Initialize(Ship ship)
    {
        if (m_Id != 0)
        {
            Debug.LogError("Error trying to Initialize this component more than once.");
            return;
        }
        m_Id = UniqueIdManager.instance.GetID();

        LoadData();

        //Last step is to register the component with it's ship.
        ship.RegisterComponent(this);
    }
    /// <summary>
    /// Generic repair method.
    /// </summary>
    /// <param name="amount">Amount of health the component will gain.</param>
    /// <returns>Return's true if the component is fully repaired.</returns>
    public bool Repair(int amount)
    {
        m_Health += amount;
        if (m_Health >= m_MaxHealth)
        {
            m_Health = m_MaxHealth;
            return true;
        }
        return false;
    }
    /// <summary>
    /// Generic method to damage a component.
    /// </summary>
    /// <param name="amount">Amount of health the component will lose.</param>
    /// <returns>Return's true when health reaches 0.</returns>
    public bool ReceiveDamage(int amount)
    {
        m_Health -= amount;
        if (m_Health <= 0)
        {
            m_Health = 0;
            return true;
        }
        else if (!m_TaskSentRepair && m_Health < m_MaxHealth)
        {
            CreateRepairTask();
        }
        return false;
    }
    #endregion

    #region Protect
    protected void CreateRepairTask()
    {
        AITask repairTask = new AITask();
        repairTask.m_Type = AITask.TaskType.Repair;
        Hashtable parameters = new Hashtable();
        parameters.Add("target", this);
        parameters.Add("callback", "OnComponentDestroy");
        m_TaskSentRepair = true;
        AITaskManager.instance.AddTask(repairTask);
    }
    #endregion

    #region Private
    private void LoadData()
    {
        //TODO: load data from config files and assign values.
        m_Health = 100;
        m_MaxHealth = 100;
        m_TaskSentRepair = false;
    }

    private void OnComponentDestroy()
    {
        //TODO: cancel task
    }
    #endregion
}
