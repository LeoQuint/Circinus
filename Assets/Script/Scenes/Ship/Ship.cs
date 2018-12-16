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
    [SerializeField] protected ShipLayout m_Layout;
    [SerializeField] protected FloorLayout m_Floors;

    [SerializeField] protected BurningController m_BurningController;
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
        m_BurningController.Init(m_Floors);
    }

    public void RegisterComponent(ShipComponent component)
    {
        m_ShipComponents.Add(component);
    }
    #endregion

    #region Protect
    protected void LoadLayout()
    {
        m_Floors.Init(m_Layout);
    }   
    #endregion

    #region Private
    #endregion
}
