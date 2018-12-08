//////////////////////////////////////////
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
    private const float CUBE_SIZE = 1f;
    private const string GROUND_LAYER = "Ground";
    private const string MODIFIER_LAYER = "GroundModifer";
    private const string COMPONENT_LAYER = "Component";
    ////////////////////////////////
    ///			Statics			 ///
    ////////////////////////////////
    private static Vector3 BottomLeft = new Vector3(-1f,-1f, 0f);
    private static Vector3 BottomRight = new Vector3(1f, -1f, 0f);
    private static Vector3 TopLeft = new Vector3(-1f, 1f, 0f);
    private static Vector3 TopRight = new Vector3(1f, 1f, 0f);
    ////////////////////////////////
    ///	  Serialized In Editor	 ///
    ////////////////////////////////

    ////////////////////////////////
    ///			Public			 ///
    ////////////////////////////////

    ////////////////////////////////
    ///			Protected		 ///
    ////////////////////////////////
    protected sTileInfo m_Info;
    protected SpriteRenderer m_TileRenderer;
    protected List<SpriteRenderer> m_ModifierRenderer;
    protected SpriteRenderer m_ComponentRenderer;
    protected BoxCollider2D m_Collider;

    protected HealthComponent m_HealthComponent;
    protected Character m_MainCharacterSlot;
    protected AITask m_RepairTask = null;
    ////////////////////////////////
    ///			Private			 ///
    ////////////////////////////////
    private bool m_IsWalkable = true;

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
    public void Init(sTileInfo info)
    {
        m_Info = info;
        InitHealth();        
        m_IsWalkable = TileUtilities.IsWalkable(m_Info.Type);
        Build();
    }

    public void Select()
    {
        Debug.Log("Selecting tile: " + gameObject.name);
    }

    public void Deselect()
    {
        //testing
        m_HealthComponent.Hit(10f);
        Debug.Log("Deselecting tile: " + gameObject.name);
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
        m_RepairTask = new AITask(AITask.TaskType.Repair, parameters);

        AITaskManager.Instance.AddTask(m_RepairTask);
    }

    protected virtual void OnComponentDestroyed()
    {
        //stub
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

    protected void Build()
    {
        //Ground
        m_TileRenderer = GetRenderer("Ground");
        m_TileRenderer.sprite = FloorLayout.SpriteData.GetSprite(m_Info.Type);
        m_TileRenderer.size = Vector2.one * CUBE_SIZE;
        m_TileRenderer.sortingLayerName = GROUND_LAYER;
        //Modifiers
        for (int i = 0; i < m_Info.Modifiers.Count; ++i)
        {
            if (i == 0)
            {
                m_ModifierRenderer = new List<SpriteRenderer>();
            }
            SpriteRenderer sr = GetRenderer(string.Format("Modifier{0}", i.ToString()));
            sr.sprite = FloorLayout.SpriteData.GetSprite(m_Info.Modifiers[i]);
            sr.size = Vector2.one * CUBE_SIZE;
            sr.sortingLayerName = MODIFIER_LAYER;
            sr.sortingOrder = i;
            m_ModifierRenderer.Add(sr);
        }
        //Component
        m_ComponentRenderer = GetRenderer("Component");
        m_ComponentRenderer.sprite = FloorLayout.SpriteData.GetSprite(m_Info.Component);
        m_ComponentRenderer.size = Vector2.one * CUBE_SIZE;
        m_ComponentRenderer.sortingLayerName = COMPONENT_LAYER;

        AddCollider();
    }

    protected void AddCollider()
    {
        m_Collider = gameObject.AddComponent<BoxCollider2D>();
        m_Collider.size = Vector2.one * CUBE_SIZE;
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
    #endregion
}
