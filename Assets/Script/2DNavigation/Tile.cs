﻿//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour, ISelectable, IDamageable {

    ////////////////////////////////
    ///			Constants		 ///
    ////////////////////////////////
    public const float CUBE_SIZE = 1f;
    private const string GROUND_LAYER = "Ground";
    private const string MODIFIER_LAYER = "GroundModifier";
    private const string COMPONENT_LAYER = "Component";
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
    protected TileInfo m_Info;
    protected SpriteRenderer m_TileRenderer;
    protected List<SpriteRenderer> m_ModifierRenderer;
    protected SpriteRenderer m_ComponentRenderer;
    protected BoxCollider2D m_Collider;

    protected HealthComponent m_HealthComponent;
    protected Character m_MainCharacterSlot;
    protected AITask m_RepairTask = null;
    protected AITaskManager m_TaskManager;
    //statuses

    ////////////////////////////////
    ///			Private			 ///
    ////////////////////////////////
    private bool m_IsWalkable = true;
    private bool m_IsFlammable = false;

    public Vector2Int Position
    {
        get { return m_Info.Position; }
    }

    public bool CanControl
    {
        get { return false; }
    }

    public int SelectPriority
    {
        get { return 0; }
    }

    public bool IsWalkable
    {
        get { return m_IsWalkable; }
    }

    public bool IsFlammable
    {
        get { return m_IsFlammable; }
    }

    public eSelectableType SelectableType
    {
        get
        {
            return eSelectableType.TILE;
        }
    }
    #region Unity API
    #endregion

    #region Public API
    public void Init(TileInfo info, Ship ship)
    {
        m_Info = info;
        m_TaskManager = ship.TaskManager;
        InitHealth();        
        m_IsWalkable = TileUtilities.IsWalkable(m_Info.Type);
        m_IsFlammable = TileUtilities.IsFlammable(m_Info.Type);
        Build(ship);
    }

    public void Select()
    {
        Debug.Log("Selecting tile: " + gameObject.name);
    }

    public void Deselect()
    {
        //testing
        Damage(20f);
        Debug.Log("Deselecting tile: " + gameObject.name);
    }   
    #endregion

    #region IDamageable
    public bool CanRepair()
    {
        return m_HealthComponent.CurrentRatio < 1f && !m_Info.Modifiers.Contains(eTileModifier.BRUNING);
    }

    public void Damage(float amount)
    {
        m_HealthComponent.Hit(amount);
        if (m_HealthComponent.CurrentRatio <= 0f)
        {
            AddModifier(eTileModifier.BROKEN);
        }
        else if (m_HealthComponent.CurrentRatio < 1f)
        {
            AddModifier(eTileModifier.DAMAGED);
        }
    }

    public void Repair(float amount)
    {
        bool wasBroken = m_HealthComponent.CurrentRatio <= 0f;
        m_HealthComponent.Heal(amount);

        if (wasBroken && m_HealthComponent.CurrentRatio > 0f)
        {
            RemoveModifier(eTileModifier.BROKEN);
        }    
        else if (m_HealthComponent.CurrentRatio >= 1f)
        {
            RemoveModifier(eTileModifier.DAMAGED);
        }
    }

    public Vector2Int WorldPosition()
    {
        return m_Info.Position;
    }
    #endregion

    #region Protect
    protected void InitHealth()
    {
        m_HealthComponent = gameObject.AddComponent<HealthComponent>();
        m_HealthComponent.Init(TileUtilities.TileHealth(m_Info.Type));
        m_HealthComponent.OnHit -= OnHit;
        m_HealthComponent.OnHit += OnHit;
        m_HealthComponent.OnHealthDepleted -= OnComponentDestroyed;
        m_HealthComponent.OnHealthDepleted += OnComponentDestroyed;
    }

    protected void Build(Ship ship)
    {
        //Ground
        m_TileRenderer = GetRenderer("Ground");
        m_TileRenderer.sprite = FloorLayout.SpriteData.GetSprite(m_Info.Type);
        m_TileRenderer.size = Vector2.one * CUBE_SIZE;
        m_TileRenderer.sortingLayerName = GROUND_LAYER;
        //Modifiers
        m_ModifierRenderer = new List<SpriteRenderer>();
        for (int i = 0; i < m_Info.Modifiers.Count; ++i)
        {
            SpriteRenderer sr = GetRenderer(string.Format("Modifier{0}", i.ToString()));
            sr.sprite = FloorLayout.SpriteData.GetSprite(m_Info.Modifiers[i]);
            sr.size = Vector2.one * CUBE_SIZE;
            sr.sortingLayerName = MODIFIER_LAYER;
            sr.sortingOrder = i;
            m_ModifierRenderer.Add(sr);
        }
        //Component
        BuildComponent(m_Info.Component, ship);

        AddCollider();
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
        parameters.Add("position", WorldPosition());
        m_RepairTask = new AITask(AITask.TaskType.Repair, parameters);
        m_TaskManager.AddTask(m_RepairTask);
    }

    protected virtual void OnComponentDestroyed()
    {
        //stub
    }

    protected void AddCollider()
    {
        m_Collider = gameObject.AddComponent<BoxCollider2D>();
        m_Collider.size = Vector2.one * CUBE_SIZE;
    }

    protected void AddModifier(eTileModifier modifier)
    {
        if (!m_Info.Modifiers.Contains(modifier))
        {
            int index = -1;
            for (int i = 0; i < m_Info.Modifiers.Count; ++i)
            {
                if (m_Info.Modifiers[i] == eTileModifier.NONE)
                {
                    index = i;
                    break;
                }
            }

            if (index >= 0)//reuse renderer
            {
                SpriteRenderer sr = m_ModifierRenderer[index];
                sr.sprite = FloorLayout.SpriteData.GetSprite(modifier);
                sr.size = Vector2.one * CUBE_SIZE;
                sr.sortingLayerName = MODIFIER_LAYER;
                sr.enabled = true;
                m_Info.Modifiers[index] = modifier;
            }
            else//create new renderer
            {
                SpriteRenderer sr = GetRenderer("Modifer");
                sr.sprite = FloorLayout.SpriteData.GetSprite(modifier);
                sr.size = Vector2.one * CUBE_SIZE;
                sr.sortingLayerName = MODIFIER_LAYER;
                m_ModifierRenderer.Add(sr);
                m_Info.Modifiers.Add(modifier);
            }
        }
    }

    protected void RemoveModifier(eTileModifier modifier)
    {
        if (m_Info.Modifiers.Contains(modifier))
        {
            for (int i = 0; i < m_Info.Modifiers.Count; ++i)
            {
                if (m_Info.Modifiers[i] == modifier)
                {
                    m_ModifierRenderer[i].enabled = false;
                    m_Info.Modifiers[i] = eTileModifier.NONE;
                    break;
                }
            }            
        }
    }
    #endregion

    #region Private
    private SpriteRenderer GetRenderer(string name)
    {
        GameObject go = new GameObject(name);
        go.transform.SetParent(transform, false);
        SpriteRenderer sr = go.AddComponent<SpriteRenderer>();
        sr.drawMode = SpriteDrawMode.Sliced;

        return sr;
    }

    private void BuildComponent(eShipComponent component, Ship ship)
    {
        if (component != eShipComponent.EMPTY)
        {
            //visuals
            m_ComponentRenderer = GetRenderer("Component");
            m_ComponentRenderer.sprite = FloorLayout.SpriteData.GetSprite(component);
            m_ComponentRenderer.size = Vector2.one * CUBE_SIZE;
            m_ComponentRenderer.sortingLayerName = COMPONENT_LAYER;
            //scripts
            ShipComponent sc = m_ComponentRenderer.gameObject.AddShipComponent(component, this);
            ship.RegisterComponent(sc);
        }
    }
    #endregion
}
