//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthComponent))]
public class ShipComponent : MonoBehaviour, IDamageable
{

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
    protected Ship m_Ship;

    protected Tile m_Tile;
    protected HealthComponent m_HealthComponent;
    protected Character m_MainCharacterSlot;
    protected AITask m_RepairTask = null;
   
    ////////////////////////////////
    ///			Private			 ///
    ////////////////////////////////

    #region Properties
    public Tile Tile
    {
        get { return m_Tile; }
        set { m_Tile = value; }
    }

    public float Health
    {
        get { return m_HealthComponent.CurrentHealth; }
    }

    public long Id
    {
        get { return m_Id; }
    }
    #endregion

    #region Unity API
    protected virtual void Awake()
    {
       
    }
    #endregion

    #region Public API
    public virtual void Init(Ship ship)
    {
        if (m_Id != 0)
        {
            Debug.LogError("Error trying to Initialize this component more than once.");
            return;
        }

        if (m_HealthComponent == null)
        {
            m_HealthComponent = GetComponent<HealthComponent>();
        }

        m_Ship = ship;
        m_Id = UniqueIdManager.Instance.GetID();

        LoadData();

        m_HealthComponent.OnHit -= OnHit;
        m_HealthComponent.OnHit += OnHit;
        m_HealthComponent.OnHealthDepleted -= OnComponentDestroyed;
        m_HealthComponent.OnHealthDepleted += OnComponentDestroyed;
    }

    public virtual void OnShipUpdate(float deltaTime)
    {

    }

    public virtual void Interact(Character character)
    {
        m_MainCharacterSlot = character;
    }

    public virtual void Disengage(Character character)
    {
        m_MainCharacterSlot = null;
    }
    #endregion

    #region Protect
    protected virtual void IssueTask()
    {

    }

    protected virtual void OnHit(float amount)
    {
        //create task
        if (CanRepair() && m_RepairTask == null)
        {
            CreateRepairTask();
        }
        //once repair
        if (!CanRepair() && m_RepairTask != null)
        {
            m_RepairTask = null;
        }
    }

    protected void CreateRepairTask()
    {
        Dictionary<string, object> parameters = new Dictionary<string, object>();
        parameters.Add("target", this);
        parameters.Add("position", this.WorldPosition());
        m_RepairTask = new AITask(AITask.TaskType.Repair, parameters);

        m_Ship.TaskManager.AddTask(m_RepairTask);
    }

    protected virtual void OnComponentDestroyed()
    {
        //stub
    }
    #endregion

    #region Private
    private void LoadData()
    {
        //TODO get Data from Config/load
        m_HealthComponent.Init(100f);
    }
    #endregion

    #region IDamageable
    public bool CanRepair()
    {
        return m_HealthComponent.CurrentRatio < 1f;
    }

    public void Damage(float amount)
    {
        m_HealthComponent.Hit(amount);
    }

    public void Repair(float amount)
    {
        m_HealthComponent.Heal(amount);
    }

    public Vector2Int WorldPosition()
    {
        return m_Tile.Position;
    }
    #endregion

    #region DEBUG
#if UNITY_EDITOR
    [ContextMenu("Simulate Damage")]
    private void SimulateDamage()
    {
        Damage(10f);
    }

    [ContextMenu("Simulate Repair")]
    private void SimulateRepair()
    {
        Repair(10f);
    }
#endif
    #endregion   
}
