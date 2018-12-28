//////////////////////////////////////////
//	Create by Leonard Marineau-Quintal  //
//		www.leoquintgames.com			//
//////////////////////////////////////////
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : Subject {

    ////////////////////////////////
    ///			Constants		 ///
    ////////////////////////////////

    ////////////////////////////////
    ///			Statics			 ///
    ////////////////////////////////

    ////////////////////////////////
    ///	  Serialized In Editor	 ///
    ////////////////////////////////
    [SerializeField] protected List<ShipComponent> m_ShipComponents = new List<ShipComponent>();
    [SerializeField] protected ShipLayout m_ShipLayout;
    [SerializeField] protected FloorLayout m_Layout;
    [SerializeField] protected BurningController m_BurningController;
    [SerializeField] protected List<Character> m_Crew = new List<Character>();
    [SerializeField] protected AITaskManager m_TaskManager;
    [SerializeField] protected ShipNavigator m_Navigator;
    ////////////////////////////////
    ///			Public			 ///
    ////////////////////////////////
    //temp for debug
    public string DEBUG_SAVED_KEY_FOR_SHIP = "";
    ////////////////////////////////
    ///			Protected		 ///
    ////////////////////////////////
    protected ShipData m_ShipData;
    ////////////////////////////////
    ///			Private			 ///
    ////////////////////////////////
    private float m_DeltaTime;

    //properties
    public ShipData Data
    {
        get { return m_ShipData; }
    }

    public FloorLayout Layout
    {
        get { return m_Layout; }
    }
    
    public AITaskManager TaskManager
    {
        get { return m_TaskManager; }
    }
    #region Unity API
    protected virtual void Awake()
    {
        Initialize();//Temp location to Init
    }

    protected virtual void Update()
    {
        m_DeltaTime = Time.deltaTime;
        for (int i = 0; i < m_ShipComponents.Count; ++i)
        {
            m_ShipComponents[i].OnShipUpdate(m_DeltaTime);
        }

        m_BurningController.OnShipUpdate(m_DeltaTime);
    }

    private void OnDestroy()
    {
    }
    #endregion

    #region Public API
    public void Initialize()
    {
        LoadData();
        LoadLayout();
        m_BurningController.Init(this);
        InitComponents();
        m_Navigator.Init(this);
        InitCrew();
    }

    public void RegisterComponent(ShipComponent component)
    {
        m_ShipComponents.Add(component);
    }
    #endregion

    #region Protect
    protected void LoadData()
    {
        if (!string.IsNullOrEmpty(DEBUG_SAVED_KEY_FOR_SHIP))
        {
            ShipData sd = ShipData.Load(DEBUG_SAVED_KEY_FOR_SHIP);
            if (sd != null)
            {
                m_ShipData = sd;
            }
            else
            {
                Debug.LogError("Ship mdata failed to load.");
            }
        }
    }

    protected void LoadLayout()
    {
        m_Layout.Init(m_ShipLayout, this);

    }

    protected void InitCrew()
    {
        foreach (Character c in m_Crew)
        {
            c.Init(this);
        }
    }

    protected void InitComponents()
    {
        for (int i = 0; i < m_ShipComponents.Count; ++i)
        {
            m_ShipComponents[i].Init(this);
        }
    }
    #endregion

    #region Private
    #endregion
}
