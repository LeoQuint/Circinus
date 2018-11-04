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
    [SerializeField]protected List<ShipComponent> m_ShipComponents = new List<ShipComponent>();
    ////////////////////////////////
    ///			Private			 ///
    ////////////////////////////////
    private float m_DeltaTime;
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
    }
    #endregion

    #region Public API
    public void Initialize()
    {
        for (int i = 0; i < m_ShipComponents.Count; ++i)
        {
            m_ShipComponents[i].Initialize(this);
        }
    }

    public void RegisterComponent(ShipComponent component)
    {
        m_ShipComponents.Add(component);
        component.Initialize(this);
    }
    #endregion

    #region Protect
    #endregion

    #region Private
    #endregion
}
