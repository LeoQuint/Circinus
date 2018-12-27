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

    ////////////////////////////////
    ///			Public			 ///
    ////////////////////////////////

    ////////////////////////////////
    ///			Protected		 ///
    ////////////////////////////////
    [SerializeField] protected List<ShipComponent> m_ShipComponents = new List<ShipComponent>();
    [SerializeField] protected ShipLayout m_ShipLayout;
    [SerializeField] protected FloorLayout m_Layout;

    [SerializeField] protected BurningController m_BurningController;

    [SerializeField] protected List<Character> m_Crew = new List<Character>();
    ////////////////////////////////
    ///			Private			 ///
    ////////////////////////////////
    private float m_DeltaTime;
    #region Unity API
    protected virtual void Awake()
    {
        ServiceLocator.RegisterService<Ship>(this);
        Initialize();//Temp location to Init
    }

    private void Start()
    {

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
        ServiceLocator.UnregisterService<Ship>(this);
    }
    #endregion

    #region Public API
    public void Initialize()
    {
        LoadLayout();
        m_BurningController.Init(m_Layout);
        InitCrew();
    }

    public void RegisterComponent(ShipComponent component)
    {
        m_ShipComponents.Add(component);
    }
    #endregion

    #region Protect
    protected void LoadLayout()
    {
        m_Layout.Init(m_ShipLayout);
    }

    protected void InitCrew()
    {
        foreach (Character c in m_Crew)
        {
            c.Init(m_Layout);
        }
    }
    #endregion

    #region Private
    #endregion
}
