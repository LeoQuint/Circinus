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

    protected List<Tile> m_BurningTiles = new List<Tile>();
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

    protected void UpdateFire()
    {
        for (int i = 0; i < m_BurningTiles.Count; ++i)
        {
            UpdateBurningTile(m_BurningTiles[i]);
        }
    }
    #endregion

    #region Private
    private void UpdateBurningTile(Tile tile)
    {
        
    }
    #endregion
}
